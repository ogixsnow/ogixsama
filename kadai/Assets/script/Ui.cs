using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour {
    public Text[] text_gun_num;

	//残段数を変更
    public void changeText_GunNum(int num)
    {
        foreach(Text t in text_gun_num)
        {
            if(t != null)
            {
                t.text = "残弾:" + num;
            }
        }
    }
}
