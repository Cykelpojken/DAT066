using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQrScanner : MonoBehaviour
{
    public Button button;
    public Text text;

    public Button GetButton()
    {
        return button;
    }

    public void SetText(string input)
    {
        text.text = input;
    } 
}
