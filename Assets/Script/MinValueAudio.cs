using UnityEngine;
using UnityEngine.UI;

public class MinValue : MonoBehaviour
{
    public Slider slider;
    public static float values;

    void Update()
    {
        values = slider.value;
        if (slider.value == -30)
        {
            slider.minValue = -80;
            slider.value = slider.minValue;
        }
        else if (slider.minValue == -80 && slider.value != -80)
        {
            slider.minValue = -30;
            slider.value = slider.minValue + 1;
        }

    }
}