using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMountComponent :MonoBehaviour, IGenericTask {

    public TaskMountComponent(Database db)
    {

    }
    public void InitTask(string[] taskInfo, Database db)
    {

    }


    public void StartTask()
    {
        Debug.Log("ShowServiceType");
    }

    public void DestroyTask()
    {
        Destroy(this);
    }

    public Action GetAction()
    {
        throw new NotImplementedException();
    }

    public void SetListener(Action action)
    {
        throw new NotImplementedException();
    }
}
