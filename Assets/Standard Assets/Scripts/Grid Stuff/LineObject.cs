using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineObject 
{
    //    public ScriptableGrid gridSettings;
    public UnityAction LineUpdateEvent;

    public Vector2[] Positions;
    public GameObject p;
    public LineRenderer pp;
    public Color[] lineColor = new Color[2];
    public bool wrap;

    public LineObject()
    {
        OnEnable();

        ScriptableScripts.ScriptableScript s = ScriptableExtensions.s.scriptable;
        EventDictionary.StartListening(s.GameEvents.GetEventByType(s.Enums.GetEnum(GlobalEnum.InputEvent, 2)).Name, LineUpdateEvent);
    }

    public void OnEnable()
    {
        LineUpdateEvent += LineUpdate;
    }
    public void OnDisable()
    {
        LineUpdateEvent -= LineUpdate;
    }
    public void Init(ScriptableLines.LineStyle Style)
    {
    
        ScriptableGrid gridSettings = ScriptableExtensions.s.scriptable.Grids;
        p = Object.Instantiate(Style.LinePrefab, GridManager.currentGrid.transform);
        pp = p.GetComponent<LineRenderer>();
        pp.widthMultiplier = Style.LineWidth;

        lineColor[0] = Style.LineColorStart;
        lineColor[1] = Style.LineColorEnd;
        pp.startColor = lineColor[0];
        pp.endColor = lineColor[1];

        pp.positionCount = Style.NumOfSegments;
        wrap = Style.Wrap;

        
    }

    public void DeInit()
    {
        OnDisable();
    }

    public void LineUpdate()
    {
        if (p == null)
        {
            DeInit();
            return;
        }


        ScriptableGrid.GridSettings MyGrid = ScriptableExtensions.s.scriptable.Grids.GetGridSettings();

        for (int i = 0; i < pp.positionCount; i++)
        {
            Vector3 checkedPosition = ScriptableExtensions.s.scriptable.Grids.SetPosition(new Vector2(Positions[i].x, Positions[i].y), wrap);
            float checkedDistance = Vector3.Distance(pp.GetPosition(i), ScriptableExtensions.s.scriptable.Grids.SetPosition(new Vector2(Positions[i].x, Positions[i].y), wrap));
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
