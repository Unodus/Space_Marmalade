using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringSetter : MonoBehaviour
{
    [SerializeField] Text textElement;
    [SerializeField] ScriptableFont fontLibrary;
    [SerializeField] ScriptableFont.FontCategories fontCategory;
    [SerializeField] ScriptableStrings stringLibrary;
    [SerializeField] ScriptableStrings.StringCategories stringCategory;



    [SerializeField] int stringIndex;


    private void Start()
    {
        UpdateString();
    }
    private void OnValidate()
    {
        UpdateString();
    }

    public void UpdateString()
    {
        if (stringLibrary == null) return;
        if (stringCategory == ScriptableStrings.StringCategories.None) return;
        if (stringIndex < 0) return;
        if (textElement == null)
        {
            if (TryGetComponent(out textElement)) UpdateString();
        }
        else
            {
                string i = stringLibrary.GetStringByIdentifier(stringCategory, stringIndex);
                textElement.text = i;
            }
    }
    public void UpdateFont()
    {


        if (fontLibrary == null) return;
        if (fontCategory == ScriptableFont.FontCategories.None) return;

        if (textElement == null)
        {
            if (TryGetComponent(out textElement)) UpdateFont();
        }
        else
        {

            ScriptableFont.FontClass i = fontLibrary.GetStyleByCategory(fontCategory);
            Debug.Log("wefw " + i.DefaultColor);
            textElement.font = i.myFont;
            textElement.color = i.DefaultColor;
            textElement.fontSize = i.FontSize;
            textElement.fontStyle = i.Style;
        }
    }
}
