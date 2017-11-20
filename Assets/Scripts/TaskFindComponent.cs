using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFindComponent : MonoBehaviour, IGenericTask
{
    private QrReader qr;
    public string vin;
    private bool first = true;
    private string oldArg = "";
    private string[] taskInfo;   
    private GameObject sprite;
    private GameObject ui;
    private UIQrScanner uiQr;
    private ShowG g;
    private Action TaskDoneAction;

    public void InitTask(string[] taskInfo, Database db)
    {
        this.taskInfo = taskInfo;
        qr = GameObject.FindObjectOfType<QrReader>();
        qr.OnQrDetected += HandleOnQrDetected;
        qr.enabled = true;
        g = gameObject.AddComponent(typeof(ShowG)) as ShowG;
        ui = Instantiate(Resources.Load("QRscanUI") as GameObject);
        uiQr = ui.GetComponent<UIQrScanner>();
        uiQr.button.gameObject.SetActive(false);
        uiQr.SetText(taskInfo[0]);

    }

    private void HandleOnQrDetected(string arg1, float[] arg2)
    {

        if (first)
        {
            first = false;
            sprite = Resources.Load("Sprite") as GameObject;
            sprite = Instantiate(sprite);
        }
        if (!arg1.Equals(oldArg))
        {
            if (arg1.Equals(taskInfo[1]))
            {
                sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Green");
                uiQr.GetButton().gameObject.SetActive(true);
                uiQr.button.enabled = true;
                uiQr.button.onClick.AddListener(ConfirmClicked);
            }
            else
            {
                sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Red");
                uiQr.button.gameObject.SetActive(false);
            }
        }
        if(!first)
        {
            g.Draw(sprite, arg2[0], arg2[1], arg2[2], arg2[3]);
        }
        oldArg = arg1;
    }

    public void StartTask()
    {
        Debug.Log("ShowServiceType");
    }

    public void DestroyTask()
    {
        Destroy(this);
    }


    public void ConfirmClicked()
    {
        DestroyTask();
        qr.OnQrDetected -= HandleOnQrDetected;
        Destroy(uiQr);
        TaskDoneAction();
    }

    public void SetListener(Action action)
    {
        TaskDoneAction += action;
    }
}

