using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UniverseManager
{
    static UniverseObject currentUniverse;
    public static void Init(this ScriptableUniverse i)
    {
        currentUniverse = i.startGrid;
        ScriptableGrid grid = i.displayGrid;
        grid.GameGrid.gridObject = currentUniverse.InsideGrid;
        grid.GameGrid.gridObject.Init();
    }
    public static void DeInit(this ScriptableUniverse i)
    {
        ScriptableGrid grid = i.displayGrid;

        grid.GameGrid.gridObject.DeInit();
        i.displayGrid.GameGrid.settings.GameObjectRef = null;
    }

    public static void UniverseUpdate(this ScriptableUniverse i)
    {
        ScriptableGrid grid = i.displayGrid;

        grid.GameGrid.gridObject.GridUpdate();
    }

    public static void SwitchUniverse(this ScriptableUniverse i, UniverseObject newUniverse)
    {

     
        ScriptableGrid grid = i.displayGrid;
        grid.GameGrid.gridObject.DeInit();

        currentUniverse = newUniverse;
        grid.GameGrid.gridObject = currentUniverse.InsideGrid;

        grid.GameGrid.gridObject.Init();

    }

}
