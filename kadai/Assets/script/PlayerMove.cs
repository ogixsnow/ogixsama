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

    public GameObject targetEnemy = null;               //Enemy格納用変数
    public GameObject prefab_hitEffect1;                //プレイヤー攻撃時、ヒットエフェクト
    private Vector3 attackPoint;                        //攻撃箇所（位置）
    private Weapon weapon;                              //武器

    
    void Start () {
        weapon = new Weapon();                          //武器のメモリを確保し、初期化
        charaCon = GetComponent<CharacterController>();
	}


    void Update() {
        setTargetEnemy();                                       //Enemy(ターゲット)の情報を取得
        if (Input.GetMouseButtonDown(0)) { attack_weapon(); }   //左クリックで武器攻撃
        if (Input.GetMouseButtonDown(1)) { change_weapon(); }   //右クリックで武器変更

        if (moveType)
        {
            playerMove_1Parson();
        }
        else
        {
            playerMove_3Parson();
        }
    }

    //武器攻撃
    private void attack_weapon()
    {
        switch (weapon.getType())
        {
            case 0:
                if(targetEnemy != null)
                {
                    GameObject effect = Instantiate(prefab_hitEffect1, attackPoint, Quaternion.identity) as GameObject;
                    Destroy(effect, 0.2f);
                    Destroy(targetEnemy);
                }
                break;
        }
    }

    //武器変更
    private void change_weapon()
    {
        weapon.changeWeapon();
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
        move = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);
        move *= speed;

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

        //プレイヤーの向き変更
        Vector3 playerDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        playerDir = transform.TransformDirection(playerDir);
        if (playerDir.magnitude > 0.1f)
        {
            Quaternion q = Quaternion.LookRotation(playerDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed*Time.deltaTime);
        }

        //移動処理
        charaCon.Move(move * speed * Time.deltaTime);
	}

    //3人称視点移動
    private void playerMove_3Parson()
    {
        //移動量取得
        float y = move.y;
        move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        Vector3 playerDir = move;
        move *= speed;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo , 10))
        {
            if(hitInfo.collider.gameObject.tag == "Enemy")
            {
                targetEnemy = hitInfo.collider.gameObject;
                attackPoint = hitInfo.point;
                return;
            }
        }
        targetEnemy = null;
    }

   
}
