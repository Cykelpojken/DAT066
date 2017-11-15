using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartupScript : MonoBehaviour {

    public Text modelTypeText;
    public Text modelText;
    public Text serviceText;
    public Image image;
    private Button[] btn;
    public Action ButtonConfirmPressed;
    public Action ButtonCancelPressed;

    // Use this for initialization
    void Start() {
        btn = gameObject.GetComponentsInChildren<Button>();
        btn[0].onClick.AddListener(ButtonConfirm);
        btn[1].onClick.AddListener(ButtonCancel);
    }

    public void InitUI(string[] taskInfo)
    {
        modelTypeText.text = taskInfo[0] + "(" + taskInfo[1] + ")";
        SetModelText(taskInfo);
        serviceText.text = "";
    }

    public void SetServiceText(string text)
    {
        serviceText.text += text;
    }

    public void SetModelText(string[] text)
    {
        modelText.text = " Engine: " + text[2] + "\n Service" + text[3] + "\n Model Info: \n " + text[4];
    }

    private void ButtonConfirm()
    {
        try
        {
            ButtonConfirmPressed();
        }
        catch(NullReferenceException E) { }
    }

    private void ButtonCancel()
    {
        try
        {
            ButtonCancelPressed();
        }
        catch (NullReferenceException E) { }
    }

    private void GetImageFromFile(string imageName)
    {
        try
        {
            if (Resources.Load<Sprite>("ModelImages/" + imageName) == null)
                throw new FileNotFoundException();

            image.GetComponent<Image>().sprite = Resources.Load<Sprite>("ModelImages/" + imageName);
        }
        catch(FileNotFoundException e)
        {
            Debug.Log("File not found: " + e);
            image.GetComponent<Image>().enabled = false;
        }
    }
    
}
