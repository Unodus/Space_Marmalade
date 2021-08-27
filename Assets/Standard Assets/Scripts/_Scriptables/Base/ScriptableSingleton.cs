using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableSingleton : MonoBehaviour
{
    private static ScriptableSingleton _instance;
    public static ScriptableSingleton Instance { get { return _instance; } }

    public ScriptableScripts scriptableScripts;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
