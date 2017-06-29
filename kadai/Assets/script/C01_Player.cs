using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C01_Player : MonoBehaviour {
    private CharacterController charaCon;
    private Vector3 move = Vector3.zero;
    private float speed = 5.0f;

	// Use this for initialization
	void Start () {
        charaCon = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        charaCon.Move(move * speed * Time.deltaTime);
	}
}
