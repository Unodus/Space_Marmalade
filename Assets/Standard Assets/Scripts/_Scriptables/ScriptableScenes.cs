using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneProfile", menuName = "ScriptableObjects/SceneProfile")]

public class ScriptableScenes : ScriptableObject

{
    [Serializable]
    public class SceneType 
    {
        public string SceneName;
    }

    public SceneType[] ScenesToLoadOnBoot;    // array of all palettes

   

}
