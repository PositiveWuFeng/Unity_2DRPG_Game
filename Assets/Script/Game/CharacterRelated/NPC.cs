using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Button button;

    public Text text;

    public CanvasGroup sayMenu;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }
    private void OnMouseDown()
    {
        if (!TaskManager.MyInstance.tasks[TaskManager.MyInstance.AcceptTask().taskID].isOn)
        {
            if (TaskManager.MyInstance.AcceptTask() != null)
            {
                sayMenu.alpha = 1;
                sayMenu.blocksRaycasts = true;
                text.text = TaskManager.MyInstance.AcceptTask().description;
            }
        }
    }

    public void OnClick()
    {
        sayMenu.alpha = 0;
        sayMenu.blocksRaycasts = false;
        TaskManager.MyInstance.tasks[TaskManager.MyInstance.AcceptTask().taskID].isOn = true;
        TaskView.MyInstance.ViewTask();
    }

}
