using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject 
{
    public enum NodeMode // Each point can have three states- whether it's empty, able to be selected, or has a ship on it
    {
        Empty,
        Selectable,
        Occupied
    }

    public ScriptableGrid gridSettings;
    public Vector2 Pos;
    public GameObject p;
    public NodeMode Mode;

    ParticleSystem Component;

    public void Init( Vector2 Position, ScriptableGrid grid)
    {
        Pos = Position;
        gridSettings = grid;
    }
    public void DeInit()
    {
        Object.Destroy(p);
    }
    public void PointUpdate()
    {
        float MovementTime = Vector3.Distance(p.transform.position, gridSettings.SetPosition(Pos)) * gridSettings.GameGrid.TransitionSpeed * Time.deltaTime;

        Vector3 TargetPosition = Vector3.MoveTowards(p.transform.position, gridSettings.SetPosition(Pos), MovementTime);
        Vector3 TargetSize = Vector3.one * gridSettings.GameGrid.Size * 0.1f;
        p.transform.localScale = TargetSize;
        p.transform.position = TargetPosition;

    }

    public void ChangeNodeType(NodeMode newMode) // Used to calculate whether a node on the map is selectable
    {
        Mode = newMode;
        ParticleSystem.MainModule settings = Component.main;
        if (Mode  == NodeMode.Empty)
        {
            settings.startColor = new Color(1, 1, 1, 0.5f);
        }
        else if (Mode == NodeMode.Occupied)
        {
            settings.startColor = Color.red;
        }
        else if (Mode == NodeMode.Selectable)
        {
            settings.startColor = Color.white;
        }

    }


}
