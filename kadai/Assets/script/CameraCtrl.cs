using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

    private PlayerMove PM;

	
	void Start () {
        PM = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
	}

    //カメラ切り替え
    public void changeSight(bool type)
    {
        if (type)
        {
            changeCameraMode_1Person();
        }
        else
        {
            changeCameraMode_3Person();
        }
    }
	
	//1人称カメラ切り替え
    private void changeCameraMode_1Person()
    {
        if(transform.parent == null)
        {
            transform.parent = PM.transform;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
        }
    }

    //3人称カメラ切り替え
    private void changeCameraMode_3Person()
    {
        if(transform.parent != null)
        {
            transform.parent = null;
            transform.position = new Vector3(0, 4, -10);
            transform.localEulerAngles = Vector3.zero;
        }
    }
    
}
