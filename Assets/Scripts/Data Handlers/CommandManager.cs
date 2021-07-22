using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour
{
    [SerializeField] InputField inputLineField;
    [SerializeField] DialogueRecorder dialogueBox;
    [SerializeField] ScriptableCheatCodes CheatCodes;
    [SerializeField] ScriptableStrings stringValues;

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

    public void ProcessCommand(ScriptableCheatCodes.CommandType input)
    {
        switch(input)
        {
            case ScriptableCheatCodes.CommandType.None:

                // No Command inputted

                break;
            case ScriptableCheatCodes.CommandType.CheatEnable:

                dialogueBox.AddLineOfText(stringValues.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 0));
                break;

            default:
                break;
        }
    }

}
