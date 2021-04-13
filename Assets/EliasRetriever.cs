using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliasRetriever : MonoBehaviour
{

    [SerializeField] ScriptableElias EliasProfileRef;
    [SerializeField] ScriptableElias.Themes myTheme;

    // Start is called before the first frame update
    void Awake()
    {
        if(!TryGetComponent (out EliasDemoEventTrigger eliasComponent))
            return;
        


        foreach(ScriptableElias.EliasPalettes i in EliasProfileRef.eliasPalette)
        {
            if( i.MusicType == myTheme)
            {
                eliasComponent.actionPresetName = i.EliasName;
            }
        }

    }

}
