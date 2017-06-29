using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCtrl : MonoBehaviour {
    private PlayerMove PM;
    private CameraCtrl cam_mainCamera, cam_subCamera;
    private bool cameraSight = true;


	void Start () {
        PM = GameObject.Find("Player").GetComponent<PlayerMove>();
        cam_mainCamera = GameObject.Find("Main Camera").GetComponent<CameraCtrl>();
        cam_subCamera = GameObject.Find("Sub camera").GetComponent<CameraCtrl>();

        changeCameraSight();
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cameraSight = !cameraSight;
            changeCameraSight();
        }
	}

    //視点切り替え命令
    private void changeCameraSight()
    {
        PM.changeMoveType(cameraSight);
        cam_mainCamera.changeSight(cameraSight);
        cam_subCamera.changeSight(!cameraSight);
    }
}
