using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Change_shaders : MonoBehaviour
{
    public void Click()
    {
        GameObject g = GameObject.Find("chassis01");
        GameObject p = GameObject.Find("front_ty23");
        GameObject[] e = { g, p };
        Highlight(e);
    }

    Material[] MaterialList;
    MeshRenderer Meshrenderer;
    public void Highlight(GameObject[] e) //Works provided that all Gameobjects are not decendent more then two childs down from main object
    {
        Transform[] ob = GameObject.Find("scania").GetComponentsInChildren<Transform>(); //Gets children by parent name 
        List<GameObject> RemoveList = new List<GameObject>();

        foreach (GameObject g in e) {
            for(int i = 0; i < ob.Length; i++)
            {
                
                if (ob[i].name == g.transform.name)
                {
                    GameObject temp = g;
                    bool parentexist = true;
                    while(parentexist)
                    {
                        
                        if(!RemoveList.Contains(temp)) //Adds all the objects that are to be ignored to a list
                            RemoveList.Add(temp);

                        if (temp.transform.parent == null)
                            parentexist = false;
                        else
                            temp = temp.transform.parent.gameObject;
                    }
                }
            }
            for(int i = 0; i < ob.Length; i++)
            {
                if (RemoveList.Contains(ob[i].gameObject))
                {
                    var foos = new List<Transform>(ob); //Removes the gameobjects from ob
                    foos.RemoveAt(i);
                    ob = foos.ToArray();
                }
            }
        }
        foreach (GameObject g in e)
        {
            HideAndHighlight(ob, g); //Sends in the list that no longer contains the choosen Gameobjects, and one of the game objects that is to be hidden
        }
        
    }

    public void HideAndHighlight(Transform[] ob, GameObject e)
    {
        foreach(Transform t in ob)
        {
            if (t.gameObject.name != e.transform.parent.name)
            {
                t.gameObject.SetActive(false);
            }
        }
        e.SetActive(true);
        Meshrenderer = e.GetComponent(typeof(MeshRenderer)) as MeshRenderer; //Works provided that all gameobjects uses MeshRenderer
        MaterialList = Meshrenderer.materials;
        foreach (Material m in MaterialList)
        {
            m.SetColor("_Color", new Color(1, 1, 0));
        }
    }
}
