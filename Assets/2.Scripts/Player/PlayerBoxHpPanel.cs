using UnityEngine;
using UnityEngine.UI;

public class PlayerBoxHpPanel : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    public void SetPanel(float health)
    {
        hpSlider.maxValue = health;
        hpSlider.value = health;
    }

    public void SetPanelValue(float value)
    {
        hpSlider.value = value;
    }
}
