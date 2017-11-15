using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface IGenericTask
{
    void InitTask(string[] taskInfo, Database db);
    void StartTask();
    void DestroyTask();
    void SetListener(Action action);
}


public class GenericTask : MonoBehaviour {
    
    private IGenericTask gTask;
    private Database db;
    private int taskAmount;
    private int currentTask;
    private string[] taskInfo;

    public void InitTask(string[] taskInfo)
    {
        db = GameObject.FindObjectOfType<Database>();
        this.taskInfo = taskInfo;
        int taskAmount = Int32.Parse(taskInfo[5]);
        currentTask = 0;
        NextTask(currentTask);
    }

    private void NextTask(int taskType)
    {
        switch (taskType)
        {
            case 0:
                gTask = gameObject.AddComponent(typeof(TaskShowServiceType)) as TaskShowServiceType;
                gTask.InitTask(taskInfo, db);
                gTask.SetListener(ActionDone);
                break;
            case 1:
                gTask = gameObject.AddComponent(typeof(TaskFindComponent)) as TaskFindComponent;
                gTask.InitTask(taskInfo, db);
                gTask.SetListener(ActionDone);
                break;
            case 2:
                break;
            case 3:
                break;
        };
    }

    private void ActionDone()
    {
        if (currentTask <= taskAmount)
        {
            currentTask++;
            taskInfo = db.GetTask(currentTask);
            NextTask(Int32.Parse(taskInfo[2]));
        }
    }

    public void StartTask()
    {
            gTask.StartTask();
    }
}

