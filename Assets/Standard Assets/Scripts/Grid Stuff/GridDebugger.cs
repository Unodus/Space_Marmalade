using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDebugger : MonoBehaviour
{
    [SerializeField] ScriptableGrid gridtype;

    ScriptableGrid.GridSettings grid;
    [SerializeField] Color HighlightColor;
    [SerializeField] Sprite testSprite;

    List<GameObject> SpriteNetwork;

    void Start()
    {
        grid = gridtype.GetGridSettings();

        SpriteNetwork = new List<GameObject>();
        for (int i = 0; i < grid.Columns; i++)
        {
            for (int j = 0; j < grid.Rows; j++)
            {
                GameObject newObject = new GameObject();
                newObject.transform.SetParent(transform);
                newObject.transform.localScale = Vector3.one * 0.5f;


                newObject.name = ("point: " + i + "/" + j);
                SpriteRenderer spriteComp = newObject.AddComponent<SpriteRenderer>();

                spriteComp.sprite = testSprite;
                spriteComp.color = HighlightColor;


                SpriteNetwork.Add(newObject);
            }
        }
    }

    void LateUpdate()
    {
        GameObject[] gameObjects = SpriteNetwork.ToArray();
        for (int i = 0; i < grid.Columns; i++)
        {
            for (int j = 0; j < grid.Rows; j++)
            {
                SpriteNetwork[(i * grid.Columns) + (j) ].transform.position = gridtype.SetNormalizedPosition(new Vector2(i, j)) ; 
            }
        }



        if(Input.GetKeyDown(KeyCode.Space))
        {
            gridtype.ChangeBool(!grid.PolarActive);
        }        
        if(Input.GetKey(KeyCode.I))
        {
            gridtype.UpdateSize(grid.Size - (1f * Time.deltaTime));
        }        
        if(Input.GetKey(KeyCode.O))
        {
            gridtype.UpdateSize(grid.Size + (1f * Time.deltaTime));
        }        
    }
}
