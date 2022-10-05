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
    /// 添加事件
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
    /// 移除事件
    /// </summary>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (actionDic.ContainsKey(name))
        {
            actionDic[name] -= action;
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    public void TriggerEventListener(string name)
    {
        if (actionDic.ContainsKey(name))
        {
            actionDic[name]?.Invoke();
        }
    }

    /// <summary>
    /// 清空事件
    /// </summary>
    public void Clean()
    {
        actionDic.Clear();
    }
}
