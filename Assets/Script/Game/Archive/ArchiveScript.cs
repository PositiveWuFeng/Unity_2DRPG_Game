using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ArchiveScript : MonoBehaviour
{
    private static ArchiveScript instance;

    public static ArchiveScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ArchiveScript>();
            }
            return instance;
        }
    }

    string jsonpath;

    private Button button;

    Archive jsondata;

    public ActionButton[] actionButtons;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        jsonpath = Application.streamingAssetsPath + "/jsontest.json";
        button.onClick.AddListener(WriteJson);
    }

    /// <summary>
    /// дjson
    /// </summary>
    public void WriteJson()
    {
        if (!File.Exists(jsonpath))
        {
            File.Create(jsonpath);
        }
        jsondata = new Archive();
        AddAll();
        string json = JsonUtility.ToJson(jsondata, true);
        File.WriteAllText(jsonpath, json);

    }

    private void AddAll()
    {
        AddInventory();
        AddTask();
        AddSpell();
        AddActionButton();
        AddPlayer();
    }

    public void AddInventory()
    {
        jsondata.inventory = new List<Inventory>();
        InventoryScript[] slots = InventoryManager.MyInstance.slots;
        for(int i=0;i<slots.Length;i++)
        {
            jsondata.inventory.Add(slots[i].inventory);
        }
    }

    public void AddTask()
    {
        jsondata.task = new List<Task>();
        for (int i = 0; i < TaskManager.MyInstance.tasks.Length; i++)
        {
            jsondata.task.Add(TaskManager.MyInstance.tasks[i]);
        }
    }

    public void AddSpell()
    {
        jsondata.spell = new List<Spell>();
        Spell[] temp = SpellBook.MyInstance.spells;
        for (int i = 0; i < temp.Length; i++)
        {
            jsondata.spell.Add(temp[i]);
        }
    }

    public void AddActionButton()
    {
        jsondata.actionBtn = new string[3];
        for (int i = 0; i < 3; i++)
        {
            jsondata.actionBtn[i] = actionButtons[i].MyIcon.name;
        }
    }

    public void AddPlayer()
    {
        jsondata.x = Player.MyInstance.gameObject.transform.position.x;
        jsondata.y = Player.MyInstance.gameObject.transform.position.y;
        jsondata.HP = Player.MyInstance.state.MyHPValue;
        jsondata.MP = Player.MyInstance.state.MyMPValue;
        jsondata.level = LevelScript.MyInstance.level;
        jsondata.currentExp = LevelScript.MyInstance.currentExp;
        jsondata.killAmount = TaskManager.MyInstance.killAmount;
    }
}
