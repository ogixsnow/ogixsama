using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

    private PlayerMove PM;

	// Use this for initialization
	void Start () {
        PM = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            changeCameraMode();
        }

	}

    //カメラを切り替える処理
    private void changeCameraMode()
    {
        if(transform.parent == null)
        {
            transform.parent =        PM.transform;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.parent = null;
            transform.position = new Vector3(0, 4, -10);
        }
    }
}
