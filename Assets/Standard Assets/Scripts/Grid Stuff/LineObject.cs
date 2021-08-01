using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineObject : MonoBehaviour
{
    public ScriptableGrid gridSettings;
    public Vector2[] Positions;
    public GameObject p;
    public LineRenderer pp;

    public void Init(ScriptableLines.LineStyle Style, ScriptableGrid grid)
    {
        gridSettings = grid;

        p = Instantiate(Style.LinePrefab);
        
        pp = p.GetComponent<LineRenderer>();
        
        pp.widthMultiplier = Style.LineWidth;
        pp.startColor = Style.LineColorStart;
        pp.endColor = Style.LineColorEnd;
        pp.positionCount = Style.NumOfSegments;

    }

    public void DeInit()
    {
        Destroy(p);
        Destroy(this);
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
        return;
    }


}
