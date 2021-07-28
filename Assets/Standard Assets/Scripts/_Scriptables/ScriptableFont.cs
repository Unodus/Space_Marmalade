using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FontProfile", menuName = "ScriptableObjects/FontProfile")]

public class ScriptableFont : ScriptableObject
{


    public enum FontCategories
    {
        None,
        Heading1,
        NormalText

    }


    [Serializable]
    public class FontClass
    {
        public Font myFont;
        public Color DefaultColor;
        public int FontSize;

        public FontStyle Style;
//        public bool Bold, Italic, Underline;
    }

    [Serializable]
    public class FontCategory
    {
        public FontCategories myCategory;
        public FontClass myFonts;
    }

    public FontCategory[] fontPalette;    // array of all palettes

    public FontClass GetStyleByCategory(FontCategories myFontCategory)
    {
        foreach (FontCategory i in fontPalette)
        {
            if (i.myCategory == myFontCategory) return i.myFonts;
        }
        return null;
    }

    void OnValidate()
    {
        UpdateFonts();
    }
    public void UpdateFonts()
    {


        StringSetter[] StringObjects = Resources.FindObjectsOfTypeAll<StringSetter>();

        foreach (StringSetter i in StringObjects)
        {
            i.UpdateFont();
        }
    }


}