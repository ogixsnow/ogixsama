using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon{
    private int type =0;
    private int num = 2;

    public void changeWeapon()
    {
        type = (type + 1) % num;
        Debug.Log("現在の武器:" + type);
    }
}
