using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour
{
    [SerializeField] InputField inputLineField;
    [SerializeField] DialogueRecorder dialogueBox;
    [SerializeField] ScriptableCheatCodes CheatCodes;

    public void AddLineFromInput()
    {
        if (inputLineField == null) return;
        if (dialogueBox == null) return;
        
        dialogueBox.AddLineOfText(inputLineField.text);

        string code = inputLineField.text;
        inputLineField.text = "";

        if (CheatCodes == null) return;

        ProcessCommand(CheatCodes.GetCodeByName(code));


    }

    public void ProcessCommand(int input)
    {
        switch(input)
        {
            case 0:

                // No Command inputted

                break;
            case 1:

                dialogueBox.AddLineOfText("Cheat Codes enabled");
                break;

            default:
                break;
        }
    }

}
