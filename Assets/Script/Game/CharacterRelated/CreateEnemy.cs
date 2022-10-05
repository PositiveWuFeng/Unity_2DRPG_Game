using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    //秒时计时器
    private float t;
    //分时计时器
    private float tMin;
    //每秒生成数量
    private int amount=2;


    private void Update()
    {
        GenerateEnemy();
    }

    private void GenerateEnemy()
    {
        if(Player.MyInstance.isPlay)
        {
            t += Time.deltaTime;
            tMin+=Time.deltaTime;

            if(t>=1f)
            {
                t = 0;
                for (int i=0;i<amount;i++)
                {
                    Instantiate(enemyPrefab, RandomPosition(), Quaternion.identity);
                }
            }
            if(tMin>=60f)
            {
                tMin = 0;
                amount++;
            }
        }
    }

    private Vector3 RandomPosition()
    {
        Vector2 vec2 = new Vector3();
        while (!(Vector2.Distance(Player.MyInstance.transform.position,vec2)>10 && Vector2.Distance(Player.MyInstance.transform.position, vec2) < 15))
        {
            //创建随机点
            vec2 = new Vector2(Player.MyInstance.transform.position.x + Random.Range(-20.0f, 20.0f), Player.MyInstance.transform.position.y + Random.Range(-20.0f, 20.0f));
        }
        return vec2;
    }
}
