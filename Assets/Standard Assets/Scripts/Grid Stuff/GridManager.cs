using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridManager 
{
    public static GameObject currentGrid;
    public static void Init(this GridObject i)
    {
        currentGrid = i.gameObject;

        PointManager.Init();
        LineManager.Init();
    }
    public static void DeInit(this GridObject i)
    {
        PointManager.Deinit();
        LineManager.DeInit();
    }

    public static void Update(this GridObject i)
    {
        LineManager.LineUpdate();
        PointManager.PointUpdate();
    }

}
