using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StartupScript : MonoBehaviour {

    public Text modelTypeText;
    public Text modelText;
    public Text serviceText;
    public Image image;

    // Use this for initialization
    void Start() {
        Button[] btn = gameObject.GetComponentsInChildren<Button>();
        btn[0].onClick.AddListener(GameObject.Find("ARCamera").GetComponent<Task>().OnClickConfirm);
        //btn[1].onClick.AddListener(GameObject.Find("ARCamera").GetComponent<Task>().OnClickCancel);
    }

    public void ApplyDBInfo(DbInfo dbStruct)
    {
        modelTypeText.text = dbStruct.ModelType;
        modelText.text = dbStruct.ModelInfo;
        serviceText.text = dbStruct.Text;

        //GetImageFromFile(dbStruct.ModelType); 
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
