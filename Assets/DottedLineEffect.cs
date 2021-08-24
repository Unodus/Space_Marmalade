using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLineEffect : MonoBehaviour
{

    void ResizeDot()
    {

if (!        TryGetComponent(out LineRenderer lineRenderer)) return;
        // width is the width of the line
        float width = lineRenderer.startWidth;
        lineRenderer.material.mainTextureScale = new Vector2(1f / width, 1.0f);
        // 1/width is the repetition of the texture per unit (thus you can also do double
        // lines)
    }
    private void OnDrawGizmos()
    {
        ResizeDot();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ResizeDot();
    }
}
