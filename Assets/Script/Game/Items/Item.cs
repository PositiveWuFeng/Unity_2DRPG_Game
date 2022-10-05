using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public int ItemID;
    public string Name;
    public string Description="";
    public Sprite icon;
    public int HP = 0;
    public int MP = 0;
}
