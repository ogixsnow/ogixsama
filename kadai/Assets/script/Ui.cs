using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour {
    public Text[] text_gun_num;
    public Text text_bom;

    //手榴弾のテキスト変更
    public void changeText_Bom(bool used)
    {
        if(text_bom != null)
        {
            text_bom.text = used ? "装填中" : "使用可";
        }
    }

    //武器タイプによるテキストの表示オン/オフ
    public void changeText_enable(int type)
    {
        switch (type)
        {
            case 0:
                foreach(Text t in text_gun_num) { t.enabled = true; }
                text_bom.enabled = false;
                break;
            case 1:
                foreach(Text t in text_gun_num) { t.enabled = false; }
                text_bom.enabled = true;
                break;
        }
    }

    //テキスト初期化用
    public void initialize(int type,int num , bool used)
    {
        changeText_enable(type);

        changeText_GunNum(num);
        changeText_Bom(used);
    }


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
