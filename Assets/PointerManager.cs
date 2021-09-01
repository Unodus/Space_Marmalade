using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerManager : MonoBehaviour
{
    private Vector3 touchStart;

    public float groundZ = 0;


    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = GetWorldPosition(groundZ);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - GetWorldPosition(groundZ);
            EventDictionary.TriggerEvent(ScriptableExtensions.s.scriptable.GameEvents.GetEventByType(ScriptableGameEvents.InputEventType.MapDrag).Name, direction);
        }
    }

    private Vector3 GetWorldPosition(float z)
    {
        Camera cam = Camera.main;
        if (cam == null) return Vector3.zero;

        Ray mousePos = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
}
