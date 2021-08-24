using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{

    public bool gridVisible;

    public LineObject[] gridLines;
    public LineObject[] Outlines;
    public ScriptableLines lineStyles;
    public ScriptableGrid gridSettings;

    public LineManager lineManager;

    [SerializeField]
    ScriptableLines.StyleType outsideStyle, insideStyle;


    public void Start()
    {

        Init();
    }

    public void Init() // Instantiates a grid of lines that go between each point, including 4 lines along the outside that make up the "border" of the game
    {
        Vector2 Start;
        Vector2 End;



        ScriptableLines.LineStyle outlineStyle = lineStyles.GetLineStyleFromPalette(outsideStyle);
        ScriptableLines.LineStyle insideLine = lineStyles.GetLineStyleFromPalette(insideStyle);

        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();

        List<LineObject> tempGridlines = new List<LineObject>();
        Outlines = new LineObject[4];


 

        Outlines[0] = lineManager.CreateLine(outlineStyle, new Vector2(-0.5f, -0.5f), new Vector2(-0.5f, myGrid.Rows - 0.5f));
        Outlines[1] = lineManager.CreateLine(outlineStyle, new Vector2(-0.5f, myGrid.Rows - 0.5f), new Vector2(myGrid.Columns - 0.5f, myGrid.Rows - 0.5f));
        Outlines[2] = lineManager.CreateLine(outlineStyle, new Vector2(myGrid.Columns - 0.5f, myGrid.Rows - 0.5f), new Vector2(myGrid.Columns - 0.5f, -0.5f));
        Outlines[3] = lineManager.CreateLine(outlineStyle, new Vector2(myGrid.Columns - 0.5f, -0.5f),  new Vector2(-0.5f, -0.5f));


        for (int i = 0; i < (myGrid.Columns ); i++)
        {

            for (int j = 0; j < (myGrid.Rows ); j++)
            {
              
                Start = new Vector2(i,j);

                for (int xi = 0; xi <= 1; xi++)
                {
                    for (int yi = 0; yi <= 1; yi++)
                    {
                        if (xi == 0 && yi == 0) continue;
                        if (xi == 1 && yi == 1) continue;
                     //   if (xi == -1 && yi == -1) continue;
                      //  if (xi == -1 && yi == 1) continue;
                      //  if (xi == 1 && yi == -1) continue;
                        if (j + yi < 0 || j + yi >= (myGrid.Rows )) continue;
                        if (i + xi < 0) continue;

                        if (i + xi >= (myGrid.Columns))
                        {

                            End = new Vector2(i + (xi * 0.5f), j + yi);
                            tempGridlines.Add(lineManager.CreateLine(insideLine, new Vector2(0, j), new Vector2(0 - (xi * 0.5f), j+ yi)));
                        }

                        else
                        {
                            End = new Vector2(i + xi, j + yi);
                        }


                        tempGridlines.Add( lineManager.CreateLine(insideLine, Start, End));


                    }
                }
            }

        }

        gridLines = tempGridlines.ToArray();


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
        for (int i = 0; i < (gridLines.Length); i++) Destroy(gridLines[i].p);
        for (int i = 0; i < (Outlines.Length); i++) Destroy(Outlines[i].p);
    }

    public void LateUpdate()
    {
        lineManager.LineUpdate();
    }

}
