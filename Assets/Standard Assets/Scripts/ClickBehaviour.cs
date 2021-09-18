using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBehaviour : MonoBehaviour
{
    public InputEventType onClickType;
    public List<Object> myObject = new List<Object>();

    public void Start()
    {
        if (TryGetComponent(out SpriteRenderer sprite))
        {
            sprite.color = Random.ColorHSV();
        }
    }

    public void OnClick()
    {
        ScriptableScripts.ScriptableScript s = ScriptableExtensions.s.scriptable;

        if (onClickType == s.GameEvents.GetEventByType(s.Enums.GetEnum(GlobalEnum.InputEvent, 3)).InputType) // Enter Grid
        {

            if (myObject.Count == 1)
            {
                if (myObject[0] is UniverseObject)
                {
                    UniverseObject newUniverse = (UniverseObject)myObject[0];


                    CameraPan cameraPan = FindObjectOfType<CameraPan>();
                    if (cameraPan != null)
                    {
                        cameraPan.transform.SetParent(transform);
                        cameraPan.LockOn();
                    }


                    StartCoroutine(UniverseManager.UniverseScript.ZoomFarChange(newUniverse, 50.0f, 1f, 1));

                }
            }
        }
    }
}
