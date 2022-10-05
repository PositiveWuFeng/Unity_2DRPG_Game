using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Task
{
    public int taskID;
    public string title;
    public string description;
    public string taskTarger;
    public bool isFinished;
    public int condition;
    public bool isOn;
}
