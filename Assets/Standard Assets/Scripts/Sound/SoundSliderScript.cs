using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundSliderScript : MonoBehaviour
{
    // Start is called before the first frame update

public GameObject SliderPrefab;
    public AudioMixer[] MyMixers;
    public  float Max, Min, DefaultVal;
    void Start()
    {
        for ( int i = 0; i < MyMixers.Length; i++)
        {
            GameObject newSlider = Instantiate(SliderPrefab, gameObject.transform);
            newSlider.TryGetComponent(out Slider slider);
            slider.maxValue = Max;
            slider.minValue = Min;
            slider.value = Mathf.Lerp(Min, Max, DefaultVal);
            slider.GetComponentInChildren<TextMeshProUGUI>().text = MyMixers[i].name;

            int j = i;
            ChangeMixVolume(slider.value, j);


            slider.onValueChanged.AddListener(value => ChangeMixVolume(slider.value, j));


        }
    }

    void ChangeMixVolume(float newValue, int audioMixerIndex)
    {

        if (MyMixers[0].SetFloat("MyExposedParam" + audioMixerIndex, newValue))
        {

        }
        else
        {
            Debug.Log("MyExposedParam" + audioMixerIndex + " didn't work.");
        }
    }

}
