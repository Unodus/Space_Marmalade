using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Door", menuName = "ScriptableObject/Door")]
public class ScriptableDoor : ScriptableObject
{
    public enum DoorState
    {
        Closed,
        Moving,
        Open
    }

    public enum LockState
    {
        Locked,
        Unlocked
    }

    public DoorState doorState;
    public LockState lockState;
    public float transitionTime;
}

public static class StaticDoorFunctions
{
    public static void Interact(this ScriptableDoor i)
    {
        if (i.doorState == ScriptableDoor.DoorState.Closed) i.OpenDoor();
        if (i.doorState == ScriptableDoor.DoorState.Open) i.CloseDoor();
    }

    public static void ToggleLock(this ScriptableDoor i)
    {
        if (i.lockState == ScriptableDoor.LockState.Locked) i.Lock();
        if (i.lockState == ScriptableDoor.LockState.Unlocked) i.Unlock();
    }

    public static void Lock(this ScriptableDoor i) { i.lockState = ScriptableDoor.LockState.Locked; }

    public static void Unlock(this ScriptableDoor i) { i.lockState = ScriptableDoor.LockState.Unlocked; }

    public static void OpenDoor(this ScriptableDoor i) { MoveDoor(i, ScriptableDoor.DoorState.Open); }

    public static void CloseDoor(this ScriptableDoor i) { MoveDoor(i, ScriptableDoor.DoorState.Closed); }

    public static bool CanMove(this ScriptableDoor i)
    {
        if (i.lockState == ScriptableDoor.LockState.Locked || i.doorState == ScriptableDoor.DoorState.Moving) return false;
        return true;
    }

    private static void MoveDoor(ScriptableDoor i, ScriptableDoor.DoorState EndState)
    {
        if (!i.CanMove()) return;
        DoorInstance doorInstance = new DoorInstance();
        doorInstance.MoveDoor(i, EndState);
    }
}

public class DoorInstance : MonoBehaviour
{

    public void MoveDoor(ScriptableDoor i, ScriptableDoor.DoorState EndState)
    {
        i.doorState = ScriptableDoor.DoorState.Moving;
        StartCoroutine(MovingDoor(i, EndState));
    }


    private IEnumerator MovingDoor(ScriptableDoor i, ScriptableDoor.DoorState endState)
    {
        yield return new WaitForSeconds(i.transitionTime);
        i.doorState = endState;
        yield return null;
    }
}
