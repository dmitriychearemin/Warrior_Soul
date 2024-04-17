using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class For_Input_Fields : MonoBehaviour
{
    private int number = 0;
    private string text;
    TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public void SetNumber()
    {
        text = inputField.text;
        int.TryParse(text, out number);
    }

    public int GetNumber()
    {
        return number;
    }


}
