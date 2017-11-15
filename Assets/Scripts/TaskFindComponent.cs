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
    private GameObject sprite;
    private GameObject ui;
    private UIQrScanner uiQr;
    private ShowG g;
    private Action TaskDoneAction;

    public void InitTask(string[] taskInfo, Database db)
    {

        qr = GameObject.FindObjectOfType<QrReader>();
        qr.OnQrDetected += HandleOnQrDetected;
        qr.enabled = true;
        g = gameObject.AddComponent(typeof(ShowG)) as ShowG;
        ui = Instantiate(Resources.Load("QRscanUI") as GameObject);
        uiQr = ui.GetComponent<UIQrScanner>();
        uiQr.SetText(taskInfo[0]);
    }

    private void HandleOnQrDetected(string arg1, float[] arg2)
    {
        if (first && oldArg != arg1)
        {
            sprite = Resources.Load("Sprite") as GameObject;
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Grey");
            sprite = Instantiate(sprite);
            first = false;
        }
        else
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
        //startupScript.ButtonConfirmPressed -= ConfirmClicked;
        TaskDoneAction();
    }

    public void SetListener(Action action)
    {
        TaskDoneAction += action;
    }
}

