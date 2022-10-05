using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup keybindMenu;

    [SerializeField]
    private CanvasGroup spellBook;

    [SerializeField]
    private CanvasGroup inventoryMenu;

    private GameObject[] keybindButtons;

    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private ActionButton[] actionButtons;

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }
    private void Start()
    {

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(inventoryMenu.alpha == 0 && spellBook.alpha == 0)
            {
                OpenClose(keybindMenu);
            }
            CloseMenu();
            
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            OpenClose(spellBook);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            OpenClose(inventoryMenu);
        }
    }

    public void UpdataKeyText(string key,KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x=>x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void CloseMenu()
    {
        spellBook.alpha = 0;
        spellBook.blocksRaycasts = false;
        inventoryMenu.alpha = 0;
        inventoryMenu.blocksRaycasts = false;
    }
}
