using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Data;

public class UIControl : MonoBehaviour
{
    private Sprite[] sprites;//存储music按钮的两个sprite
    public Button music_btn;//音乐
    public Button start_btn;//开始按钮
    public AudioSource bgAudio;
    public GameObject bush1;//草丛1
    public GameObject bush2;//草丛2
    public bool isStart;//是否点击开始按钮

    public GameObject user_title;//用户名
    public GameObject password_title;//密码

    public InputField input_password;
    public InputField input_user;

    public GameObject login_menu;
    public GameObject load;
    public Button login_btn;
    public GameObject mistake;

    private float t1;//记录时间
    private float t2;//记录加载时间

    SqlManage sql = new SqlManage();
    private void Awake()
    {
        sprites = new Sprite[2];
        sprites[0] = Resources.LoadAll<Sprite>("UI/Iconic1024x1024")[5];
        sprites[1] = Resources.LoadAll<Sprite>("UI/Iconic1024x1024")[17];

    }
    private void Start()
    {
        user_title.transform.localScale=Vector3.zero;
        password_title.transform.localScale = Vector3.zero;
        start_btn.transform.localScale = Vector3.zero;
        login_btn.transform.localScale = Vector3.zero;
        mistake.SetActive(false);
        login_btn.onClick.AddListener(OnLoginBtn);
    }

    private void Update()
    {
        if (isStart)
        {
            t1 += Time.deltaTime;
            Debug.Log("1");
            if (t1 > 1f)
            {
                Debug.Log("2");
                DontDestroyOnLoad(bgAudio);
                SceneManager.LoadSceneAsync(1);
            }
        }
        t2 += Time.deltaTime;
        if(t2>=5 && t2<=7)
        {
            load.GetComponent<Image>().DOColor(new Color(load.GetComponent<Image>().color.r, load.GetComponent<Image>().color.g, load.GetComponent<Image>().color.b, 0), 2);
        }
        if(t2>=7.1f && t2<=9.2f)
        {
            user_title.transform.DOScale(1, 2);
            password_title.transform.DOScale(1, 2);
            login_btn.transform.DOScale(1, 2);
        }
    }

    public void OnStartBtn()
    {
        start_btn.gameObject.SetActive(false);
        music_btn.gameObject.SetActive(false);
        bush1.transform.DOMove(new Vector3(-5, bush1.transform.position.y, 0), 1);
        bush2.transform.DOMove(new Vector3(5, bush2.transform.position.y, 0), 1);
        isStart = true;
    }
    public void OnMusicBtn()
    {
        if (music_btn.transform.GetComponent<Image>().sprite == sprites[0])
        {
            music_btn.transform.GetComponent<Image>().sprite = sprites[1];
            bgAudio.volume = 0;
        }
        else
        {
            music_btn.transform.GetComponent<Image>().sprite = sprites[0];
            bgAudio.volume = 0.5f;
        }
    }
    public void OnLoginBtn()
    {
        if(sql.SqlSelect(input_user.text).password==(input_password.text))
        {
            login_menu.transform.DOScale(0, 2);
            StartCoroutine(Wait(2f));
        }
        else
        {
            mistake.SetActive(true);
        }
    }
    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
        start_btn.transform.DOScale(1, 2);
    }
}
