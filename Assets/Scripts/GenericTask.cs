using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface IGenericTask
{
    void StartTask();
}

public class GenericTask : MonoBehaviour, IGenericTask {

    public GenericTask(int taskType)
    {
        switch(taskType)
        {
            case 0:
                
                // New class of task type 0
                // example new TaskFindVehicle();
                break;

            case 1:

                // New class of task type 0
                // example new TaskDisplayServiceIinfo();
                break;

            case 2:

                // New class of task type 0
                // example new TaskFindComponent();
                break;

            case 3:

                // New class of task type 0
                // example new TaskMountComponent();
                break;
        };
    }

    public void StartTask()
    {

    }

}
