using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDebugger : MonoBehaviour
{
    public ScriptableGrid gridtype;

    ScriptableGrid.GridProfile grid;

     GridObject ThisGrid;
//    public Color HighlightColor;
 //   public Sprite testSprite;

  //  List<GameObject> SpriteNetwork;

    void Awake()
    {
        grid = gridtype.GetGridSettings();
        grid.settings.GameObjectRef = gameObject;
        grid.settings.PolarActive = false;
        ThisGrid = grid.gridObject;
        ThisGrid.Init();
        //SpriteNetwork = new List<GameObject>();
        //for (int i = 0; i < grid.Columns; i++)
        //{
        //    for (int j = 0; j < grid.Rows; j++)
        //    {
        //        GameObject newObject = new GameObject();
        //        newObject.transform.SetParent(transform);
        //        newObject.transform.localScale = Vector3.one * 0.5f;


        //        newObject.name = ("point: " + i + "/" + j);
        //        SpriteRenderer spriteComp = newObject.AddComponent<SpriteRenderer>();

        //        spriteComp.sprite = testSprite;
        //        spriteComp.color = HighlightColor;


        //        SpriteNetwork.Add(newObject);
        //    }
        //}
    }
    private void OnDestroy()
    {
        ThisGrid.DeInit();
        grid.settings.GameObjectRef = null;
    }
    void LateUpdate()
    {
        //GameObject[] gameObjects = SpriteNetwork.ToArray();
        //for (int i = 0; i < grid.Columns; i++)
        //{
        //    for (int j = 0; j < grid.Rows; j++)
        //    {
        //        SpriteNetwork[(i * grid.Columns) + (j) ].transform.position = gridtype.SetNormalizedPosition(new Vector2(i, j)) ; 
        //    }
        //}

        ThisGrid.GridUpdate();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            gridtype.ChangeBool(!grid.settings.PolarActive);
        }        
        if(Input.GetKey(KeyCode.I))
        {
            gridtype.UpdateSize(grid.settings.Size - (1f * Time.deltaTime));
        }        
        if(Input.GetKey(KeyCode.O))
        {
            gridtype.UpdateSize(grid.settings.Size + (1f * Time.deltaTime));
        }        
    }
}
