using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSwitcher : MonoBehaviour
{
    [SerializeField] MyProjector MyGrid;
    [SerializeField] GameObject[] Objects = new GameObject[2];
    [SerializeField] ScriptableElias EliasThemeNames;
    [SerializeField] string[] ThemeNames = new string[2];
   
    bool MoveOrAiming;

    // EliasDemoEventTrigger EliasComponent;

    [SerializeField]
    GameObject EliasObject;
    EliasPlayer EliasComponent;

    void Start()
    {
        MoveOrAiming = true;
        SwitchOpenObject(0);
        if(!EliasComponent)
            EliasObject.TryGetComponent(out EliasComponent);
        SetMove();
    }

    public void Toggle()
    {
        MoveOrAiming = !MoveOrAiming;

        if (MoveOrAiming)
        {
            SetMove();
        }
        else
        {
            SetAim();
        }
    }

    public void SetMove()
    {
        SwitchOpenObject(0);
        MyGrid.ChangeBool(true);
  
    }
    public void SetAim()
    {
        SwitchOpenObject(1);
        MyGrid.ChangeBool(false);
    }


    public void SwitchOpenObject(int SelectedObject)
    {
        for (int i = 0; i < Objects.Length; i++)
        {

            if (i == SelectedObject)

            { Objects[i].SetActive(true); }
            else
            { Objects[i].SetActive(false); }

        }
    }

   
}
