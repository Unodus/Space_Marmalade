using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostSwitcher : MonoBehaviour
{
    [SerializeField] MyProjector MyGrid;
    [SerializeField] GameObject[] Objects = new GameObject[2];
    [SerializeField] string[] EliasThemeNames = new string[2];
    bool MoveOrAiming;

    EliasDemoEventTrigger EliasComponent;

    void Start()
    {
        MoveOrAiming = true;
        SwitchOpenObject(0);
        TryGetComponent(out EliasComponent);
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
        EliasComponent.actionPresetName = EliasThemeNames[0];
        EliasComponent.UpdateElias();

    }
    public void SetAim()
    {
        SwitchOpenObject(1);
        MyGrid.ChangeBool(false);
        EliasComponent.actionPresetName = EliasThemeNames[1];
        EliasComponent.UpdateElias();
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
