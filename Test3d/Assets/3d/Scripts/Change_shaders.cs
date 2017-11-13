using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_shaders : MonoBehaviour
{

    public void Do_shit(GameObject e)
    {
        //Color black = new Color(0, 0, 0, 1);
        /*Material[] Materials;
        Materials = GetComponents<Material>();*/

        List<GameObject> GameObjectList = new List<GameObject>();
        Material[] MaterialList;
        MeshRenderer Meshrenderer;

        Transform[] ob = GameObject.Find("scania").GetComponentsInChildren<Transform>(); //Gets children by parent name
        foreach (Transform t in ob) 
        {
            if (t.gameObject.name != "scania" && t.gameObject.name != e.transform.parent.name) //Does not hide parent
                t.gameObject.SetActive(false);

            GameObjectList.Add(t.gameObject);
            if (t.gameObject.name == e.name)//Does not hide the argument-object
            {
                t.gameObject.SetActive(true);

                Meshrenderer = t.gameObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer; //Works provided that all gameobjects uses MeshRenderer
                MaterialList = Meshrenderer.materials;
                foreach (Material m in MaterialList)
                {
                    m.SetColor("_Color", new Color(1, 1, 0));
                }
            }
        }
    }
}
