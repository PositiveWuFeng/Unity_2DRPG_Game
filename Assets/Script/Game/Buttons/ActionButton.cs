using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ActionButton : MonoBehaviour,IPointerClickHandler
{
    public IUseable MyUseable;

    public Button MyButton;
    public Image MyIcon;

    [SerializeField]
    private Image icon;

    string jsonpath = Application.streamingAssetsPath + "/jsontest.json";

    void Start()
    {
        MyButton = GetComponent<Button>();
        StartCoroutine(ReadJson());
    }

    void Update()
    {
        GetInputSpell();
    }

    private void GetInputSpell()
    {
        if(MyUseable!=null)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds[this.gameObject.name]))
            {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        if(MyUseable != null)
        {
            MyUseable.Use();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(HandScript.MyInstance.MyMoveable!=null && HandScript.MyInstance.MyMoveable is IUseable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
            }
        }
    }

    public void SetUseable(IUseable useable)
    {
        this.MyUseable = useable;
         
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;
    }

    /// <summary>
    /// ∂¡»°Json
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

        switch (gameObject.name)
        {
            case "Skill1":
                MyUseable = SpellBook.MyInstance.spells[0];
                gameObject.name = jsondata.actionBtn[0];
                MyIcon.sprite = SpellBook.MyInstance.spells[0].MyIcon;
                break;
            case "Skill2":
                MyUseable = SpellBook.MyInstance.spells[1];
                gameObject.name = jsondata.actionBtn[1];
                MyIcon.sprite = SpellBook.MyInstance.spells[1].MyIcon;
                break;
            case "Skill3":
                MyUseable = SpellBook.MyInstance.spells[2];
                gameObject.name = jsondata.actionBtn[2];
                MyIcon.sprite = SpellBook.MyInstance.spells[2].MyIcon;
                break;
        }
        MyIcon.color = Color.white;
        

    }
}
