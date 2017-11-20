using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;


public class TaskShowServiceType : MonoBehaviour, IGenericTask
{

    private StartupScript startupScript;
    private GameObject ui;
    private EventSystem eventSystem;
    public Action TaskDoneAction;
    private Action listner;

    public void InitTask(string[] taskInfo, Database db)
    {
        ui = Instantiate(Resources.Load("ServiceUI") as GameObject);
        startupScript = ui.GetComponent<StartupScript>();
        startupScript.ButtonConfirmPressed += ConfirmClicked;
        startupScript.InitUI(taskInfo);

        int amount = Int32.Parse(taskInfo[5]);
        startupScript.SetServiceText("Amount of tasks: " + amount + "\n\n ");
        for(int i = 1; i <= amount; i++)
        {
            startupScript.SetServiceText("\t" + i + ": " + db.GetTask(i)[0] + "\n");
        }
        StartTask();
    }


    public void StartTask()
    {
        Debug.Log("ShowServiceType");
    }

    public void DestroyTask()
    {
        Destroy(gameObject.GetComponent<TaskShowServiceType>());
    }

    public void ConfirmClicked()
    {
        DestroyTask();
        startupScript.ButtonConfirmPressed -= ConfirmClicked;
        ui.SetActive(false);
        TaskDoneAction();
    }
    public void SetListener(Action action)
    {
        TaskDoneAction += action;
    }
}

