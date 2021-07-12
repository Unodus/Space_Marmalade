using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class LoadTextFromMemory : MonoBehaviour
{
    [SerializeField] TextAsset txt;
    [SerializeField] int Line;
    


    void LoadFromFile(int newline)
    {

        txt = (TextAsset)Resources.Load("strings", typeof(TextAsset));

        string[] linesInFile = txt.text.Split('\n');


        if (TryGetComponent(out Text i) )
            i.text = linesInFile[newline];
        
        if (TryGetComponent(out TextMeshProUGUI j))
            j.text = linesInFile[newline];
        
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadFromFile(Line);
    }

}
