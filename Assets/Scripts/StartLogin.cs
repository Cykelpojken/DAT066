using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLogin : MonoBehaviour {

    public event Action<String> VinScanned;
    private QrReader qr;
    private Database db;
    private GameObject sprite = null;
    private UIQrScanner uiQr;
    private ShowG g;
    private string qrData = "", vinNumber;
    private bool vinScanned = false, login = true, first = true;

    public void SetStart(QrReader qr, UIQrScanner uiQR)
    {
        db = GameObject.FindObjectOfType<Database>();
        this.qr = qr;
        this.uiQr = uiQR;
        qr.OnQrDetected += HandleQr;
        qr.enabled = true;
        uiQr.button.gameObject.SetActive(false);
        g = gameObject.AddComponent(typeof(ShowG)) as ShowG;
    }

    private void HandleQr(string arg1, float[] arg2)
    {
        if (qrData != arg1)
        {
            if (login)
            {
                HandleQrLogin(arg1);
            }
            else
            {
                if(!HandleQrVin(arg1))
                {
                    if (first)
                    {
                        first = false;
                        sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Grey");
                        sprite.SetActive(true);
                    }
                }
            }

        }
        if(sprite != null)
            g.Draw(sprite, arg2[0], arg2[1], arg2[2], arg2[3]);
        qrData = arg1;
        vinScanned = true;
    }

    private void HandleQrLogin(string arg1)
    {
        if (db.GetUser(arg1))
        {
            uiQr.SetText(arg1 + ", do you want to log in?");
            uiQr.button.gameObject.SetActive(true);
            uiQr.button.enabled = true;
            uiQr.button.onClick.AddListener(OnLoginAccept);
            if (sprite != null)
                sprite.SetActive(false);
        }
        else
        {
            sprite = Resources.Load("Sprite") as GameObject;
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Grey");
            sprite = Instantiate(sprite);
            uiQr.SetText("User not recognized");
        }
    }

    private bool HandleQrVin(string arg1)
    {
        if (db.GetModel(arg1))
        {
            uiQr.SetText("Vin : " + arg1 + " scanned, do you want to continue?");
            uiQr.GetButton().gameObject.SetActive(true);
            uiQr.button.enabled = true;
            vinNumber = arg1;
            uiQr.button.onClick.AddListener(OnClickVinAccept);
            if(sprite != null)
                sprite.SetActive(false);
        }
        else
        {
            return false;
        }
        return true;
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
        Destroy(sprite);
        try
        {
            VinScanned(vinNumber);
        }
        catch(NullReferenceException E)
        {

        }
    }
}
