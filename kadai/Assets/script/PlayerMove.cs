using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private CharacterController charaCon;               //キャラクターコンポーネントの変数
    private Vector3             move = Vector3.zero;    //プレイヤー移動量
    private float               speed = 2.0f;           //プレイヤー移動速度
    private float               jumpPower = 2.5f;       //プレイヤーの跳躍力
    private float               rotationSpeed = 180.0f; //プレイヤー回転速度
    private const float         GRAVITY = 9.8f;         //重力
    private bool                moveType = true;        //1人称視点の時:true 3人称視点の時:false

    public  GameObject          targetEnemy = null;     //Enemy格納用変数
    public  GameObject          prefab_hitEffect1;      //プレイヤー攻撃時、ヒットエフェクト
    private Vector3             attackPoint;            //攻撃箇所（位置）
    private Weapon              weapon;                //武器

    public GameObject prefab_bom;
    private bool used_bom = false;

    public int gun_num;
    private const int GUM_MAX_NUM=30;
    private Ui ui;

    
    void Start () {
        ui = GameObject.Find("GameRoot").GetComponent<Ui>();
        weapon = new Weapon();                          //武器のメモリを確保し、初期化
        charaCon = GetComponent<CharacterController>();
        gun_num = GUM_MAX_NUM;
        ui.changeText_GunNum(gun_num);
        
    }


    void Update() {
        setTargetEnemy();                                       //Enemy(ターゲット)の情報を取得
        if (Input.GetMouseButtonDown(0)) { attack_weapon(); }   //左クリックで武器攻撃
        if (Input.GetMouseButtonDown(1)) { change_weapon(); }   //右クリックで武器変更

        if (moveType)
        {
            playerMove_1Parson();                               //1人称視点操作
        }
        else
        {
            playerMove_3Parson();                               //3人称視点操作
        }
    }

    //武器攻撃
    private void attack_weapon()
    {
        switch (weapon.getType())
        {
            case 0:attack01_gun(); break;
            case 1:attack02_bom(); break;
        }
        
    }

    //銃攻撃
    private void attack01_gun()
    {
        if (gun_num == 0) { return; }

        if(targetEnemy != null)
        {
            GameObject effect = Instantiate(prefab_hitEffect1, attackPoint, Quaternion.identity) as GameObject;
            Destroy(effect, 0.2f);
            Destroy(targetEnemy);
        }

        gun_num--;
        ui.changeText_GunNum(gun_num);
        if(gun_num == 0) { StartCoroutine("reChargeGun"); }
        
    }

    //銃の再装填コルーチン
    IEnumerator reChargeGun()
    {
        yield return new WaitForSeconds(3.0f);
        gun_num = GUM_MAX_NUM;
        ui.changeText_GunNum(gun_num);
    }

    


    //ボム攻撃
    private void attack02_bom()
    {
        if (!used_bom)
        {
            Vector3 pos = transform.position + transform.TransformDirection(Vector3.forward);
            GameObject bom = Instantiate(prefab_bom, pos, Quaternion.identity) as GameObject;

            Vector3 bom_speed = transform.TransformDirection(Vector3.forward) * 5;
            bom_speed += Vector3.up * 5;
            bom.GetComponent<Rigidbody>().velocity = bom_speed;

            bom.GetComponent<Rigidbody>().angularVelocity = Vector3.forward * 7;

            used_bom = true;
            StartCoroutine("reChargeBom");
        }
    }

    //ボムの連射管理コルーチン
    IEnumerator reChargeBom()
    {
        yield return new WaitForSeconds(2.5f);
        used_bom = false;
    }

    //武器変更
    private void change_weapon()
    {
        weapon.changeWeapon();          //武器切り替え
    }

    //視点切り替え
    public void changeMoveType(bool type)
    {
        moveType = type;
    }

    //1人称視点
    private void playerMove_1Parson() { 
        //移動量取得
        float y = move.y;
        move = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));  //上下キー入力を取得、移動量に代入
        move = transform.TransformDirection(move);                  //プレイヤー基準の移動方向へ修正
        move *= speed;                                              //移動速度を乗算

        //重力/ジャンプ処理
        move.y += y;
        if (charaCon.isGrounded)                                    //地面に設置していたら
        {
            if (Input.GetKeyDown(KeyCode.Space))                    //ジャンプ処理
            {
                move.y = jumpPower;
            }
        }
        move.y -= GRAVITY * Time.deltaTime;                         //重力代入

        //プレイヤーの向き変更
        Vector3 playerDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);                                 //左右キー入力を取得。移動方向に代入
        playerDir = transform.TransformDirection(playerDir);                                                      //プレイヤー基準の向きたい方向へ修正
        if (playerDir.magnitude > 0.1f)
        {
            Quaternion q = Quaternion.LookRotation(playerDir);                                                   //向きたい方角をQuaternion型に直す
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed*Time.deltaTime);  //向きをqに向けて徐々に変化
        }

        //移動処理
        charaCon.Move(move * speed * Time.deltaTime);            
	}

    //3人称視点移動
    private void playerMove_3Parson()
    {
        //移動量取得
        float y = move.y;
        move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));  //上下左右のキー入力を取得し、移動量に代入
    
        Vector3 playerDir = move;       //移動方向を取得
        move *= speed;                  //移動速度を乗算

        //重力/ジャンプ処理
        move.y += y;
        if (charaCon.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                move.y = jumpPower;
            }
        }
        move.y -= GRAVITY * Time.deltaTime;

        //プレイヤーの方向変更
        if(playerDir.magnitude > 0.1f)
        {
            Quaternion q = Quaternion.LookRotation(playerDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);
        }
        //移動処理
        charaCon.Move(move * Time.deltaTime);
    }

    //Enemy(ターゲット)情報を取得
    private void setTargetEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //クリックした位置から光線を真っすぐ発射
        RaycastHit hitInfo;                                             //ヒット情報を格納する変数を作成

        if(Physics.Raycast(ray, out hitInfo , 10))                      //カメラから距離10の光線を出し、もし何かに当たった場合
        {
            if(hitInfo.collider.gameObject.tag == "Enemy")              //タグがEnemyだったら
            {
                targetEnemy = hitInfo.collider.gameObject;              //オブジェクトを参照
                attackPoint = hitInfo.point;                            //光線が当たった位置を取得
                return;                                                 //ターゲットが見つかったので、処理を抜ける
            }
        }
        targetEnemy = null;                                             //Enemyが見つからない場合はnull
    }

   
}
