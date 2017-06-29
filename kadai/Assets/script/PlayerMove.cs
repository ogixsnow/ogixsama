using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private CharacterController charaCon;
    private Vector3 move = Vector3.zero;
    private float speed = 5.0f;

    private const float GRAVITY = 9.8f;

    // Use this for initialization
    void Start () {
        charaCon = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float y = move.y;
        move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        move *= speed;
        move.y += y;
        move.y -= GRAVITY * Time.deltaTime;
        charaCon.Move(move * speed * Time.deltaTime);
	}
}
