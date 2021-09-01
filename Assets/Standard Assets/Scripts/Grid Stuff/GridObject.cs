using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public void Start()
    {
        this.Init();
    }
    public void OnDestroy()
    {
        this.DeInit();
    }
    public void LateUpdate()
    {
        this.GridUpdate();
    }
}
