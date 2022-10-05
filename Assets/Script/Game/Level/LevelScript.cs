using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    private static LevelScript instance;

    public static LevelScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<LevelScript>();
            }
            return instance;
        }
    }

    public Text levelText;

    public Text expText;

    public Image xpBar;

    public int level=1;//当前等级

    public float currentExp=0;//当前exp

    public float increaseExp;//杀怪增长exp

    public float[] MaxExps;//每级最大exp

    private SpellBook spellBook;

    private void Start()
    {
        ReadArchive();
        spellBook = SpellBook.MyInstance;
    }

    /// <summary>
    /// 读档
    /// </summary>
    public void ReadArchive()
    {

    }

    public void RiseExp()
    {
        if (currentExp < MaxExps[9])
        {
            currentExp += increaseExp;
            if (currentExp >= MaxExps[level-1] && level != 10)
            {
                RiseLevel();
            }
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        levelText.text = level.ToString();
        expText.text=currentExp.ToString()+"/"+MaxExps[level-1].ToString();

        xpBar.fillAmount = currentExp / MaxExps[level-1];
    }
    /// <summary>
    /// 升级
    /// </summary>
    private void RiseLevel()
    {
        level++;
        for (int i=0;i< spellBook.spells.Length;i++)
        {
            spellBook.spells[i].MyDamage += (spellBook.spells[i].MyDamage/10);
            spellBook.spells[i].MyCastTime -= (spellBook.spells[i].MyCastTime*0.03f);
        }
    }
}
