using System;
using UnityEngine;

public class JSListener : MonoBehaviour
{
    private string displayed;

    private void Awake()
    {
        displayed = String.Empty;
    }

    public void MyFunction(string MyString)
    {
        displayed = MyString;
    }

    private void OnGUI()
    {
        if (string.IsNullOrEmpty(displayed))
        {
            GUILayout.Label($"MyString IsNullOrEmpty");
            return;
        }
        
        GUILayout.Label($"MyString: {displayed}");
    }
}
