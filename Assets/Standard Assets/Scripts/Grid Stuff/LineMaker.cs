using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LineMaker
{

    #region depricated code
    //public static void SpawnLine(Vector2 Start, Vector2 End) // Instantiates the line gameobject 
    //{
    //    MyLine lp;
    //    lp = new MyLine();
    //    lp.p = Object.Instantiate(LinePrefab);
    //    lp.p.transform.SetParent(LineHolder.transform);
    //    lp.p.name = "Line: " + Start + " to " + End;


    //    lp.pp = lp.p.GetComponent<LineRenderer>();



    //    lp.pp.widthMultiplier = LineWidth;

    //    lp.pp.startColor    = Color.blue;
    //    lp.pp.endColor      = LineColor;

    //    lp.pp.positionCount = NumOfSegments;

    //    lp.Positions = new Vector2[NumOfSegments];
    //    for (int i = 0; i < NumOfSegments; i++)
    //    {

    //        if (GridMath.PolarActive)
    //        {
    //            Vector2 NewStart = GridMath.SetPosition(Start.x, Start.y);
    //            Vector2 NewEnd = GridMath.SetPosition(End.x, End.y);

    //            Vector2 Interp = new Vector2(Mathf.Lerp(NewStart.x, NewEnd.x, (i / (float)NumOfSegments)), Mathf.Lerp(NewStart.y, NewEnd.y, (i / (float)NumOfSegments)));

    //            lp.Positions[i] =GridMath.GetPosition(Interp);
    //            lp.pp.SetPosition(i, GridMath.SetPosition(Interp.x, Interp.y));

    //        }
    //        else
    //        {
    //            Vector2 Interp = new Vector2(Mathf.Lerp(Start.x, End.x, (i / (float)NumOfSegments)), Mathf.Lerp(Start.y, End.y, (i / (float)NumOfSegments)));
    //            lp.Positions[i] = Interp;

    //            lp.pp.SetPosition(i, GridMath.SetPosition(Interp.x, Interp.y));

    //        }
    //    }

    //    MyLines.Add(lp);

    //}

    //public static void SpawnLine(Vector3 cStart, Vector3 cEnd) // Instantiates the line object, after converting world space to grid space
    //{

    //    Vector2 Start = GridMath.GetPosition(cStart);
    //    Vector2 End = GridMath.GetPosition(cEnd);

    //    SpawnLine(Start, End);

    //}
    //public static void SpawnRandomLine() // spawns a random line on the grid, useful for debugging
    //{
    //          Vector3 Start = GridMath.SetPosition(Random.Range(0, GridMath.Columns), Random.Range(0, GridMath.Rows));
    //          Vector3 End = GridMath.SetPosition(Random.Range(0, GridMath.Columns), Random.Range(0, GridMath.Rows));
    //    SpawnLine(Start, End);
    //}

    //public static void SpawnRandomCircle() // Spawns a random circle, useful for debugging
    //{
    //    Vector2 StartPoint = new Vector2(Random.Range(1, GridMath.Columns - 1), Random.Range(1, GridMath.Rows - 1));

    //    Vector3 Start = GridMath.SetPosition(StartPoint.x, StartPoint.y);

    //    StartPoint = GridMath.CalcluateRatio(StartPoint);

    //    Vector3 End = GridMath.SetPosition(Random.Range(1, GridMath.Columns-1), Random.Range(1, GridMath.Rows-1));


    //    Debug.Log( StartPoint + " vs " + GridMath.PolartoCoord(Start));
    //    SpawnCircle(Start, End);
    //}
    //public static void SpawnCircle(Vector3 cStart, Vector3 cEnd) // Spawn circle with world space vector
    //{

    //    Vector2 Start = GridMath.GetPosition(cStart);
    //    Vector2 End = GridMath.GetPosition(cEnd);

    //    SpawnCircle(Start, End);

    //}

    //public static void SpawnCircle(Vector2 Start, Vector2 End) // Spawn circle with grid space vector
    //{
    //    float Radius = Vector2.Distance(Start, End);
    //    Radius *= 0.5f;

    //    MyLine lp;
    //    lp = new MyLine();
    //    lp.p = Object.Instantiate(LinePrefab);
    //    lp.p.transform.SetParent(LineHolder.transform);
    //    lp.p.name = "Circle: " + Start + " with radius " + Radius;

    //    lp.pp = lp.p.GetComponent<LineRenderer>();
    //    lp.pp.widthMultiplier = LineWidth;

    //    lp.pp.startColor = Color.blue;
    //    lp.pp.endColor = LineColor;

    //    lp.pp.positionCount = NumOfSegments;
    //    lp.Positions = new Vector2[NumOfSegments];

    //    for (int i = 0; i < NumOfSegments; i++)
    //    {

    //        if (GridMath.PolarActive)
    //        {

    //            Vector3 NewStart = GridMath.SetPosition(Start.x, Start.y);
    //            //  Vector2 NewEnd = GridMath.SetPosition(End.x, End.y);

    //            float angle = Mathf.Lerp(0, 2 * Mathf.PI, i / (NumOfSegments - 1.0f));
    //            float x = Mathf.Sin(angle) * Radius;
    //            float y = Mathf.Cos(angle) * Radius;



    //            Vector3 Interp = NewStart + GridMath.SetPosition(x, y);

    //            lp.Positions[i] = GridMath.GetPosition(Interp);

    //        }
    //        else
    //        {


    //            float angle = Mathf.Lerp(0, 2 * Mathf.PI, i / (NumOfSegments -1.0f));
    //            float x = Mathf.Sin(angle) * Radius;
    //            float y = Mathf.Cos(angle) * Radius;

    //            lp.Positions[i] = Start + new Vector2(x, y);
    //        }
    //    }

    //    MyLines.Add(lp);

    //}
    #endregion


}