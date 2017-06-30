using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour {
    public GameObject prefab_HitEffect2;
	
	void Start () {
        StartCoroutine("bom");//コルーチン開始
	}
	
	IEnumerator bom()
    {
        yield return new WaitForSeconds(2.5f);//2.5秒後に処理

        GameObject effect = Instantiate(prefab_HitEffect2, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 1.0f);

        Destroy(gameObject);
    }
}
