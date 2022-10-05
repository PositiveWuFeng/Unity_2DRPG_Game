using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Spell:IUseable,IMoveable
{
    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject spellPrefab;

    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private int MP;

    public float MySpeed { get => speed; set => speed = value; }
    public float MyCastTime { get => castTime; set => castTime = value; }
    public GameObject MySpellPrefab { get => spellPrefab; set => spellPrefab = value; }
    public int MyDamage { get => damage; set => damage = value; }
    public Sprite MyIcon { get => icon; set => icon = value; }
    public int MyMP { get => MP; set => MP = value;}
    public string MyName { get => name; set => name = value; }

    public void Use()
    {
        Player.MyInstance.InputSpell(MyName);
    }
}