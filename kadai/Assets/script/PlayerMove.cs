﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private CharacterController charaCon;
    private Vector3             move = Vector3.zero;
    private float               speed = 2.0f;
    private float               jumpPower = 2.5f;
    private float               rotationSpeed = 180.0f;
    private const float         GRAVITY = 9.8f;
    private bool                moveType = true;

    public GameObject targetEnemy = null;
    public GameObject prefab_hitEffect1;
    private Vector3 attackPoint;
    private Weapon weapon;

    
    void Start () {
        weapon = new Weapon();
        charaCon = GetComponent<CharacterController>();
	}


    void Update() {
        if (Input.GetMouseButtonDown(1))
        {
            weapon.changeWeapon();
        }
        setTargetEnemy();
        attack_LClick();

        if (moveType)
        {
            playerMove_1Parson();
        }
        else
        {
            playerMove_3Parson();
        }
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

    private void attack_LClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(targetEnemy != null)
            {
                GameObject effect = Instantiate(prefab_hitEffect1, attackPoint, Quaternion.identity) as GameObject;
                Destroy(effect, 0.2f);
                Destroy(targetEnemy);
            }
        }
    }
}
