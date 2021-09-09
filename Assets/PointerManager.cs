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

        ScriptableScripts.ScriptableScript s = ScriptableExtensions.s.scriptable;


        // This stuff needs to be put in the optimized input manager
        ScriptableGrid gridtype = s.Grids;
        ScriptableGrid.GridProfile grid= s.Grids.GameGrid;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gridtype.ChangeBool(!grid.settings.PolarActive);
        }
        if (Input.GetKey(KeyCode.I))
        {
            gridtype.UpdateSize(grid.settings.Size - (1f * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.O))
        {
            gridtype.UpdateSize(grid.settings.Size + (1f * Time.deltaTime));
        }

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

            Debug.Log("Dragging");
            EventDictionary.TriggerEvent(s.GameEvents.GetEventByType(s.Enums.GetEnum(GlobalEnum.InputEvent, 1)).Name, direction);
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
