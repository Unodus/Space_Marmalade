using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CameraPan : MonoBehaviour
{
    // Camera cam;

    public     InputEventType inputEvent;

//    bool Locked;

  //  public Transform lockedPosition;

    UnityAction<Vector3> unityAction;

 
    public void Start()
    {
       
        EventDictionary.StartListening(ScriptableExtensions.s.scriptable.GameEvents.GetEventByType(inputEvent).Name, unityAction);

    }

    public void LockOn()
    {
        StartCoroutine(transform.LerpLocalPosition(new Vector3(0, 0, -10), 0.25f));
    }

    //IEnumerator LockedOn(Transform i)
    //{
    //    //if (Locked) yield break;
    //    //Locked = true;
    //    lockedPosition = i;

    //    while (Vector3.Magnitude(transform.localPosition) > 0.1f)
    //    {
    //        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero , 0.99f);
    //        yield return null;
    //    }
    //   // Locked = false;
    //    yield return null;
    //}
    public void UpdatePos(Vector3 direction)
    {
        transform.position += direction;
    }
 
    public void OnEnable()
    {
        unityAction += UpdatePos;

      }

    public void OnDisable()
    {
        unityAction -= UpdatePos;
    }
}
