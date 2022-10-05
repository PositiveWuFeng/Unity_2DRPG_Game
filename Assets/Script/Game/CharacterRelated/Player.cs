using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;

public class Player : Character
{
    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    public Stat state;//玩家状态

    [SerializeField]
    public GameObject[] spellPrefab;

    [SerializeField]
    private Block[] blocks;

    public static Vector2 spellPos;

    public Transform Mytarget { get; set; }

    private int exitIndex;

    private float skillCD=1f;

    public static bool isAttack;

    Spell newSpell;

    private SpriteRenderer spriteRenderer;

    private Coroutine colorCor;
    //是否进入副本
    public bool isPlay=false;

    string jsonpath = Application.streamingAssetsPath + "/jsontest.json";

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        state = GetComponent<Stat>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state.MyHPValue = 50;
        state.MyMPValue = 50;

        StartCoroutine(ReadJson());
    }

    protected override void Update()
    {
        GetInput();
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    /// <summary>
    /// 根据键盘输入
    /// </summary>
    private void GetInput()
    {
        PlayerMove();
       // ReleaseSpell();

    }
    private void PlayerMove()
    {
        direction = Vector2.zero;
        if(Input.GetKeyDown(KeyCode.P))
        {
            this.transform.position = new Vector3(-7.12f, 4, 0);
            isPlay = false;
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["Up"]))
        {
            exitIndex = 0;
            direction += Vector2.up;
            spellPos = direction;
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["Down"]))
        {
            exitIndex = 1;
            direction += Vector2.down;
            spellPos = direction;
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["Left"]))
        {
            exitIndex = 2;
            direction += Vector2.left;
            spellPos = direction;
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["Right"]))
        {
            exitIndex = 3;
            direction += Vector2.right;
            spellPos = direction;
        }
        foreach (string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }
    }
    public void InputSpell(string spellName)
    {
        Block();
        if (!isAttacking && !IsMoving && state.MyMPValue != 0)
        {
            attackCoroutine = StartCoroutine(Attack(spellName));
        }
    }
    public IEnumerator Attack(string spellName)
    {
        newSpell = SpellBook.MyInstance.CastSpell(spellName);


        isAttacking = true;

        myAnimator.SetBool("attack", isAttacking);
        yield return new WaitForSeconds(newSpell.MyCastTime * skillCD);
        CastSpell(spellName);
        StopAttack();

    }

    public void CastSpell(string spellName)
    {
        if (spellName == "FlyCutter")
        {
            state.MyMPValue -= SpellBook.MyInstance.spells[0].MyMP;
            isAttack = InLineOfSight();

            Vector3 pos;

            pos = new Vector3(transform.position.x + spellPos.x * 1.2f, transform.position.y + spellPos.y * 1.2f, 0);
            Instantiate(newSpell.MySpellPrefab, pos, Quaternion.identity);
        }
        else if (spellName == "Thunder")
        {
            state.MyMPValue -= SpellBook.MyInstance.spells[1].MyMP;
            isAttack = InLineOfSight();

            Vector3 pos;

            pos = new Vector3(transform.position.x + spellPos.x * 1.2f, transform.position.y + spellPos.y * 1.2f, 0);
            Instantiate(newSpell.MySpellPrefab, pos, Quaternion.identity);
        }
        else if (spellName == "Bomb")
        {
            state.MyMPValue -= SpellBook.MyInstance.spells[2].MyMP;
            Instantiate(newSpell.MySpellPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
    }

    public bool InLineOfSight()
    {
        Vector3 targetDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)),256);

        if(hit.collider == null)
        {
            return true;
        }

        return false;
    }

    private void Block()
    {
        foreach(Block b in blocks)
        {
            b.Deactivate();
        }
        blocks[exitIndex].Activate();
    }

    public override void StopAttack()
    {
        SpellBook.MyInstance.StopCasting();

        base.StopAttack();
    }

    private void Hurt()
    {
        //受伤动画
        spriteRenderer.color = Color.red;
        if (colorCor != null)
        {
            StopCoroutine(colorCor);
        }
        colorCor = StartCoroutine(ColorDisappear());
    }

    private void Die()
    {
        if (state.MyHPValue == 0)
        {
            myRigidbody.velocity = Vector2.zero;
            this.transform.position = new Vector3(-7.12f, 4, 0);
            state.MyHPValue = 100;
            state.MyMPValue = 100;
            isPlay = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            state.MyHPValue -= 10;
            GameObject.Destroy(collision.gameObject);
            Hurt();
        }
        if(collision.name=="TP1")
        {
            transform.position = new Vector3(125, -8, 0);
            isPlay = true;
        }
        Die();
    }

    private IEnumerator ColorDisappear()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
        isHurt = false;
    }

    /// <summary>
    /// 读取Json
    /// </summary>
    IEnumerator ReadJson()
    {
        yield return new WaitForSeconds(1f);
        if (!File.Exists(jsonpath))
        {
            jsonpath = Application.streamingAssetsPath + "/jsontest1.json";
        }
        string json = File.ReadAllText(jsonpath);
        Archive jsondata = new Archive();
        jsondata = JsonUtility.FromJson<Archive>(json);

        LevelScript.MyInstance.level = jsondata.level;
        LevelScript.MyInstance.currentExp = jsondata.currentExp;
        state.MyHPValue = jsondata.HP;
        state.MyMPValue = jsondata.MP;

        transform.position = new Vector2(jsondata.x, jsondata.y);
    }

}

