using UnityEngine;

public class PostSwitcher : MonoBehaviour
{
    public GameObject[] Objects = new GameObject[2];
    public ScriptableElias EliasThemeNames;
    public string[] ThemeNames = new string[2];

    bool MoveOrAiming;

    // EliasDemoEventTrigger EliasComponent;

    public
    GameObject EliasObject;
    EliasPlayer EliasComponent;

    void Start()
    {
        MoveOrAiming = true;
        SwitchOpenObject(0);
        if (!EliasComponent)
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
//        GridExtensions.ChangeBool(true);

    }
    public void SetAim()
    {
        SwitchOpenObject(1);
  //      GridExtensions.ChangeBool(false);
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
