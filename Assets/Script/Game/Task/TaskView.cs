using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskView : MonoBehaviour
{
    public Text description;

    public Text taskTarget;

    private static TaskView instance;

    public static TaskView MyInstance
    {
        get
        {
            if(instance==null)
            {
                instance = FindObjectOfType<TaskView>();
            }
            return instance;
        }
    }

    public void ViewTask()
    {
        description.text = TaskManager.MyInstance.AcceptTask().description;
        string temp;
        temp = TaskManager.MyInstance.AcceptTask().taskTarger;
        if(TaskKind(TaskManager.MyInstance.AcceptTask())==0)
        {
            temp = (TaskManager.MyInstance.killAmount).ToString() + "/500";
        }
        if (TaskKind(TaskManager.MyInstance.AcceptTask()) == 1)
        {
            temp = (LevelScript.MyInstance.level).ToString() + "/5";
        }
        taskTarget.text = temp;
    }
    public void ViewTask(bool b)
    {
        description.text = "";
        taskTarget.text = "";
    }
    /// <summary>
    /// 分类 0杀怪 1等级
    /// </summary>
    public int TaskKind(Task task)
    {
        switch (task.taskID)
        {
            case 0:
                return 0;
            case 1:
                return 1;   
        }
        return -1;
    }
}
