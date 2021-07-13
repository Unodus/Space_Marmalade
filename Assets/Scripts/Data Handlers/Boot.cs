using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{

    [SerializeField]  DialogueRecorder ConsoleLog;

    [SerializeField] ScriptableScenes ScenesToLoadOnBoot;

    List<IEnumerator> StartUpProcesses;
    WaitForEndOfFrame RestFrame;

    [SerializeField] bool GameLoaded;

    void Start()
    {
        StartBoot();
    }


    public void StartBoot()
    {
        LogToConsole("Boot Started");
        StartUpProcesses = new List<IEnumerator>();
        #region
        // Put Startup Enumerators here:

        StartUpProcesses.Add(InitialiseBootVariables());

        // ^^^^^
        #endregion
        StartCoroutine(Startup(StartUpProcesses.ToArray()));
        LogToConsole("Ready");
    }

    public void LoadBuild()
    {
        LogToConsole("Loading Game");
        StartUpProcesses = new List<IEnumerator>();
        #region
        // Put Build Enumerators here:

        StartUpProcesses.Add(LoadBootScenes());


        // ^^^^^
        #endregion
        StartCoroutine(Startup(StartUpProcesses.ToArray()));


    }

    public void UnloadBuild()
    {
        LogToConsole("Unloading Game");
        StartUpProcesses = new List<IEnumerator>();
        #region
        // Put Build Enumerators here:

        StartUpProcesses.Add(UnloadBootScenes());


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

        yield return RestFrame;
        foreach (IEnumerator i in StartUpProcesses)
        {
            StartCoroutine(i);
            yield return RestFrame;
        }
    }





    IEnumerator InitialiseBootVariables()
    {
        RestFrame = new WaitForEndOfFrame();

        GameLoaded = false;

        LogToConsole("Boot Loaded");
        yield return null;
    }
    
    IEnumerator LoadBootScenes()
    {
        if (!GameLoaded) LogToConsole("Completed Load");
        else LogToConsole("Load Failed");


        if (ScenesToLoadOnBoot == null || GameLoaded) yield return null;

        else
        {

            foreach (ScriptableScenes.SceneType i in ScenesToLoadOnBoot.ScenesToLoadOnBoot)
            {
                if (i.SceneName == null) continue;
                SceneManager.LoadScene(i.SceneName, LoadSceneMode.Additive);
            }

            GameLoaded = true;
            yield return null;


        }
    }
    
    IEnumerator UnloadBootScenes()
    {
        if (GameLoaded) LogToConsole("Completed Load");
        else LogToConsole("Load Failed");

        if (ScenesToLoadOnBoot == null || !GameLoaded) yield return null;
        else
        {
            foreach (ScriptableScenes.SceneType i in ScenesToLoadOnBoot.ScenesToLoadOnBoot)
            {
                if (i.SceneName == null) continue;
                SceneManager.UnloadSceneAsync(i.SceneName);
            }
            GameLoaded = false;
            yield return null;

        }
    }
}
