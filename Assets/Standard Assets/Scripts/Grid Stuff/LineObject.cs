using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineObject 
{
    public ScriptableGrid gridSettings;
    public Vector2[] Positions;
    public GameObject p;
    public LineRenderer pp;
    public Color[] lineColor = new Color[2];

    public void Init(ScriptableLines.LineStyle Style)
    {
        gridSettings = ScriptableExtensions.s.scriptable.Grids;
        p = Object.Instantiate(Style.LinePrefab, GridManager.currentGrid.transform);
        pp = p.GetComponent<LineRenderer>();
        pp.widthMultiplier = Style.LineWidth;

        lineColor[0] = Style.LineColorStart;
        lineColor[1] = Style.LineColorEnd;
        pp.startColor = lineColor[0];
        pp.endColor = lineColor[1];

        pp.positionCount = Style.NumOfSegments;


    }

    public void DeInit()
    {
        Object.Destroy(p);
    }

    public void LineUpdate()
    {

        ScriptableGrid.GridSettings MyGrid = gridSettings.GetGridSettings();

        for (int i = 0; i < pp.positionCount; i++)
        {
            Vector3 checkedPosition = gridSettings.SetPosition(Positions[i].x, Positions[i].y);
            float checkedDistance = Vector3.Distance(pp.GetPosition(i), gridSettings.SetPosition(Positions[i].x, Positions[i].y));
            Vector3 FinalPosition = Vector3.MoveTowards(pp.GetPosition(i), checkedPosition, checkedDistance * MyGrid.TransitionSpeed * Time.deltaTime);
            pp.SetPosition(i, FinalPosition);
        }

        float StartMouseDistance = Vector3.Distance(Input.mousePosition, Positions[0]);
        float EndMouseDistance = Vector3.Distance(Input.mousePosition, Positions[pp.positionCount -1]);
        if (StartMouseDistance > 1 && EndMouseDistance > 1) return;
    //    pp.startColor = Color.white; // Color.Lerp(Color.red, Color.clear, StartMouseDistance );
      //  pp.endColor = Color.white;// Color.Lerp(Color.red, Color.clear, EndMouseDistance );
        

        

        return;
    }


}
