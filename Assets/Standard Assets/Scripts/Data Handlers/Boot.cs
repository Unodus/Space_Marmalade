using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{

    [SerializeField]  DialogueRecorder ConsoleLog;

    List<IEnumerator> StartUpProcesses;
    WaitForEndOfFrame RestFrame;

    [SerializeField] bool GameLoaded;

    void Start()
    {
        StartBoot();
    }


    public void StartBoot()
    {
        LogToConsole(ScriptableExtensions.m_ScriptableStrings.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 1));
        StartUpProcesses = new List<IEnumerator>();
        #region
        // Put Startup Enumerators here:

        StartUpProcesses.Add(InitialiseBootVariables());

        // ^^^^^
        #endregion
        StartCoroutine(Startup(StartUpProcesses.ToArray()));
        LogToConsole(ScriptableExtensions.m_ScriptableStrings.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 2));
    }

    public void LoadBuild()
    {
        LogToConsole(ScriptableExtensions.m_ScriptableStrings.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 3));
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
        LogToConsole(ScriptableExtensions.m_ScriptableStrings.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 4));
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

        LogToConsole(ScriptableExtensions.m_ScriptableStrings.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 5));
        yield return null;
    }
    
    IEnumerator LoadBootScenes()
    {
        if (!GameLoaded) LogToConsole(ScriptableExtensions.m_ScriptableStrings.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 6));
        else LogToConsole(ScriptableExtensions.m_ScriptableStrings.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 7));


        if (GameLoaded) yield return null;

        else
        {

            foreach (ScriptableScenes.SceneType i in ScriptableExtensions.m_ScriptableScenes.ScenesToLoadOnBoot)
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
        if (GameLoaded) LogToConsole(ScriptableExtensions.m_ScriptableStrings.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 6));
        else LogToConsole(ScriptableExtensions.m_ScriptableStrings.GetStringByIdentifier(ScriptableStrings.StringCategories.ConsoleMessages, 7));

        if (!GameLoaded) yield return null;
        else
        {
            foreach (ScriptableScenes.SceneType i in ScriptableExtensions.m_ScriptableScenes.ScenesToLoadOnBoot)
            {
                if (i.SceneName == null) continue;
                SceneManager.UnloadSceneAsync(i.SceneName);
            }
            GameLoaded = false;
            yield return null;

        }
    }
}
