using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueRecorder : MonoBehaviour
{
    [TextArea(15, 20)]
    [SerializeField] string TextContent;
    [SerializeField] Text TextComponent;

    [SerializeField] uint MinLine, LineStart, LineAmount, MaxLine;


    // To DO: Work out why lines occassionally are allowed above LineAmount. Work out how to calculate LineAmount at runtime

    int test = 0;
    [ContextMenu("Test Adding stuff")]
    public void Addstuff()
    {
        StartCoroutine(DoSomething());
    }
    IEnumerator DoSomething()
    {
        WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
        for (int i = 0; i <5; i++)
        {

        AddLineOfText("debugline" + test);
        test++;

            yield return endOfFrame;
        }

    }

    public void AddLineOfText(string newText)
    {
        TextContent +=  newText + '\n';
        MaxLine++;
        BumpLine(+1);
       
    }

    public void ClearText()
    {
        TextContent = "";
        MaxLine = 0;
        LineStart = 0;
        ChangeLine(0);
    }

    public void BumpLine(int lineDisplacement)
    {

        int iLineStart = (int)LineStart;
        int iMaxLine = (int)MaxLine;

        int i = (iLineStart + lineDisplacement);


        if (i > iMaxLine ) i = iMaxLine ;
        if (i < 0)            i = 0;
      

        
        ChangeLine((uint)i);
    }

    void ChangeLine(uint newline)
    {
        
        if (newline < MinLine) newline = MinLine;
        if (newline > MaxLine ) newline = MaxLine ;

      //  if (MinLine + newline < LineAmount) LineStart = newline;
        
        if (newline + LineAmount < MaxLine) LineStart = newline;
        UpdateTextBox();
    }    

    void UpdateTextBox()
    {
        if (TextContent == "")
        {
            TextComponent.text = "";
            return;
        }

        TextComponent.text = TextContent;
        string FinalText = TextContent;
        Canvas.ForceUpdateCanvases();

        for (int i = 0; i < TextComponent.cachedTextGenerator.lines.Count; i++)
        {
            int startIndex = TextComponent.cachedTextGenerator.lines[i].startCharIdx;
            int endIndex = (i == TextComponent.cachedTextGenerator.lines.Count - 1) ? TextComponent.text.Length
                : TextComponent.cachedTextGenerator.lines[i + 1].startCharIdx;
            int length = endIndex - startIndex;

            if (i < LineStart)
            {

                FinalText =  FinalText.Remove(0, length);
            }

            if (i >= LineStart + LineAmount)
            {
                FinalText = FinalText.Remove(FinalText.Length - length, length);
            }

        }

        TextComponent.text = FinalText;
    }


    [ContextMenu("Test counting stuff")]
    void CountLines()
    {
        Canvas.ForceUpdateCanvases();
        for (int i = 0; i < TextComponent.cachedTextGenerator.lines.Count; i++)
        {
            int startIndex = TextComponent.cachedTextGenerator.lines[i].startCharIdx;
            int endIndex = (i == TextComponent.cachedTextGenerator.lines.Count - 1) ? TextComponent.text.Length
                : TextComponent.cachedTextGenerator.lines[i + 1].startCharIdx;
            int length = endIndex - startIndex;
            Debug.Log(TextComponent.text.Substring(startIndex, length));
        }
    }    
    
    
    

    public void OpenOrClose(int Mode)
    {
        if (Mode == 0) // open from bottom
        {
            StartCoroutine(ResizeBox(new Vector2(680, 0), new Vector2(680, 160), 1));
        }
        else if (Mode == 1) // close from bottom
        {
            StartCoroutine( ResizeBox(new Vector2(680, 160), new Vector2(680, 0), 1));
        }
        else if (Mode == 2) // open from side
        {
            StartCoroutine( ResizeBox(new Vector2(0, 160), new Vector2(680, 160), 1));
        }
        else if (Mode == 3) // close from side
        {
            StartCoroutine( ResizeBox(new Vector2(680, 160), new Vector2(0, 160), 1));
        }
    }




    IEnumerator ResizeBox(Vector2 StartSize, Vector2 EndSize, float Speed)
    {

        float lerpValue = 0;
        WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
        RectTransform rt = GetComponent<RectTransform>();

        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * Speed;

            Vector2 lerpedVector = Vector2.Lerp(StartSize, EndSize, lerpValue);
            rt.sizeDelta = lerpedVector;

            yield return endOfFrame;
        }

        rt.sizeDelta = EndSize;

    }



}
