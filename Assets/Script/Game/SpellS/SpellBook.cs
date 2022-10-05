using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    private static SpellBook instance;

    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text spellName;

    [SerializeField]
    private Text castingTime;

    [SerializeField]
    public Spell[] spells;

    private Coroutine spellRoutine;

    private Coroutine fadeRoutine;

    [SerializeField]
    private CanvasGroup canvasGroup;

    string jsonpath = Application.streamingAssetsPath + "/jsontest.json";

    private void Start()
    {
        StartCoroutine( ReadJson());
    }

    public Spell CastSpell(string spellName1)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName1);

        spellName.text = spell.MyName;

        castingBar.fillAmount = 0;

        spellRoutine = StartCoroutine(Progress(spell));

        fadeRoutine = StartCoroutine(FadeBar());


        return spell;
    }

    private IEnumerator Progress(Spell spell)
    {
        float timePassed = Time.deltaTime;

        float rate = 1 / spell.MyCastTime;

        float progress = 0;

        while(progress<=1.0)
        {
            castingBar.fillAmount = Mathf.Lerp(0,1,progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castingTime.text = (spell.MyCastTime - timePassed).ToString("F2");

            if(spell.MyCastTime - timePassed < 0)
            {
                castingTime.text = "0.00";
            }

            yield return null;
        }
        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        float rate = 1 / 0.5f;

        float progress = 0;

        while (progress <= 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    public void StopCasting()
    {
        if(fadeRoutine!=null)
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
            fadeRoutine = null;
        }

        if(spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }

    public float GetDamage(string name)
    {
        for(int i=0;i<spells.Length; i++)
        {
            if (name == spells[i].MyName)
            {
                return (float)spells[i].MyDamage;
            }
        }
        return 0;
    }

    public Spell GetSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);
        return spell;
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
        //只输出不为空的
        for(int i=0;i<jsondata.spell.Count;i++)
        {
            spells[i].MyCastTime=jsondata.spell[i].MyCastTime;
            spells[i].MyDamage=jsondata.spell[i].MyDamage;
        }
    }


}
