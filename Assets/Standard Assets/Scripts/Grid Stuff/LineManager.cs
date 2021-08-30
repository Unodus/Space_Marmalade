using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LineManager
{

    // Ref to the line Object



    static ScriptableLines LineStyles;
    static ScriptableGrid gridSettings;
    static LineObject[] gridLines;
    static LineObject Outline;
    static List<LineObject> MyLines = new List<LineObject>();

    static Pool<PooledItem> LineObjects;
    public static void Init()
    {
        LineStyles = ScriptableExtensions.m_ScriptableLines;
        gridSettings = ScriptableExtensions.m_ScriptableGrid;

        CreateGrid();
    }

    public static void DeInit()
    {
        for (int i = 0; i < (gridLines.Length); i++) Object.Destroy(gridLines[i].p);
        Object.Destroy(Outline.p);

    }

    static void CreateGrid()
    {
        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();
        ScriptableLines.LineStyle outlineStyle = LineStyles.GetLineStyleFromPalette(ScriptableLines.StyleType.Outline);
        ScriptableLines.LineStyle insideLine = LineStyles.GetLineStyleFromPalette(ScriptableLines.StyleType.InsideLine);

        List<LineObject> tempGridlines = new List<LineObject>();
        List<Vector2> OutlinePositions = new List<Vector2>();

        OutlinePositions.Add(new Vector2(-0.5f, -0.5f));
        OutlinePositions.Add(new Vector2(-0.5f, myGrid.Rows - 0.5f));
        OutlinePositions.Add(new Vector2(myGrid.Columns - 0.5f, myGrid.Rows - 0.5f));
        OutlinePositions.Add(new Vector2(myGrid.Columns - 0.5f, -0.5f));
        OutlinePositions.Add(new Vector2(-0.5f, -0.5f));


        Outline = CreateLine(outlineStyle, OutlinePositions.ToArray());

        Vector2 Start;
        Vector2 End;

        for (int i = 0; i < (myGrid.Columns); i++)
        {

            for (int j = 0; j < (myGrid.Rows); j++)
            {

                Start = new Vector2(i, j);

                for (int xi = 0; xi <= 1; xi++)
                {
                    for (int yi = 0; yi <= 1; yi++)
                    {
                        if (xi == 0 && yi == 0) continue;
                        if (xi == 1 && yi == 1) continue;
                        //   if (xi == -1 && yi == -1) continue;
                        //  if (xi == -1 && yi == 1) continue;
                        //  if (xi == 1 && yi == -1) continue;
                        if (j + yi < 0 || j + yi >= (myGrid.Rows)) continue;
                        if (i + xi < 0) continue;

                        if (i + xi >= (myGrid.Columns))
                        {

                            End = new Vector2(i + (xi * 0.5f), j + yi);
                            tempGridlines.Add(CreateLine(insideLine, new Vector2(0, j), new Vector2(0 - (xi * 0.5f), j + yi)));
                        }

                        else
                        {
                            End = new Vector2(i + xi, j + yi);
                        }


                        tempGridlines.Add(CreateLine(insideLine, Start, End));


                    }
                }
            }

        }

        gridLines = tempGridlines.ToArray();

    }


    public static void LineUpdate() // Every frame, go through the list of Lines and update their positions.
    {
        foreach (var Line in MyLines)
        {
            if (Line.p == null)
            {
                MyLines.Remove(Line);
                Line.DeInit();
                return;
            }
            Line.LineUpdate();
        }
    }



    public static LineObject CreateLine(ScriptableLines.LineStyle Style, Vector2 Start, Vector2 End) // Converts two points in "grid space " into a line
    {
        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

        LineObject lp = new LineObject();

        lp.Init(Style);
        //        lp.p.transform.SetParent(LineHolder.transform);
        lp.p.name = "Line: " + Start + " to " + End;
        lp.Positions = new Vector2[Style.NumOfSegments];

        for (int i = 0; i < Style.NumOfSegments; i++)
        {
            if (myGrid.PolarActive)
            {
                Vector2 NewStart = gridSettings.SetPosition(Start.x, Start.y);
                Vector2 NewEnd = gridSettings.SetPosition(End.x, End.y);
                Vector3 Interp = new Vector2(Mathf.Lerp(NewStart.x, NewEnd.x, i / (Style.NumOfSegments - 1.0f)), Mathf.Lerp(NewStart.y, NewEnd.y, (i / (Style.NumOfSegments - 1.0f))));
                lp.Positions[i] = gridSettings.GetPosition(Interp);
            }
            else
            {
                lp.Positions[i] = new Vector2(Mathf.Lerp(Start.x, End.x, (i / (Style.NumOfSegments - 1.0f))), Mathf.Lerp(Start.y, End.y, (i / (Style.NumOfSegments - 1.0f))));
            }
            lp.pp.SetPosition(i, gridSettings.SetPosition(lp.Positions[i].x, lp.Positions[i].y));
        }

        MyLines.Add(lp);
        return lp;
    }

    public static LineObject CreateLine(ScriptableLines.LineStyle Style, Vector2[] Positions) // Converts two points in "grid space " into a line
    {
        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

        LineObject lp = new LineObject();

        lp.Init(Style);
        lp.p.name = "Line: " + Positions[0];
        lp.Positions = new Vector2[Style.NumOfSegments];

        // Get the total distance of lines and add to list of vecs
        List<float> distances = new List<float>();
        float TotalDistance = 0;
        Vector2 TempVector = Positions[0];

        foreach (Vector2 i in Positions)
        {
            TotalDistance += Vector2.Distance(TempVector, i);
            distances.Add(TotalDistance);
            TempVector = i;
        }

        for (int i = 0; i < Style.NumOfSegments; i++)
        {
            float tempLerp = Mathf.Lerp(0, TotalDistance, (i / (Style.NumOfSegments - 1.0f)));
            int j = 0;
            float tempLerp2 = 0;
            for (int i2 = distances.Count - 1; i2 >= 0; i2--)
            {
                if (tempLerp > distances[i2] )
                {
                    j = i2;
                    tempLerp2 = Mathf.InverseLerp(distances[i2], distances[i2 + 1], tempLerp);
                    i2 = 0;
                }
            }

            if (myGrid.PolarActive)
            {
                Vector2 NewStart = gridSettings.SetPosition(Positions[j]);
                Vector2 NewEnd = gridSettings.SetPosition(Positions[j + 1]);
                Vector3 Interp = new Vector2(Mathf.Lerp(NewStart.x, NewEnd.x, tempLerp2), Mathf.Lerp(NewStart.y, NewEnd.y, tempLerp2));
                lp.Positions[i] = gridSettings.GetPosition(Interp);
            }
            else
            {
                lp.Positions[i] = Vector2.Lerp(Positions[j], Positions[j + 1], tempLerp2); // this needs to use a value that isn't i based, since i now covers the whole line
            }
            lp.pp.SetPosition(i, gridSettings.SetPosition(lp.Positions[i].x, lp.Positions[i].y));
        //    Debug.Log(i + " = " +  j+ ": "+ tempLerp2);
        }

        MyLines.Add(lp);
        return lp;
    }


    public static GameObject CreateLine(ScriptableLines.LineStyle Style, Vector3 cStart, Vector3 cEnd) // Converts two points in world space into a line game object, properly segmented
    {

        Vector2 Start = gridSettings.GetPosition(cStart);
        Vector2 End = gridSettings.GetPosition(cEnd);

        return CreateLine(Style, Start, End).p;

    }

}
