using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushControl : MonoBehaviour
{
    private GameObject bush1;//²Ý´Ô1
    private GameObject bush2;//²Ý´Ô2
    private void DestroyBush1()
    {
        GameObject.Destroy(bush1);
    }
    private void DestroyBush2()
    {
        GameObject.Destroy(bush2);
    }
    void Start()
    {
        //²éÕÒ×é¼þ
        bush1 = GameObject.Find("Bush1");
        bush2 = GameObject.Find("Bush2");
        Tween t1 = bush1.transform.DOMove(new Vector3(bush1.transform.position.x-2000, bush1.transform.position.y, 0), 5);
        Tween t2 = bush2.transform.DOMove(new Vector3(bush2.transform.position.x + 2000, bush2.transform.position.y, 0), 5);
        t1.OnComplete(DestroyBush1);
        t2.OnComplete(DestroyBush2);
    }
}
