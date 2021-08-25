using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{

    public ScriptableParticles PointPrefabs;
    public ScriptableParticles.Particle PointStyle;
    public List<PointObject> MyPoints = new List<PointObject>();
    public ScriptableGrid gridSettings;

    int NumOfPoints;

    public void Start()
    {
        InitiatePoints();
    }
    public void PointUpdate() // Every frame, go through the list of Lines and update their positions.
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

    public PointObject CreatePoint(Vector2 Position) // Converts two points in "grid space " into a line
    {
        PointObject i = new PointObject();
        return i;
    }

    public PointObject GetRandomPoint()
    {
        return GetRandomPoints(1)[0];
    }

    public PointObject[] GetRandomPoints(int NumberOfPoints)
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

    void InitiatePoints() // Instantiate Point gameobjects, based on GridMath data 
    {
        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

        NumOfPoints = myGrid.Columns * myGrid.Rows;

        for (int i = 0; i < myGrid.Columns; i++)
        {

            PointObject pp;
            for (int j = 0; j < myGrid.Rows; j++)
            {
                pp = new PointObject
                {
                    Pos = new Vector2(i, j),
                    p = Instantiate(PointPrefabs.GetParticleByName(PointStyle).ParticlePrefab) 

                };
                pp.ChangeNodeType(PointObject.NodeMode.Empty);
                pp.p.transform.SetParent(transform);
                pp.p.name = i + ", " + j;
                pp.p.tag = "Point";

                pp.p.transform.position = gridSettings.SetPosition(i, j);

                Vector3 Displacement = new Vector3((2 * ((1.25f + myGrid.Columns) / myGrid.Columns)) * myGrid.Size * myGrid.ScreenRatio.x, 0, 0);

                MyPoints.Add(pp);
            }
        }
    }

    void ResetPoints()
    {
        DeinitialisePoints();
        InitiatePoints();
    }
    void DeinitialisePoints() // When a grid is resized/deleted, this function clears the gameobjects that have been instantiated
    {
        foreach (var p in MyPoints)
        {
            p.DeInit();
        }
        MyPoints.Clear();

    }

    void HighlightNodes(Vector3 ObjectPosition, float Range)
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

    PointObject[] NodesInRange(Vector3 ObjectPosition, float Range) // Makes nodes within a certain range selectable
    {
        List<PointObject> returnNodes = new List<PointObject>();

        foreach (PointObject p in MyPoints)
        {
            if (Vector3.Distance(p.p.transform.position, ObjectPosition) < Range) returnNodes.Add(p);
        }
        return returnNodes.ToArray();
    }
    PointObject[] NodesInRange(Vector3 ObjectPosition, float Range, PointObject.NodeMode nodeType) // Makes nodes within a certain range selectable
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
    PointObject SearchNodes(PointObject[] nodes, Vector3 ObjectPosition, bool ClosestOrFurthest)
    {
        PointObject closestNode = nodes[0];     
        if(ClosestOrFurthest)
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
    PointObject GetClosestNode(Vector3 ObjectPosition, float CutOff) // Function for finding the closest node, usually for when a node has been clicked
    {
        PointObject[] nodes = NodesInRange(ObjectPosition, CutOff);
        if (nodes.Length == 0) return null;
        return SearchNodes(nodes, ObjectPosition, true);
    }
    PointObject GetClosestNode(Vector3 ObjectPosition, float CutOff, PointObject.NodeMode nodeType) // Function for finding the closest node, usually for when a node has been clicked
    {
        PointObject[] nodes = NodesInRange(ObjectPosition, CutOff, nodeType);
        if (nodes.Length == 0) return null; 
        return SearchNodes(nodes, ObjectPosition, true);
    }
    PointObject GetFurthestNode(Vector3 ObjectPosition, float CutOff) // Function for finding the furest node, usually for ships trying to flee
    {
        PointObject[] nodes = NodesInRange(ObjectPosition, CutOff);
        if (nodes.Length == 0) return null;
        return SearchNodes(nodes, ObjectPosition, false);
    }
    PointObject GetFurthestNode(Vector3 ObjectPosition, float CutOff, PointObject.NodeMode nodeType) // Function for finding the furest node, usually for ships trying to flee
    {
        PointObject[] nodes = NodesInRange(ObjectPosition, CutOff, nodeType);
        if (nodes.Length == 0) return null;
        return SearchNodes(nodes, ObjectPosition, false);
    }

}
