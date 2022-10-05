using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    public static EventManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<EventManager>();
            }
            return instance;
        }
    }

    public Dictionary<string, UnityAction> actionDic=new Dictionary<string, UnityAction>();

    /// <summary>
    /// ����¼�
    /// </summary>
    public void AddEventListener(string name,UnityAction action)
    {
        if(actionDic.ContainsKey(name))
        {
            actionDic[name]+=action;
        }
        else
        {
            actionDic.Add(name, action);
        }
    }

    /// <summary>
    /// �Ƴ��¼�
    /// </summary>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (actionDic.ContainsKey(name))
        {
            actionDic[name] -= action;
        }
    }

    /// <summary>
    /// �����¼�
    /// </summary>
    public void TriggerEventListener(string name)
    {
        if (actionDic.ContainsKey(name))
        {
            actionDic[name]?.Invoke();
        }
    }

    /// <summary>
    /// ����¼�
    /// </summary>
    public void Clean()
    {
        actionDic.Clear();
    }
}
