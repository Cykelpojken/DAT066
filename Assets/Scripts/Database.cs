using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Database : MonoBehaviour {
    private IDbConnection dbconn;
    private DbInfo dbStruct;
    private string conn;
    public Database()
    {
            conn = "URI=file:" + Application.dataPath + "/DB3.sqlite3"; //Path to database.
    }

    public bool GetCon()
    {
        try
        {
            dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open(); //Open connection to the database.
        }
        catch(Exception E)
        {
            return false;
        }
        return true;
    }

    public void GetModel(string model, DbInfo db)
    {
        try
        {
            dbStruct = db;
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT * FROM QR WHERE ID = '" + model + "'";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                dbStruct.ModelType = reader.GetString(0);
                dbStruct.ModelInfo = reader.GetString(1);
                dbStruct.TaskAmount = reader.GetInt32(2);
            }
            dbStruct.Id = 1;
            GetTask(dbStruct);
        }
        catch(SqliteException e)
        {
            dbStruct.ModelType = null;
        }
    }

    public void GetTask(DbInfo db)
    {
        try
        {
            dbStruct = db;
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT task, task_type FROM " + dbStruct.ModelType + " WHERE tasknr = " + dbStruct.Id;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                dbStruct.Text = reader.GetString(0);
                dbStruct.Type = reader.GetInt32(1);
            }
        }
        catch (SqliteException e)
        {

        }
    }
    
    public DbInfo GetDbInfo()
    {
        return dbStruct;
    }
}

