using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridManager 
{
    public static GameObject currentGrid;
    public static string GridUpdateEvent;
    public static void Init(this GridObject i)
    {
        currentGrid = i.gameObject;
        GridUpdateEvent = ScriptableExtensions.s.scriptable.GameEvents.GetEventByType( ScriptableExtensions.s.scriptable.Enums.GetEnum(GlobalEnum.InputEvent, 2)).Name;
        ScriptableExtensions.s.scriptable.Grids.Init();
        PointManager.Init();
        LineManager.Init();
    }
    public static void DeInit(this GridObject i)
    {
        PointManager.Deinit();
        LineManager.DeInit();
    }

    public static void GridUpdate(this GridObject i)
    {
        EventDictionary.TriggerEvent(GridUpdateEvent);
    }

}
