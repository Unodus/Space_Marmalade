using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PointManager
{

     static ScriptableParticles PointPrefabs;
     static ScriptableGrid gridSettings;
     static ScriptableParticles.Particle PointStyle;
     static List<PointObject> MyPoints = new List<PointObject>();

    static int NumOfPoints;

    
    public static void PointUpdate() // Every frame, go through the list of Lines and update their positions.
    {
        foreach (var Point in MyPoints)
        {
            if (Point.p == null)
            {
                MyPoints.Remove(Point);
                Point.DeInit();
                return;
            }
            Point.PointUpdate();
        }
    }

    static public PointObject CreatePoint(Vector2 Position) // Converts two points in "grid space " into a line
    {
        PointObject i = new PointObject();
        return i;
    }

    static public PointObject GetRandomPoint()
    {
        return GetRandomPoints(1)[0];
    }

    static public PointObject[] GetRandomPoints(int NumberOfPoints)
    {

        int RandomNumber;

        var TempList = new List<PointObject>(MyPoints);
        var ReturnList = new List<PointObject>();

        for (int i = 0; i < NumberOfPoints; i++)
        {
            RandomNumber = Random.Range(0, TempList.Count);
            ReturnList.Add(TempList[RandomNumber]);
            TempList.RemoveAt(RandomNumber);

        }

        return ReturnList.ToArray();

    }

    static public void Init() // Instantiate Point gameobjects, based on GridMath data 
    {
        PointPrefabs = ScriptableExtensions.m_ScriptableParticles;
        gridSettings = ScriptableExtensions.m_ScriptableGrid;
        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

        NumOfPoints = myGrid.Columns * myGrid.Rows;

        for (int i = 0; i < myGrid.Columns; i++)
        {

            // PointObject pp;
            for (int j = 0; j < myGrid.Rows; j++)
            {
                PointObject pp = new PointObject();
                pp.Init(new Vector2(i, j ), PointPrefabs.GetParticleByName(PointStyle).ParticlePrefab, gridSettings);
        //        pp.p.transform.SetParent(transform);
                MyPoints.Add(pp);

                //       Vector3 Displacement = new Vector3((2 * ((1.25f + myGrid.Columns) / myGrid.Columns)) * myGrid.Size * myGrid.ScreenRatio.x, 0, 0);


            }
        }
    }

    static void ResetPoints()
    {
        Deinit();
        Init();
    }
    public static void Deinit() // When a grid is resized/deleted, this function clears the gameobjects that have been instantiated
    {
        foreach (var p in MyPoints)
        {
            p.DeInit();
        }
        MyPoints.Clear();

    }

    static void HighlightNodes(Vector3 ObjectPosition, float Range)
    {
        foreach (PointObject p in MyPoints)
        {
            if (p.Mode == PointObject.NodeMode.Selectable) p.ChangeNodeType(PointObject.NodeMode.Empty);
        }

        PointObject[] nodes = NodesInRange(ObjectPosition, Range);

        foreach (PointObject i in nodes)
        {
            i.ChangeNodeType(PointObject.NodeMode.Selectable);
        }
    }

    static PointObject[] NodesInRange(Vector3 ObjectPosition, float Range) // Makes nodes within a certain range selectable
    {
        List<PointObject> returnNodes = new List<PointObject>();

        foreach (PointObject p in MyPoints)
        {
            if (Vector3.Distance(p.p.transform.position, ObjectPosition) < Range) returnNodes.Add(p);
        }
        return returnNodes.ToArray();
    }
    static PointObject[] NodesInRange(Vector3 ObjectPosition, float Range, PointObject.NodeMode nodeType) // Makes nodes within a certain range selectable
    {
        List<PointObject> returnNodes = new List<PointObject>();

        foreach (PointObject p in MyPoints)
        {
            if (p.Mode == nodeType)
            {
                if (Vector3.Distance(p.p.transform.position, ObjectPosition) < Range) returnNodes.Add(p);
            }
        }
        return returnNodes.ToArray();
    }
    static PointObject SearchNodes(PointObject[] nodes, Vector3 ObjectPosition, bool ClosestOrFurthest)
    {
        PointObject closestNode = nodes[0];
        if (ClosestOrFurthest)
        {
            foreach (var node in nodes)
            {
                if (Vector3.Distance(ObjectPosition, node.p.transform.position) < Vector3.Distance(ObjectPosition, closestNode.p.transform.position)) closestNode = node;
            }
        }
        else
        {
            foreach (var node in nodes)
            {
                if (Vector3.Distance(ObjectPosition, node.p.transform.position) > Vector3.Distance(ObjectPosition, closestNode.p.transform.position)) closestNode = node;
            }
        }
        return closestNode;

    }
    static PointObject GetClosestNode(Vector3 ObjectPosition, float CutOff) // Function for finding the closest node, usually for when a node has been clicked
    {
        PointObject[] nodes = NodesInRange(ObjectPosition, CutOff);
        if (nodes.Length == 0) return null;
        return SearchNodes(nodes, ObjectPosition, true);
    }
    static PointObject GetClosestNode(Vector3 ObjectPosition, float CutOff, PointObject.NodeMode nodeType) // Function for finding the closest node, usually for when a node has been clicked
    {
        PointObject[] nodes = NodesInRange(ObjectPosition, CutOff, nodeType);
        if (nodes.Length == 0) return null;
        return SearchNodes(nodes, ObjectPosition, true);
    }
    static PointObject GetFurthestNode(Vector3 ObjectPosition, float CutOff) // Function for finding the furest node, usually for ships trying to flee
    {
        PointObject[] nodes = NodesInRange(ObjectPosition, CutOff);
        if (nodes.Length == 0) return null;
        return SearchNodes(nodes, ObjectPosition, false);
    }
    static PointObject GetFurthestNode(Vector3 ObjectPosition, float CutOff, PointObject.NodeMode nodeType) // Function for finding the furest node, usually for ships trying to flee
    {
        PointObject[] nodes = NodesInRange(ObjectPosition, CutOff, nodeType);
        if (nodes.Length == 0) return null;
        return SearchNodes(nodes, ObjectPosition, false);
    }

}
