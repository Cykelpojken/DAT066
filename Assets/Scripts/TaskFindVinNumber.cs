using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFindVinNumber : MonoBehaviour, IGenericTask {
    private QrReader qr;
    public TaskFindVinNumber(Database db)
    {
        qr =  GameObject.FindObjectOfType<QrReader>();
        qr.OnQrDetected += HandleOnQrDetected;
        qr.enabled = true;
    }
    public void StartTask()
    {
        Debug.Log("TaskFindVin");
        /*
         * Dispay Text stating:
         * "Scan vin number to continue"
         *  Testa DB con
         *  ingen wifi? 
         *  ->display No wifi symbol
         *  
         *  annars ha kvar text
         *  detta måste sitta i main tho.... fuck
         */ 

    }

    private void HandleOnQrDetected(string arg1, float[] arg2)
    {
        Debug.Log("asd");
    }

    public void InitTask(string[] taskInfo, Database db)
    {
        throw new System.NotImplementedException();
    }

    public void DestroyTask()
    {
        throw new System.NotImplementedException();
    }

    public Action SetListener()
    {
        throw new NotImplementedException();
    }

    public void SetListener(Action action)
    {
        throw new NotImplementedException();
    }
}

