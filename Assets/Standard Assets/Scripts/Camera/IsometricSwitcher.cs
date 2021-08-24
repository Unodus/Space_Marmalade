using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricSwitcher : MonoBehaviour
{

    [SerializeField] Vector3 NormalPosition, IsometricPosition;
    [SerializeField] Vector3 NormalRotation, IsometricRotation;

    bool isometricMode;
    bool isRunning;

    // Update is called once per frame
    void LateUpdate()
    {

        if (isRunning) return;
        if (Input.GetKeyDown(KeyCode.P))
        {

            Vector3 StartRotation, EndRotation, StartPosition, EndPosition;

            isometricMode = !isometricMode;

            if (isometricMode)
            {
                StartPosition = NormalPosition;
                StartRotation = NormalRotation;
                EndPosition = IsometricPosition;
                EndRotation = IsometricRotation;
            }
            else
            {
                EndPosition = NormalPosition;
                EndRotation = NormalRotation;
                StartPosition = IsometricPosition;
                StartRotation = IsometricRotation;
            }

            StartCoroutine(SwitchContext(StartPosition, EndPosition, StartRotation, EndRotation, 1.0f));

        }




    }

    IEnumerator SwitchContext(Vector3 StartPosition, Vector3 EndPosition, Vector3 StartRotation, Vector3 EndRotation, float TimeVal)
    {
        float Clock = 0;
        isRunning = true;
        while (Clock < TimeVal)
        {
            float iStep = Mathf.Lerp(0, Clock, TimeVal);


            transform.localPosition = Vector3.Lerp(StartPosition, EndPosition, iStep);
            transform.localEulerAngles = Vector3.Lerp(StartRotation, EndRotation, iStep);

            Clock += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = EndPosition;
        transform.localEulerAngles = EndRotation;

        isRunning = false;
        yield return null;
    }
}
