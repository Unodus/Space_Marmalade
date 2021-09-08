using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDebugger : MonoBehaviour
{
    public ScriptableUniverse gridverse;
 
    void Awake()
    {
        ScriptableGrid gridprofile = gridverse.displayGrid;
        gridprofile.GameGrid.settings.GameObjectRef = gameObject;
        gridprofile.GameGrid.settings.PolarActive = false;
        gridverse.Init();
    }
    private void OnDestroy()
    {
        gridverse.DeInit();
    }
    void LateUpdate()
    {
        gridverse.UniverseUpdate();
    }
}
