using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpellScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField]
    private float speed;

    private Vector3 target;

    private float t;//Ïú»ÙÊ±¼ä

    private Player player;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Start()
    {
        transform.position=new Vector3(transform.position.x, transform.position.y, 0);
        name = name.Remove(name.Length - 7, 7);
        Vector2 direction = new Vector2();
        if (Player.isAttack)
        {
            direction = target - transform.position;
        }
        else
        {
            direction = Player.spellPos;
        }
        myRigidbody.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        t += Time.deltaTime;
        if(this.name=="Bomb" && t>=2)
        {
            Instantiate(player.spellPrefab[3], transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }
        if(t>=3.5f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        
    }

    /*private void OnTriggerEnter2D(Collider collision)
    {
        if(collision.tag == "Enemy")
        {

        }
    }*/

}
