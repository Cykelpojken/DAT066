using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLogin : MonoBehaviour {

    public event Action<String> VinScanned;
    private QrReader qr;
    private Database db;
    private UIQrScanner uiQr;
    private string qrData = "", vinNumber;
    private bool vinScanned = false, login = true;

    public void SetStart(QrReader qr, UIQrScanner uiQR)
    {
        this.qr = qr;
        this.uiQr = uiQR;
        qr.OnQrDetected += HandleQr;
        qr.enabled = true;
        uiQr.button.gameObject.SetActive(false);
    }

    private void HandleQr(string arg1, float[] arg2)
    {
        if (qrData != arg1)
        {
            if (login)
            {
                uiQr.SetText(arg1 + ", do you want to log in?");
                uiQr.button.gameObject.SetActive(true);
                uiQr.button.enabled = true;
                uiQr.button.onClick.AddListener(OnLoginAccept);
            }
            else
            {
                uiQr.SetText("Vin : " + arg1 + " scanned, do you want to continue?");
                uiQr.GetButton().gameObject.SetActive(true);
                uiQr.button.enabled = true;
                vinNumber = arg1;
                uiQr.button.onClick.AddListener(OnClickVinAccept);
            }

        }
        qrData = arg1;
        vinScanned = true;
    }

    private void OnLoginAccept()
    {
        login = false;
        uiQr.SetText("Scan vin number");
        uiQr.button.onClick.RemoveAllListeners();
        uiQr.button.gameObject.SetActive(false);
        uiQr.button.onClick.AddListener(OnClickVinAccept);
    }

    private void OnClickVinAccept()
    {
        qr.OnQrDetected -= HandleQr;
        qr.enabled = false;
        try
        {
            VinScanned(vinNumber);
        }
        catch(NullReferenceException E)
        {

        }
    }
}
