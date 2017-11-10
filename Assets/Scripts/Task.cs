using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Task : MonoBehaviour{
    
    private int taskType;
    private DbInfo dbStruct;
    private Observer test;
    private GameObject obj;
    private GameObject ui;
    private bool first = true;
    private static QrReader qr;
    private ShowG g;
    private string oldArg;

    public void InitDbStruct(DbInfo dbStruct)
    {
        this.dbStruct = dbStruct;
    }

    public void StartTask()
    { 
        if (dbStruct.Type == 0)
        {
            g = gameObject.AddComponent(typeof(ShowG)) as ShowG;
            qr = gameObject.AddComponent(typeof(QrReader)) as QrReader;
            qr.OnQrDetected += HandleOnQrDetected;
            if (dbStruct.GetDatabase().GetCon())
            {

            }
            else
            {
                obj = Resources.Load("Sprite") as GameObject;
                obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/NoWifi");
                obj = Instantiate(obj);
            }
        }
    }

    private void HandleOnQrDetected(string arg1, float[] arg2)
    {
        if(dbStruct.Type == 0)
        {
            if (first && oldArg != arg1)
            {
                dbStruct.GetDatabase().GetModel(arg1, dbStruct);
                dbStruct = dbStruct.GetDatabase().GetDbInfo();
                if (dbStruct.ModelType != null)
                {
                    ui = Instantiate(Resources.Load("ServiceUI") as GameObject);
                    ui.GetComponent<StartupScript>().ApplyDBInfo(dbStruct);

                    try
                    {

                        Destroy(obj);
                        
                    }
                    catch(Exception e)
                    {

                    }
                    Destroy(g);
                    qr.OnQrDetected -= HandleOnQrDetected;
                    Destroy(qr);
                    first = false;
                }
                else
                {
                    obj = Resources.Load("Sprite") as GameObject;
                    obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Grey");
                    obj = Instantiate(obj);
                }
            }
            else
            {
                g.Draw(obj, arg2[0], arg2[1], arg2[2], arg2[3]);
            }
            oldArg = arg1;
        }
    }

    public void OnClickConfirm()
    {
        if(dbStruct.Type == 1 && !dbStruct.Begun)
        {
            dbStruct.Begun = true;
            dbStruct.Text += "\n The task has begun";
            ui.GetComponent<StartupScript>().ApplyDBInfo(dbStruct);
        }
        else
        {
            dbStruct.Id++;
            dbStruct.Begun = false;
            dbStruct.GetDatabase().GetTask(dbStruct);
            dbStruct = dbStruct.GetDatabase().GetDbInfo();
            ui.GetComponent<StartupScript>().ApplyDBInfo(dbStruct);
        }
    }
}
