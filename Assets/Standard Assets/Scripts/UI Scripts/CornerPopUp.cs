using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CornerPopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] Vector3 TargetPos;
    [SerializeField] float TransitionTime;
    RectTransform MyTransform;
    float Speed;
    public void CustomStart(string text )
    {
        Speed = 1 / TransitionTime;

        TryGetComponent(out MyTransform);

        myText.text = text;


        StartCoroutine(LerpMove(MyTransform.anchoredPosition, TargetPos, 0));

    }

    IEnumerator LerpMove(Vector3 Start, Vector3 End, float LerpValue)
    {
        LerpValue += Speed * Time.deltaTime;
        if (LerpValue > 1)
            LerpValue = 1;

        Vector3 LerpedValue = Vector3.Lerp(Start, End,  Mathf.Clamp( Mathf.Sin(LerpValue * Mathf.PI) * 2, 0, 1 ));
        MyTransform.anchoredPosition = LerpedValue;


        yield return null;

        if (LerpValue < 1)
            StartCoroutine(LerpMove(Start, End, LerpValue));
        else Destroy(gameObject);
    }
    

}
