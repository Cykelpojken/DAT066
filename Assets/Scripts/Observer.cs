using UnityEngine;
using UnityEngine.SceneManagement;

public class Observer : MonoBehaviour{

    
    private QrReader qr;
    private GameObject obj;
    private ShowG g;
    private bool first = true;

    public Observer(QrReader qr, ShowG g)
    {
        this.qr = qr;
        this.g = g;
        qr.OnQrDetected += HandleOnQrDetected;
    }

    private void HandleOnQrDetected(string arg1, float[] arg2)
    {
        if (first)
        {
            obj = Resources.Load("Sprite") as GameObject;
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Red");
            obj = Instantiate(obj);
            first = false;
        }
        g.Draw(obj, arg2[0], arg2[1], arg2[2], arg2[3]);
    }
}
