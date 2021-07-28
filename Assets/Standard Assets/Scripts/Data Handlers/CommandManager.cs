using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour
{
    [SerializeField] InputField inputLineField;
    [SerializeField] DialogueRecorder dialogueBox;
    [SerializeField] public ScriptableCheatCodes CheatCodes;
    [SerializeField] ScriptableStrings stringValues;

    [SerializeField] ScriptableSounds soundFiles;
    [SerializeField] ScriptableElias eliasFiles;

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

    public void ProcessCommand(ScriptableCheatCodes.CheatType input)
    {
        dialogueBox.AddLineOfText(stringValues.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, input.CommandMessageReference));

        switch (input.InternalCode)
        {
            case ScriptableCheatCodes.CommandType.None:

                // No Command inputted

                break;
            case ScriptableCheatCodes.CommandType.CheatEnable:


                break;

            default:
                break;
        }


    }

    public void ProcessCommand(ScriptableCheatCodes.CheatType input, string inputString)
    {
        dialogueBox.AddLineOfText(stringValues.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, input.CommandMessageReference) + inputString);

        switch (input.InternalCode)
        {
            case ScriptableCheatCodes.CommandType.None:

                // No Command inputted

                break;
            case ScriptableCheatCodes.CommandType.PlaySound:
                
                break;

            default:
                break;
        }
    }
}
