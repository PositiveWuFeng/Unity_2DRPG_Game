using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Task[] tasks;

    public int killAmount;

    private static TaskManager instance;

    public static TaskManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TaskManager>();
            }
            return instance;
        }
    }

    string jsonpath = Application.streamingAssetsPath + "/jsontest.json";

    private void Start()
    {
        StartCoroutine(ReadJson());
    }

    /// <summary>
    /// 接受任务
    /// </summary>
    public Task AcceptTask()
    {
        for(int i=0;i<tasks.Length;i++)
        {
            if (!tasks[i].isFinished)
            {
                return tasks[i];
            }
        }
        return null;
    }

    public void UpdateTask()
    {
        if(!tasks[AcceptTask().taskID].isOn)
        {
            return;
        }
        if(TaskView.MyInstance.TaskKind(AcceptTask()) ==0)
        {
            killAmount++;
            if (killAmount == tasks[0].condition)
            {
                TaskView.MyInstance.ViewTask(true);
                killAmount = 0;
                tasks[AcceptTask().taskID].isFinished = true;
                tasks[AcceptTask().taskID].isOn = false;
            }
            else
            {
                TaskView.MyInstance.ViewTask();
            }
        }
        else if(TaskView.MyInstance.TaskKind(AcceptTask()) == 1)
        {
            if (killAmount == tasks[1].condition)
            {
                TaskView.MyInstance.ViewTask(true);
                killAmount = 0;
                return;
            }
            if (killAmount != tasks[1].condition)
            {
                TaskView.MyInstance.ViewTask();
            }
        }
    }
    /// <summary>
    /// 读取Json
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
        Debug.Log(jsondata.task.Count);
        //只输出不为空的
        for (int i = 0; i < jsondata.task.Count; i++)
        {
            tasks[i].isOn= jsondata.task[i].isOn;
            tasks[i].isFinished= jsondata.task[i].isFinished;
        }
        killAmount = jsondata.killAmount;

        if (killAmount > 0)
        {
            killAmount--;
        }
        UpdateTask();
    }
}
