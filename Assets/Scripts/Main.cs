using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Main : MonoBehaviour {
    

    private static QrReader qr;
    private GameObject ui;
    private Database db;
    private UIQrScanner uiQr;
    private StartLogin startLogin;

    private void Awake()
    {

        db = gameObject.AddComponent(typeof(Database)) as Database;
        db.InitDb();
        qr = gameObject.AddComponent(typeof(QrReader)) as QrReader;
        qr.enabled = false;
        startLogin = gameObject.AddComponent(typeof(StartLogin)) as StartLogin;
        ui = Instantiate(Resources.Load("QRscanUI") as GameObject);
        uiQr = ui.GetComponent<UIQrScanner>();
        startLogin.VinScanned += HandleVinScanned;
        startLogin.SetStart(qr, uiQr);
    }

    private void HandleVinScanned(string arg1)
    {
        startLogin.VinScanned -= HandleVinScanned;
        db.SetModel(arg1);
        Destroy(startLogin);
        Destroy(uiQr);
        Destroy(ui);
        GenericTask task = gameObject.AddComponent(typeof(GenericTask)) as GenericTask;
        task.InitTask(db.GetTasks());
    }
}