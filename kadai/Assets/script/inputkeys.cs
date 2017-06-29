using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputkeys : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = this.transform.position;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            v.x -= 0.05f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            v.x += 0.05f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            v.z += 0.05f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            v.z -= 0.05f;
        }
        this.transform.position = v;

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(new Vector3(0, 0, -1f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(new Vector3(0, 0, 1f));
        }
    }
}
