using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBehaviour : MonoBehaviour
{
    public InputEventType onClickType;

    public void OnClick()
    {
        ScriptableScripts.ScriptableScript s = ScriptableExtensions.s.scriptable;

        if (onClickType == s.GameEvents.GetEventByType(s.Enums.GetEnum(GlobalEnum.InputEvent, 3)).InputType ) // Enter Grid
        {
            Debug.Log("Enter!");
        }
    }
}
