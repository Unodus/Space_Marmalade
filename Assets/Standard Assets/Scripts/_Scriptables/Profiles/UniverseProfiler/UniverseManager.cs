using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UniverseManager
{
    public static UniverseObject currentUniverse;
    public static GameObject UniverseCentre;
    public static void Init(this ScriptableUniverse i)
    {
        currentUniverse = i.startGrid;
        ScriptableGrid grid = i.displayGrid;

        grid.GameGrid.settings.PolarActive = false;
        grid.GameGrid.settings.GameObjectRef = UniverseCentre;

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


    public static IEnumerator ZoomFarChange(this ScriptableUniverse i, UniverseObject newUniverse, float beforeZoom, float afterZoom, float Speed)
    {
        float CurrentZoom = i.displayGrid.GameGrid.settings.Size;
        yield return ZoomLerp(i.displayGrid.GameGrid.settings, CurrentZoom, beforeZoom, Speed);
        yield return new WaitForSeconds(0.1f);
        i.SwitchUniverse(newUniverse);
        yield return ZoomLerp(i.displayGrid.GameGrid.settings, afterZoom, CurrentZoom, Speed);

        yield return null;

    }
      static IEnumerator ZoomLerp(GridSettings i, float StartZoom, float EndZoom, float Speed)
    {
        float lerpVal = 0;
        GridSettings gridSettings = i;

        while (lerpVal <= 1)
        {
            gridSettings.Size = Mathf.Lerp(StartZoom, EndZoom, lerpVal);
            lerpVal += Time.deltaTime * Speed;
            yield return new WaitForEndOfFrame();
        }

        gridSettings.Size = Mathf.Lerp(StartZoom, EndZoom, 1);
        yield return null;
    }

    public static void SwitchUniverse(this ScriptableUniverse i, UniverseObject newUniverse)
    {


        ScriptableGrid grid = i.displayGrid;


        grid.GameGrid.gridObject.DeInit();

        currentUniverse = newUniverse;

        grid.GameGrid.gridObject = currentUniverse.InsideGrid;


        grid.GameGrid.settings.GameObjectRef = UniverseCentre;
        grid.GameGrid.settings.CachedGridPositions.Clear();
        grid.GameGrid.settings.CachedPolarPositions.Clear();

        if (grid.GameGrid.settings.PolarActive)
        {
            grid.GameGrid.settings.PolarActive = false;
            grid.GameGrid.gridObject.Init();
            grid.GameGrid.settings.PolarActive = true;

        }
        else
        {
            grid.GameGrid.gridObject.Init();
        }


    }

}
