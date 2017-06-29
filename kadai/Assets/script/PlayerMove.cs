using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private CharacterController charaCon;
    private Vector3             move = Vector3.zero;
    private float               speed = 2.0f;
    private float               jumpPower = 2.5f;
    private float               rotationSpeed = 180.0f;
    private const float         GRAVITY = 9.8f;

    
    void Start () {
        charaCon = GetComponent<CharacterController>();
	}


    void Update() {
        playerMove_1Parson();
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
}
