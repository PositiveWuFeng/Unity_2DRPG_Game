using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Discovery.Configuration;
using UnityEngine;

public class KeybindManager : MonoBehaviour
{
    private static KeybindManager instance;

    //public static KeybindManager Instance;

    public static KeybindManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<KeybindManager>();
            }
            return instance;
        }
    }

    public Dictionary<string, KeyCode> Keybinds { get; set; }

    public Dictionary<string, KeyCode> ActionBinds { get; set; }

    private string bindName;

    private void Start()
    {
        Keybinds = new Dictionary<string, KeyCode>();

        ActionBinds = new Dictionary<string, KeyCode>();

        BindKey("Up", KeyCode.W);
        BindKey("Down", KeyCode.S);
        BindKey("Left", KeyCode.A);
        BindKey("Right", KeyCode.D);

        BindKey("Skill1", KeyCode.Q);
        BindKey("Skill2", KeyCode.E);
        BindKey("Skill3", KeyCode.R);
    }

    public void BindKey(string key,KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = Keybinds;

        if(key.Contains("ACT"))
        {
            currentDictionary = ActionBinds;
        }

        if(!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keyBind);
            UIManager.MyInstance.UpdataKeyText(key, keyBind);
        }
        else if(currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;

            currentDictionary[myKey] = KeyCode.None;

            UIManager.MyInstance.UpdataKeyText(key, KeyCode.None);
        }

        currentDictionary[key] = keyBind;
        UIManager.MyInstance.UpdataKeyText(key, keyBind);
        bindName = string.Empty;
    }

    public void KeyBindOnClick(string bindName)
    {
        this.bindName = bindName;
    }

    private void OnGUI()
    {
        if(bindName!=string.Empty)
        {
            Event e = Event.current;

            if(e.isKey)
            {
                BindKey(bindName, e.keyCode);
            }
        }
    }
}
