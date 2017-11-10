using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct DbInfo
{
    private static Database Db = new Database();
    public string ModelType { get; set; }
    public string ModelInfo { get; set; }
    public int TaskAmount { get; set; }
    public string Text { get; set; }
    public int Type { get; set; }
    public bool Begun { get; set; }
    public int Id { get; set; }
    public Database GetDatabase()
    {
        return Db;
    }
}


public class Main : MonoBehaviour {

    public static Task task;

    private void Awake()
    {
        task = gameObject.AddComponent(typeof(Task)) as Task;
        DbInfo firstTaskInfo = new DbInfo
        {
            Type = 0,
            Begun = false
        };
        task.InitDbStruct(firstTaskInfo);
        task.StartTask();
    }
}

