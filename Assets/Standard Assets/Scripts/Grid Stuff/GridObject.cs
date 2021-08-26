using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{

    public bool gridVisible;

    
    public ScriptableLines lineStyles;
    public ScriptableGrid gridSettings;
    public ScriptableParticles pointStyles;

  //  public PointManager pointManager;

    [SerializeField]
    ScriptableLines.StyleType outsideStyle, insideStyle;
    [SerializeField]
    ScriptableParticles.Particle point;


    public void Start()
    {

        Init();
    }

    public void Init() // Instantiates a grid of lines that go between each point, including 4 lines along the outside that make up the "border" of the game
    {


       

        PointManager.Init(gridSettings, pointStyles);
        LineManager.Init(gridSettings, lineStyles);


      


        //for (int i = 0; i < (myGrid.Columns - 1); i++)
        //{
        //    float j = Mathf.Lerp(i, i + 1, 0.5f);
        //    Start = new Vector2(j, -0.5f);
        //    End = new Vector2(j, myGrid.Rows - 0.5f);
        //    ColumnLines[i] = lineManager.CreateLine(insideLine, Start, End);

        //}
        //for (int i = 0; i < (myGrid.Rows - 1); i++)
        //{
        //    float j = Mathf.Lerp(i, i + 1, 0.5f);
        //    Start = new Vector2(-0.5f, j);
        //    End = new Vector2(myGrid.Columns - 0.5f, j);

        //    RowLines[i] = lineManager.CreateLine(insideLine, Start, End);

        //}

    }
    public void DeInit() // Removes lines for if the grid changes size
    {
        PointManager.Deinit ();
        LineManager.DeInit();

    }

    public void LateUpdate()
    {
        LineManager.LineUpdate();
        PointManager.PointUpdate();
    }

}
