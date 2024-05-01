using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Debug : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public void AddText(string text)
    {
        string displayText = this.text.text;

        displayText += "\n" + text;

        this.text.text = displayText;
    }
}
