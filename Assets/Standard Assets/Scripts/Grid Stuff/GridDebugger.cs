using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDebugger : MonoBehaviour
{
    public ScriptableUniverse gridverse;
 
    void Awake()
    {
        ScriptableGrid gridprofile = gridverse.displayGrid;

        UniverseManager.UniverseCentre = gameObject;

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

    [ContextMenu("Leave Universe")]
    void LeaveUniverse()
    {
        StartCoroutine(gridverse.ZoomFarChange(UniverseManager.currentUniverse.ContainerUniverse, 0.0f, 50.0f, 1));
    }

    [ContextMenu("Enter Universe")]
    void EnterUniverse()
    {
        StartCoroutine(gridverse.ZoomFarChange(UniverseManager.currentUniverse.ContainerUniverse, 50.0f, 0.0f, 1));
    }
}
