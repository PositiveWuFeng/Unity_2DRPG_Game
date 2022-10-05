using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public InventoryScript[] slots= new InventoryScript[30];
    //slots 两个父
    public GameObject[] gb = new GameObject[2];

    private static InventoryManager instance;

    public static InventoryManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
            }
            return instance;
        }
    }

    private void Start()
    {
        GetInventoryScript();
        AddScript();
    }
    private void Update()
    {
        GetInputFastBar();
    }
    /// <summary>
    /// 获取组件
    /// </summary>
    private void GetInventoryScript()
    {
        InventoryScript[] temp1 = new InventoryScript[24];
        InventoryScript[] temp2 = new InventoryScript[6];
        temp1 = gb[0].GetComponentsInChildren<InventoryScript>();
        temp2 = gb[1].GetComponentsInChildren<InventoryScript>();
        for (int i=0;i<24;i++)
        {
            slots[i] = temp1[i];
        }
        for(int i=24;i<30;i++)
        {
            slots[i] = temp2[i - 24];
        }
    }
    /// <summary>
    /// 移动
    /// </summary>
    public void MoveInventory(int index)
    {
        //点击的是插槽
        if(index>=24&&index<=29)
        {
            for (int i = 0; i < 24; i++)
            {
                 if(slots[i].inventory.isNull)
                {
                    SwapImage(i, index);
                    return;
                }
            }
        }
        else
        {
            for (int i = 24; i < 30; i++)
            {
                if (slots[i].inventory.isNull)
                {
                    SwapImage(i, index);
                    return;
                }
            }
        }
    }

    private void SwapImage(int i,int index)
    {
        Sprite sprite = slots[i].itemImage.sprite;
        slots[i].itemImage.sprite = slots[index].itemImage.sprite;
        slots[index].itemImage.sprite = sprite;

        slots[index].itemImage.color = new Color(156/255f, 156/255f, 86/255f);
        slots[i].itemImage.color = Color.white;

        slots[i].inventory.isNull = false;
        slots[i].inventory.amount = slots[index].inventory.amount;
        slots[i].inventory.ItemID = slots[index].inventory.ItemID;

        slots[index].inventory.isNull = true;
        slots[index].inventory.amount = 0;
        slots[index].inventory.ItemID = -1;

        slots[index].UpdateText();
        slots[i].UpdateText();
    }

    private void AddScript()
    {
        for(int i=0;i<30;i++)
        {
            slots[i].itemImage.AddComponent<MiddleButton>();
        }
    }
    /// <summary>
    /// 快捷栏按键
    /// </summary>
    private void GetInputFastBar()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(slots[24].name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(slots[25].name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(slots[26].name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(slots[27].name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseItem(slots[28].name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UseItem(slots[29].name);
        }
    }
    public void AddItem(string name)
    {
        for (int i = 0; i < 30; i++)
        {
            if (name == "RedBottle" && slots[i].inventory.ItemID == 0)
            {
                if (slots[i].inventory.amount != 5)
                {
                    slots[i].inventory.amount++;
                    slots[i].UpdateText();
                    slots[i].UpdateSprite();
                    return;
                }
            }
            if (name == "BlueBottle" && slots[i].inventory.ItemID == 1)
            {
                if (slots[i].inventory.amount != 5)
                {
                    slots[i].inventory.amount++;
                    slots[i].UpdateText();
                    slots[i].UpdateSprite();
                    return;
                }
            }
        }
        for (int i = 0; i < 30; i++)
        {
            if (slots[i].inventory.isNull)
            {
                slots[i].inventory.isNull = false;
                slots[i].inventory.amount = 1;
                if (name == "RedBottle")
                {
                    slots[i].inventory.ItemID = 0;
                }
                else
                {
                    slots[i].inventory.ItemID = 1;
                }
                slots[i].UpdateText();
                slots[i].UpdateSprite();
                return;
            }
        }
    }

    /// <summary>
    /// 使用道具
    /// </summary>
    public void UseItem(string name)
    {
        int index = GetIndex(name);
        if (!slots[index].inventory.isNull)
        {
            Debug.Log(name+" "+index);
            Player.MyInstance.state.MyHPValue += ItemManager.MyInstance.items[slots[index].inventory.ItemID].HP;
            Player.MyInstance.state.MyMPValue += ItemManager.MyInstance.items[slots[index].inventory.ItemID].MP;

            slots[index].inventory.amount--;
            slots[index].UpdateText();
            if (slots[index].inventory.amount == 0)
            {
                slots[index].inventory.ItemID = -1;
                slots[index].inventory.isNull = true;
                slots[index].itemImage.sprite = SpriteManager.MyInstance.sprites[0].sprite;
                slots[index].itemImage.color = new Color(156 / 255f, 156 / 255f, 86 / 255f);
            }
        }
    } 

    private int GetIndex(string name)
    {
        int index = 0;
        if (name.Length == 5)
        {
            index = Convert.ToInt32(name[4]) - 49;
            return index;
        }
        if (name.Length == 6 && (Convert.ToInt32(name[5]) - 49) == -1 && (Convert.ToInt32(name[4]) - 49) == 0)
        {
            index = 9;
            return index;
        }
        if ((Convert.ToInt32(name[4]) - 49) == 0)
        {
            index = 10;
            index += (Convert.ToInt32(name[5]) - 49);
            return index;
        }
        if ((Convert.ToInt32(name[4]) - 49) == 1 && name.Length == 6 && (Convert.ToInt32(name[5]) - 49) == -1)
        {
            index = 19;
        }
        else if ((Convert.ToInt32(name[4]) - 49) == 1 && name.Length == 6)
        {
            index = 20;
            index += (Convert.ToInt32(name[5]) - 49);
        }
        else if ((Convert.ToInt32(name[4]) - 49) == 2)
        {
            index = 29;
        }
        return index;
    }
}
