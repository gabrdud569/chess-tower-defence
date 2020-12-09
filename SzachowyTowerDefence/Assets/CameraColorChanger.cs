using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorChanger : MonoBehaviour
{
    public void OnSliderChanged(PinchSlider slider)
    {
        this.GetComponent<Camera>().backgroundColor = new Color(slider.SliderValue, slider.SliderValue, slider.SliderValue);
    }
}
