using UnityEngine.UI;
using UnityEngine;

public class PowerSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public void SetMaxPower(float maxPower)
    {
        slider.maxValue = maxPower;
        slider.value = 0f;
        fill.color = gradient.Evaluate(0);// вопросик? 
    }

    public void Setpower(float power)
    {
        slider.value = power;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
