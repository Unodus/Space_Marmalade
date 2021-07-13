using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour
{

    [SerializeField]  DialogueRecorder ConsoleLog;
    List<IEnumerator> StartUpProcesses;
    WaitForEndOfFrame RestFrame;

    void Start()
    {
        
        StartUpProcesses = new List<IEnumerator>();
        #region
        // Put Startup Enumerators here:

        StartUpProcesses.Add(InitialiseBootVariables());
        
        // ^^^^^
        #endregion
        StartCoroutine(Startup(StartUpProcesses.ToArray()));
    }

    public void LogToConsole(string LogThis)
    {
        if (ConsoleLog == null) return;
        ConsoleLog.AddLineOfText(LogThis);
    }

    IEnumerator Startup(IEnumerator[] StartUpProcesses)
    {
        LogToConsole("Boot Started");
        yield return RestFrame;
        foreach (IEnumerator i in StartUpProcesses)
        {
            StartCoroutine(i);
            yield return RestFrame;
        }
        LogToConsole("Ready");
    }

    IEnumerator InitialiseBootVariables()
    {
        RestFrame = new WaitForEndOfFrame();
        LogToConsole("Boot Loaded");
        yield return null;
    }
}
