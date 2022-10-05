using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stat : MonoBehaviour
{
    [SerializeField]
    public Image HP;
    [SerializeField]
    public Image MP;

    public float MyHPValue
    {
        get
        {
            return HP.fillAmount * 100;
        }
        set
        {
            if (value > 100)
            {
                HP.fillAmount = 1;
            }
            else if (value < 0)
            {
                HP.fillAmount = 0;
            }
            else
            {
                HP.fillAmount = value / 100;
            }
        }
    }
    public float MyMPValue
    {
        get
        {
            return MP.fillAmount * 100;
        }
        set
        {
            if (value > 100)
            {
                MP.fillAmount = 1;
            }
            else if (value < 0)
            {
                MP.fillAmount = 0;
            }
            else
            {
                MP.fillAmount = value / 100;
            }
        }
    }

}
