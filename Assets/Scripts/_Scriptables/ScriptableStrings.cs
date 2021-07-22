using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StringProfile", menuName = "ScriptableObjects/StringProfile"   )]

public class ScriptableStrings : ScriptableObject
{


    public enum StringCategories
    {
        None,
        BootUI,
        ConsoleMessages

    }


    [Serializable]
    public class GameString
    {
        public string Content;
    }

    [Serializable]
    public class StringCategory
    {
        public StringCategories myCategory;
        public GameString[] myGameStrings;
    }

    public StringCategory[] stringPalette;    // array of all palettes

    public string GetStringByIdentifier(StringCategories myStringCategory, int stringIndex)
    {
        string returnVal = "Null Ref";



        foreach(StringCategory i in stringPalette)
        {

            if (i.myCategory == myStringCategory)
            { 
                if (stringIndex < i.myGameStrings.Length) return i.myGameStrings[stringIndex].Content; 
            }

        }

        return returnVal;
    }

    void OnValidate()
    {
        UpdateStrings();
    }
    public void UpdateStrings()
    {
        StringSetter[] StringObjects = Resources.FindObjectsOfTypeAll<StringSetter>();
        foreach(StringSetter i in StringObjects)
        {
            i.UpdateString();
        }
    }


}
