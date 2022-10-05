using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapMove : MonoBehaviour
{
    private GameObject[] background = new GameObject[3];//地图背景
    private void Start()
    {
        //获取组件
        for(int i=0;i<3;i++)
        {
            background[i] = GameObject.Find("background" + (i+1).ToString());
        }
    }
    private void Update()
    {
        background[0].transform.position = Vector3.MoveTowards(background[0].transform.position, new Vector3(-22f, 0, 10), Time.deltaTime);
        background[1].transform.position = Vector3.MoveTowards(background[1].transform.position, new Vector3(-22f, 0, 10), Time.deltaTime);
        background[2].transform.position = Vector3.MoveTowards(background[2].transform.position, new Vector3(-22f, 0, 10), Time.deltaTime);
        for (int i=0;i<3;i++)
        {
            if (background[i].transform.position.x <= -21.5f)
            {
                Debug.Log("a");
                background[i].transform.position = new Vector3(43,0,10);
            }
        }
    }
}
