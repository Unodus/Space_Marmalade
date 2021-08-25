using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{

    // Ref to the line Object

    //  public ScriptableLines LineStyles;
    public GameObject LineHolder;
    public List<LineObject> MyLines = new List<LineObject>();
    public ScriptableGrid gridSettings;

    public void LineUpdate() // Every frame, go through the list of Lines and update their positions.
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



    public LineObject CreateLine(ScriptableLines.LineStyle Style, Vector2 Start, Vector2 End) // Converts two points in "grid space " into a line
    {
        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

        LineObject lp = new LineObject();

        lp.Init(Style, gridSettings);
        lp.p.transform.SetParent(LineHolder.transform);
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

    public LineObject CreateLine(ScriptableLines.LineStyle Style, Vector2[] Positions) // Converts two points in "grid space " into a line
    {
        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

        LineObject lp = new LineObject();

        lp.Init(Style, gridSettings);
        lp.p.transform.SetParent(LineHolder.transform);
        lp.p.name = "Line: " + Positions[0];

        lp.Positions = new Vector2[Style.NumOfSegments];


        for (int i = 0; i < Style.NumOfSegments; i++)
        {
            float tempLerp = Mathf.Lerp(0, Positions.Length - 1.01f, (i / (Style.NumOfSegments - 1.0f)));
            int j = (int)Mathf.Floor(tempLerp);
                if (myGrid.PolarActive)
                {
                    Vector2 NewStart = gridSettings.SetPosition(Positions[j]);
                    Vector2 NewEnd = gridSettings.SetPosition(Positions[j + 1]);
                    Vector3 Interp = new Vector2(Mathf.Lerp(NewStart.x, NewEnd.x, i / (Style.NumOfSegments - 1.0f)), Mathf.Lerp(NewStart.y, NewEnd.y, (i / (Style.NumOfSegments - 1.0f))));
                    lp.Positions[i] = gridSettings.GetPosition(Interp);
                }
                else
                {

                    lp.Positions[i] = Vector2.Lerp(Positions[j], Positions[j + 1],  (i / (Style.NumOfSegments - 1.0f))); // this needs to use a value that isn't i based, since i now covers the whole line
                }
                lp.pp.SetPosition(i, gridSettings.SetPosition(lp.Positions[i].x, lp.Positions[i].y));
            
        }

        MyLines.Add(lp);
        return lp;
    }


    public GameObject CreateLine(ScriptableLines.LineStyle Style, Vector3 cStart, Vector3 cEnd) // Converts two points in world space into a line game object, properly segmented
    {

        Vector2 Start = gridSettings.GetPosition(cStart);
        Vector2 End = gridSettings.GetPosition(cEnd);

        return CreateLine(Style, Start, End).p;

    }

}
