using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup healthGroup;

    [SerializeField]
    private Stat HP;

    private float t = 1;

    private Coroutine disappear;

    private Coroutine colorCor;

    private SpriteRenderer spriteRenderer;

    private GameObject player;

    private float speed = 2.0f;

    private Rigidbody2D myRigidbody;

    private Animator animator;
    //是否死亡
    public bool isDie;

    EventManager action = new EventManager();

    private void Start()
    {
        HP.MyHPValue = 100;
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        action.AddEventListener("Die", TaskManager.MyInstance.UpdateTask);
        action.AddEventListener("Die", LevelScript.MyInstance.RiseExp);
    }
    private void Update()
    {
        EnemyMove();
        DestoryEnemy();
        Die();
    }
    private void Hurt(Collider2D collision)
    {
        healthGroup.alpha = 1;
        spriteRenderer.color = Color.red;
        if (colorCor != null)
        {
            StopCoroutine(colorCor);
        }
        colorCor = StartCoroutine(ColorDisappear());
        //击退方向及距离
        Vector2 vector = (collision.transform.position - transform.position).normalized;
        transform.DOMoveX((transform.position.x - vector.x), 0.75f);
        transform.DOMoveY((transform.position.y - vector.y), 0.75f);
    }
    private void Die()
    {
        if(!isDie)
        {
            if (HP.MyHPValue == 0)
            {
                action.TriggerEventListener("Die");
                isDie = true;
                myRigidbody.velocity = Vector2.zero;
                StartCoroutine(DieIEnu());
            }
        }
    }
    private void EnemyMove()
    {
        if (isDie != true)
        {
            Vector3 direction = player.transform.position - transform.position;
            myRigidbody.velocity = direction.normalized * speed;
            animator.SetBool("isMove", true);
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
            animator.SetBool("isMove", false);
        }
    }
    private IEnumerator GroupDisappear()
    {
        yield return new WaitForSeconds(5);
        healthGroup.alpha = 0;
    }
    private IEnumerator ColorDisappear()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }
    private IEnumerator DieIEnu()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 255);
            yield return new WaitForSeconds(0.2f);
        }
        RandomDrop();
        GameObject.Destroy(gameObject);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie == false)
        {
            if (collision.tag == "Spell")
            {
                HP.MyHPValue -= SpellBook.MyInstance.GetDamage(collision.name);
                Hurt(collision);
            }
        }
        Die();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDie == false)
        {
            if (collision.tag == "Spell")
            {
                if (collision.name == "EnergyPoint")
                {
                    t += Time.deltaTime;
                    if (t >= 1)
                    {
                        HP.MyHPValue -= SpellBook.MyInstance.GetDamage(collision.name);
                        t = 0;
                    }
                    Hurt(collision);
                    Die();
                }
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Die();
        if (disappear != null)
        {
            StopCoroutine(disappear);
        }
        disappear = StartCoroutine(GroupDisappear());
    }
    /// <summary>
    /// 销毁物体
    /// </summary>
    public void DestoryEnemy()
    {
        if (!Player.MyInstance.isPlay)
        {
            GameObject.Destroy(gameObject);
        }
    }

    /// <summary>
    /// 随机掉落子弹或者草莓
    /// </summary>
    public void RandomDrop()
    {
        //50%，
        int num = Random.Range(1, 10);
        if (num  == 1)
        {
            Instantiate(PrafabManager.MyInstance.perfabs[0],transform.position,Quaternion.identity);
        }
        if (num  == 2)
        {
            Instantiate(PrafabManager.MyInstance.perfabs[1], transform.position, Quaternion.identity);
        }

    }
}
