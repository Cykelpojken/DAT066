using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour {
    private Observer test;
    private static QrReader qr;
    private ShowG g;
    private void Awake()
    {
        g = gameObject.AddComponent(typeof(ShowG)) as ShowG;
        qr = gameObject.AddComponent(typeof(QrReader)) as QrReader;
        test = new Observer(qr, g);
    }
    

}
