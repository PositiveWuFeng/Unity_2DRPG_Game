using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RegisterUIControl : MonoBehaviour
{
    public GameObject register_btn1;//登录界面的注册按钮
    public Button register_btn2;


    public InputField username;
    public InputField password;
    public InputField password2;

    public GameObject login_menu;

    SqlManage sql = new SqlManage();
    private void Awake()
    {

        username = GetComponentsInChildren<InputField>()[0];
        password = GetComponentsInChildren<InputField>()[1];
        password2 = GetComponentsInChildren<InputField>()[2];
    }
    private void Start()
    {
        this.gameObject.transform.localScale= Vector3.zero;
        register_btn1.transform.localScale=Vector3.zero;
        register_btn1.GetComponent<Button>().onClick.AddListener(RegisterBtn1);
        register_btn2.onClick.AddListener(RegisterBtn2);
        StartCoroutine(Wait(7.1f));
    }

    public void RegisterBtn1()
    {
        login_menu.transform.DOScale(0, 2);
        StartCoroutine(Wait(2f));
        this.gameObject.transform.DOScale(1, 2);
    }

    public void RegisterBtn2()
    {
        if(password.text!=password2.text)
        {
            return;
        }
        if(sql.SqlInsert(username.text,password.text))
        {
            this.gameObject.transform.DOScale(0, 2);
            StartCoroutine(Wait(2f));
            login_menu.transform.DOScale(1, 2);
        }
    }

    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        register_btn1.transform.DOScale(1, 2);
    }
}
