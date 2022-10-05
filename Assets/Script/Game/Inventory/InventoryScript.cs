using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryScript : MonoBehaviour
{
    string jsonpath = Application.streamingAssetsPath + "/jsontest.json";

    public Inventory inventory;

    private int index;

    public Image itemImage;//道具

    [SerializeField]
    private GameObject amountTextPrefab;

    private GameObject amountText;

    private void Awake()
    {
        itemImage = GetComponentsInChildren<Image>()[0];
    }

    private void Start()
    {
        inventory = new Inventory();
        itemImage.gameObject.GetComponent<Button>().onClick.AddListener(OnClickButton);
        GetIndex();
        StartCoroutine(ReadJson());
    }

    private void GetIndex()
    {
        if(gameObject.name.Length==5)
        {
            index = Convert.ToInt32(gameObject.name[4]) - 49;
            return;
        }
        if (gameObject.name.Length == 6 && (Convert.ToInt32(gameObject.name[5]) - 49) == -1 && (Convert.ToInt32(gameObject.name[4]) - 49) == 0)
        {
            index = 9;
            return;
        }
        if((Convert.ToInt32(gameObject.name[4]) - 49)==0)
        {
            index = 10;
            index += (Convert.ToInt32(gameObject.name[5]) - 49);
            return;
        }
        if ((Convert.ToInt32(gameObject.name[4]) - 49) == 1 && gameObject.name.Length == 6 && (Convert.ToInt32(gameObject.name[5]) - 49)==-1)
        {
            index = 19;
        }
        else if ((Convert.ToInt32(gameObject.name[4]) - 49) == 1 && gameObject.name.Length == 6)
        {
            index = 20;
            index += (Convert.ToInt32(gameObject.name[5]) - 49);
        }
        else if ((Convert.ToInt32(gameObject.name[4]) - 49) == 2)
        {
            index = 29;
        }
    }

    /// <summary>
    /// 读取Json
    /// </summary>
    IEnumerator ReadJson()
    {
        yield return new WaitForSeconds(1f);
        if(!File.Exists(jsonpath))
        {
            jsonpath = Application.streamingAssetsPath + "/jsontest1.json";
        }
        string json = File.ReadAllText(jsonpath);
        Archive jsondata = new Archive();
        jsondata = JsonUtility.FromJson<Archive>(json);
        inventory.isNull = jsondata.inventory[index].isNull;
        //只输出不为空的
        if (!jsondata.inventory[index].isNull)
        {
            inventory.ItemID = jsondata.inventory[index].ItemID;
            inventory.amount = jsondata.inventory[index].amount;

            DisplayItem();
        }
        CreateText();
    }
    /// <summary>
    /// 显示道具
    /// </summary>
    private void DisplayItem()
    {
        if(inventory.ItemID!=-1)
        {
            itemImage.sprite = ItemManager.MyInstance.items[inventory.ItemID].icon;
            itemImage.color = Color.white;
        }
    }
    /// <summary>
    /// 生成道具数量Text
    /// </summary>
    private void CreateText()
    {
        amountText = Instantiate(amountTextPrefab, itemImage.transform);
        amountText.transform.localPosition = new Vector2(21.5f, -18.75f);
        amountText.GetComponent<Text>().text = inventory.amount.ToString();
    }
    /// <summary>
    /// 更新数量
    /// </summary>
    public void UpdateText()
    {
        amountText.GetComponent<Text>().text = inventory.amount.ToString();
    }
    /// <summary>
    /// 更新图标
    /// </summary>
    public void UpdateSprite()
    {
        inventory.ItemID++;
        itemImage.sprite = SpriteManager.MyInstance.sprites[inventory.ItemID].sprite;
        itemImage.color = SpriteManager.MyInstance.sprites[inventory.ItemID].color;
        inventory.ItemID--;
    }
    public void OnClickButton()
    {
        if(!inventory.isNull)
        {
            InventoryManager.MyInstance.MoveInventory(index);
        }
    }
}
