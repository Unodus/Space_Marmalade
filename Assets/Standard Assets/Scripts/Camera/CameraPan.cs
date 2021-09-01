using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CameraPan : MonoBehaviour
{
    // Camera cam;
    [SerializeField]
    ScriptableGameEvents.InputEventType inputEvent = ScriptableGameEvents.InputEventType.MapDrag;

    UnityAction<Vector3> unityAction;

    // Update is called once per frame
    Vector3 dir = Vector3.zero;
    public void UpdatePos(Vector3 direction)
    {
        transform.position += direction;
    }
 
    public void OnEnable()
    {
        unityAction += UpdatePos;
        EventDictionary.StartListening(ScriptableExtensions.s.scriptable.GameEvents.GetEventByType(inputEvent).Name, unityAction);
    }

    public void OnDisable()
    {
        unityAction -= UpdatePos;
        EventDictionary.StopListening(ScriptableExtensions.s.scriptable.GameEvents.GetEventByType(inputEvent).Name, unityAction);
    }
}
