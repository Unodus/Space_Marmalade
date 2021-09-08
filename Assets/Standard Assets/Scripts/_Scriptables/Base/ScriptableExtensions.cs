using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptableExtensions
{
    #region StaticScriptables

    public static ScriptableScripts s;


    static ScriptableExtensions()
    {
        s = ScriptableSingleton.Instance.scriptableScripts;
    }

    #endregion

    #region Scriptable Retrieval

    public static ScriptableObject GetEnum(this ScriptableEnums a, GlobalEnum globalEnum, int index)
    {
        foreach (ScriptableEnums.ScriptableEnum i in a.enums)
        {
            if (globalEnum == i.EnumName) return i.Enum[index];
        }

        Debug.Log("No Enum found for " + globalEnum.ToString() + " at " + index);
        return null;
    }

    public static ScriptableGameEvents.TurnEventSettings GetEventByName(ScriptableGameEvents a, string myName)
    {
        foreach (ScriptableGameEvents.TurnEventSettings i in a.TurnEvents)
        {
            if (i.Name == myName) return i;

        }

        Debug.LogWarning(myName + " is not registered in the profiler");
        return null;
    }
    public static ScriptableGameEvents.TurnEventSettings GetEventByPhase(this ScriptableGameEvents a, ScriptableObject myEnum)
    {
        foreach (ScriptableGameEvents.TurnEventSettings i in a.TurnEvents)
        {
            if (i.turnPhase == myEnum) return i;
        }
        Debug.LogWarning(myEnum + " is not registered in the profiler");
        return null;
    }
    public static ScriptableGameEvents.InputEventSettings GetEventByType(this ScriptableGameEvents a, ScriptableObject myEnum)
    {

        foreach (ScriptableGameEvents.InputEventSettings i in a.InputEvents)
        {

            if (i.InputType == myEnum) return i;
        }

        Debug.LogWarning(myEnum + " is not registered in the profiler");
        return null;
    }


    public static ParticleObject GetParticle(this ScriptableParticles a, ParticleObject myName)
    {
        foreach (ScriptableParticles.ParticleSettings i in a.Particles)
        {
            if (i.particle == myName)
            {
                return i.particle;
            }
        }

        Debug.LogWarning(myName + " is not registered in the profiler");
        return null;
    }

    public static ParticleObject GetParticleByInt(this ScriptableParticles a, int particleRef)
    {
        return a.Particles[particleRef].particle;
    }
    public static ScriptableLines.LineStyle GetLineStyleFromPalette(this ScriptableLines a, ScriptableLines.StyleType style)
    {
        foreach (ScriptableLines.LineStyle i in a.LineTemplates)
        {
            if (i.StyleName == style)
            {
                ScriptableLines.LineStyle returnLine = i;
                return returnLine;
            }
        }
        return null;
    }

    public static ScriptableSounds.SoundPalette GetSoundFromPalette(this ScriptableSounds a, ScriptableSounds.GameSounds soundName)
    {
        ScriptableSounds.SoundPalette returnSound = new ScriptableSounds.SoundPalette();

        foreach (ScriptableSounds.SoundGroup i in a.SoundGroups)
        {
            foreach (ScriptableSounds.SoundPalette j in i.SoundFiles)
            {
                if (j.Name == soundName)
                {
                    returnSound = j;
                    returnSound.volume = Mathf.Lerp(0, i.GroupVolume, returnSound.volume);
                    return returnSound;
                }
            }
        }
        return returnSound;
    } // finds a sound by an enum and returns it

    //todo - reimplemen this!

    public static CheatType GetCodeByName(this ScriptableCheatCodes a, string Code)
    {
        foreach (ScriptableCheatCodes.CommandType i in a.Commands)
        {
            if (i.Name == Code)
            {
                return i.InternalCode;
            }
        }
        return null;
    }

    public static EliasObject GetEliasName(this ScriptableElias a, string myTheme)
    {
        foreach (ScriptableElias.EliasPalettes i in a.eliasPalette)
        {
            if (i.Name == myTheme)
            {
                return i.eliasObject;
            }
        }

        Debug.LogWarning("No Theme Detected");
        return null;
    }

    public static ScriptableFont.FontClass GetStyleByCategory(this ScriptableFont a, ScriptableFont.FontCategories myFontCategory)
    {
        foreach (ScriptableFont.FontCategory i in a.fontPalette)
        {
            if (i.myCategory == myFontCategory) return i.myFonts;
        }
        return null;
    }

    #endregion

    #region Grid Extensions

    //public static void LoadGridFromUniverse(this ScriptableGrid a, UniverseObject universe)
    //{
    //    a.Deinit();
    //    a.GameGrid = universe.GetGridFromUniverse();
    //    a.Init();
    //}

    //public static ScriptableGrid.GridSettings GetGridFromUniverse(this UniverseObject universe)
    //{
    //    return universe.Grid;
    //}

    public static ScriptableGrid.GridProfile GetGridSettings(this ScriptableGrid a)
    {
        return a.GameGrid;
    }


    public static void Init(this ScriptableGrid i) // Gets grid ready for instantiating lines
    {
        ScriptableGrid.GridProfile GridValues = i.GetGridSettings();
        GridValues.settings.Size = 1;
        i.SetRatio();

        //  Grid = new Vector3[GridMath.Columns, GridMath.Rows];

        //for (int i = 0; i < GridMath.Columns; i++)
        //{
        //    for (int j = 0; j < GridMath.Rows; j++)
        //    {
        //        SetPosition(i, j);
        //    }
        //}
        //UpdateSize(GridMath.Size);

    }

    static void Deinit(this ScriptableGrid i) // every time the grid changes size, it needs to get rid of the previous grid
    {
        //        LineMaker.DeSpawnGrid();
    }


    static public void ChangeGridSize(this ScriptableGrid i, int newX, int newY)//update width & height of grid
    {
        if (newX <= 1 || newY <= 1)
            return;

        GridObject gridSettings = i.GetGridSettings().gridObject;
        gridSettings.Columns = newX;
        gridSettings.Rows = newY;
    }

    static public void ChangeBool(this ScriptableGrid i, bool newBool)// switch between polar and grid mode
    {

        GridSettings gridSettings = i.GetGridSettings().settings;
        gridSettings.PolarActive = newBool;
    }


    static public void UpdateSize(this ScriptableGrid i, Single newSize) // changes grid zoom size
    {
        GridSettings gridSettings = i.GetGridSettings().settings;
        gridSettings.Size = newSize;
    }




    // This probably needs revising!
    static Vector3 SetPosition(this ScriptableGrid i, int newx, int newy) // Plots a point on the Grid
    {
        GridSettings gridSettings = i.GetGridSettings().settings;
        return i.SetPosition(new Vector2(newx, newy), false);
    }


    public static Vector3 SetPosition(this ScriptableGrid a, in Vector2 position, bool Wrap)
    {
        GridSettings i = a.GetGridSettings().settings;
        GridObject j = a.GetGridSettings().gridObject;

        Vector2 Pos = position;
        if (Wrap)
        {

            float ModifiedX = i.ColDisplacement + position.x;
            ModifiedX += 0.5f; // this is kind of a magic number to make the grid centre at 0

            float Max = j.Columns;
            if (ModifiedX > 0) ModifiedX = ModifiedX % Max;
            else ModifiedX = Max - ((ModifiedX * -1) % Max);
            Pos = new Vector2(ModifiedX, position.y);
        }

        return a.SetNormalizedPosition(Pos) * i.Size;
    }

    static Vector3 SetPosition(this ScriptableGrid a, float x, float y)
    {
        GridSettings gridSettings = a.GetGridSettings().settings;

        Vector2 Pos = new Vector2(x, y);
        Pos = a.CalculateRatio(Pos);

        if (gridSettings.PolarActive) return a.CoordtoPolar(Pos);
        else return a.CoordtoGrid(Pos) * 0.5f;

    }

    public static Vector3 SetNormalizedPosition(this ScriptableGrid a, in Vector2 position)
    {
    
        GridSettings j = a.GetGridSettings().settings;

        if (j.PolarActive)
        {
            if (j.CachedPolarPositions.TryGetValue(key: position, value: out Vector2 __result)) return __result;            //If the dictionary contains an entry with key in it returns the found entry.
            j.CachedPolarPositions.Add(key: position, value: a.SetPosition(position.x, position.y));            //If not, it adds it and returns the result.
            return j.CachedPolarPositions[key: position];
        }
        else
        {
            if (j.CachedGridPositions.TryGetValue(key: position, value: out Vector2 __result)) return __result;            //If the dictionary contains an entry with key in it returns the found entry.
            j.CachedGridPositions.Add(key: position, value: a.SetPosition(position.x, position.y));            //If not, it adds it and returns the result.
            return j.CachedGridPositions[key: position];
        }
    }



    public static Vector2 GetPosition(this ScriptableGrid a, Vector3 CurrentPosition)// get the position on a grid from a worldspace coordinate
    {
        GridSettings gridSettings = a.GetGridSettings().settings;

        Vector2 Pos;
        if (gridSettings.PolarActive)
        {
            Pos = a.PolartoCoord(CurrentPosition);
            Pos = a.UnCalcluatePolarRatio(Pos);
        }
        else
        {
            CurrentPosition *= 2;
            Pos = a.GridtoCoord(CurrentPosition);
            Pos = a.UnCalculateRatio(Pos);
        }
        return Pos;

    }


    public static void SetRatio(this ScriptableGrid i)
    {

        GridSettings GridSettings = i.GetGridSettings().settings;
        GridObject GridObject = i.GetGridSettings().gridObject;

        float Ratio;

        Ratio = GridObject.Columns / (float)GridObject.Rows;

        GridSettings.ScreenRatio.y = Camera.main.orthographicSize;
        GridSettings.ScreenRatio.x = GridSettings.ScreenRatio.y * Ratio; //* (Screen.width / Screen.height);
    }



    public static Vector2 CalculateRatio(this ScriptableGrid a, Vector2 Value)// x must be expressed as (x) / (float)(Columns - 1.0f))
    {
        GridSettings i = a.GetGridSettings().settings;
        GridObject j = a.GetGridSettings().gridObject;

        Vector2 MaxValue = new Vector2(j.Columns, j.Rows);

        if (i.PolarActive)
        {
            Value.x += 1;
            Value.y += 1;
        }
        else
        {
            MaxValue.x -= 1;
            MaxValue.y -= 1;
        }

        Vector2 Ratio;
        Ratio = Value / MaxValue;  // x (between +/- 0.5*vertical)
        return Ratio;
    }

    public static Vector2 UnCalculateRatio(this ScriptableGrid i, Vector2 Ratio)// x must be expressed as (x) / (float)(Columns - 1.0f))
    {
        GridObject GridSettings = i.GetGridSettings().gridObject;
        Vector2 MaxValue = new Vector2(GridSettings.Columns, GridSettings.Rows);

        MaxValue.x -= 1;
        MaxValue.y -= 1;

        return (Ratio * MaxValue);
    }
    public static Vector2 UnCalcluatePolarRatio(this ScriptableGrid i, Vector2 Ratio)// x must be expressed as (x) / (float)(Columns - 1.0f))
    {
        GridObject GridSettings = i.GetGridSettings().gridObject;
        Vector2 MaxValue = new Vector2(GridSettings.Columns, GridSettings.Rows);

        Ratio = Ratio * MaxValue;
        Ratio.x += 1;
        Ratio.y += 1;
        return (Ratio);
    }
    public static Vector3 CoordtoGrid(this ScriptableGrid a, Vector2 Position)// x must be expressed as (x) / (float)(Columns - 1.0f))
    {
        GridSettings i = a.GetGridSettings().settings;
        float column = Mathf.LerpUnclamped(-i.Size * i.ScreenRatio.x, i.Size * i.ScreenRatio.x, Position.x);  // x (between +/- 0.5*vertical)
        float row = Mathf.LerpUnclamped(-i.Size * i.ScreenRatio.y, i.Size * i.ScreenRatio.y, Position.y);   // y (between +/- 0.5*horizontal)
        return new Vector3(column, row, 0);
    }
    public static Vector3 CoordtoPolar(this ScriptableGrid a, Vector2 Position)
    {
        GridSettings i = a.GetGridSettings().settings;
        float angle = Mathf.LerpUnclamped(0, 2 * Mathf.PI, Position.x); // = x (between 0 and 2*pi)
        float radius = Mathf.LerpUnclamped(0, i.Size * i.ScreenRatio.y, Position.y);  // = y (between 0 and 0.5*vertical)
        return new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
    }
    public static Vector2 PolartoCoord(this ScriptableGrid a, Vector3 Position)// This doesn't work
    {
        GridSettings i = a.GetGridSettings().settings;

        Vector2 CoordPosition;// =         CoordtoPolar (GridtoCoord(Position));        
        float radius = Vector3.Magnitude(Position);
        CoordPosition.y = Mathf.InverseLerp(0, i.Size * i.ScreenRatio.y, radius);

        CoordPosition.x = Position.x / radius;
        CoordPosition.x = Mathf.Acos(CoordPosition.x);

        CoordPosition.x = Mathf.InverseLerp(0, 2 * Mathf.PI, CoordPosition.x);  // = x (between 0 and 2*pi)

        return CoordPosition; // new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
    }
    public static Vector2 GridtoCoord(this ScriptableGrid a, Vector3 Position)// x must be expressed as (x) / (float)(Columns - 1.0f))
    {
        GridSettings i = a.GetGridSettings().settings;

        Vector2 CoordPosition;

        CoordPosition.x = Mathf.InverseLerp(-i.Size * i.ScreenRatio.x, i.Size * i.ScreenRatio.x, Position.x);  // x (between +/- 0.5*vertical)
        CoordPosition.y = Mathf.InverseLerp(-i.Size * i.ScreenRatio.y, i.Size * i.ScreenRatio.y, Position.y);   // y (between +/- 0.5*horizontal)

        return CoordPosition;
    }


    #endregion

    #region Elias Extensions
    public static void ChangeElias(this ScriptableElias a, EliasPlayer eliasPlayer, string myTheme)
    {
        EliasObject i = a.GetEliasName(myTheme);

        if (i.useSetLevel)
        {
            eliasPlayer.QueueEvent(i.setLevel.CreateSetLevelEvent(eliasPlayer.Elias));
        }
        if (i.useSetLevelOnTrack)
        {
            eliasPlayer.QueueEvent(i.setLevelOnTrack.CreateSetLevelOnTrackEvent(eliasPlayer.Elias));
        }
        if (i.usePlayStinger)
        {
            eliasPlayer.QueueEvent(i.playStinger.CreatePlayerStingerEvent(eliasPlayer.Elias));
        }
        if (i.useActionPreset)
        {
            int j = UnityEngine.Random.Range(0, i.actionPresetName.Length);
            eliasPlayer.RunActionPreset(i.actionPresetName[j], i.allowRequiredThemeMissmatch);
        }
        if (i.useDoubleEffectParam)
        {
            eliasPlayer.QueueEvent(i.doubleEffectParam.CreateSetEffectParameterEvent(eliasPlayer.Elias));
        }
        if (i.useSetSendVolume)
        {
            eliasPlayer.QueueEvent(i.setSendVolume.CreateSetSendVolumeEvent(eliasPlayer.Elias));
        }

    }

    public static void CreateMusicPopUp(this ScriptableElias a, string input, EliasPlayer eliasPlayer)
    {
        if (!a.PopupPrefab)
            return;

        GameObject newCanvasObject = new GameObject();
        newCanvasObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;


        GameObject Popup = UnityEngine.Object.Instantiate(a.PopupPrefab, newCanvasObject.transform);
        Popup.GetComponent<CornerPopUp>().CustomStart(eliasPlayer.GetActiveTheme() + ": " + input);
        UnityEngine.Object.Destroy(newCanvasObject, 5);
    }


    #endregion
}
