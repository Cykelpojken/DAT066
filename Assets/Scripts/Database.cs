using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Database : MonoBehaviour {

    private string model;
    private string url;

    private string answer = null;

    public void InitDb()
    {
        this.url = "http://DESKTOP-HB18BHU/ar_service_db/";
    }

    public void SetModel(string model)
    {
        this.model = model;
    }

    private void SendRequest(string url, WWWForm form)
    {
        
        using (WWW www = new WWW(url, form))
        {
            WaitForSeconds w;
            while (!www.isDone)
                w = new WaitForSeconds(0.1f);

            answer = www.text;
        }
    }
    public Boolean GetUser(string user)
    {
        WWWForm form = new WWWForm();
        form.AddField("userPost", user);
        SendRequest(url + "CheckUser.php", form);
        string tmp = "true";
        Debug.Log(answer);
        char[] tasd = answer.ToCharArray();
        for (int i = 0; i < tasd.Length -1; i++)
            if (tasd[i] != tmp[i])
            {
                return false;
            }
        return true;
    }

    public Boolean GetModel(string model)
    {
        WWWForm form = new WWWForm();
        form.AddField("modelPost", model);
        SendRequest(url + "CheckModel.php", form);
        string tmp = "true";
        char[] tasd = answer.ToCharArray();
        for (int i = 0; i < tasd.Length - 1; i++)
            if (tasd[i] != tmp[i])
            {
                return false;
            }
        return true;
    }
    public string[] GetTasks()
    {
        WWWForm form = new WWWForm();
        form.AddField("modelPost", model);
        SendRequest(url + "GetModel.php", form);
        return SplitString(';'); 
    }

    public string[] GetTask(int taskNr)
    { 
        WWWForm form = new WWWForm();
        form.AddField("modelPost", model);
        form.AddField("tasknrPost", taskNr);
        SendRequest(url + "GetTask.php", form);
        return SplitString(';');
    }

    private string[] SplitString(char splitter) 
    {
        return answer.Split(splitter);
    }

    private int[] SplitStringToInt(char splitter)
    {
        string[] splitAnswer = answer.Split(splitter);
        int[] intArray = new int[splitAnswer.Length];
        int index = 0;
        foreach(string s in splitAnswer)
        {
            intArray[index] = int.Parse(s);
            index++;
        }
        return intArray;
    }
}
