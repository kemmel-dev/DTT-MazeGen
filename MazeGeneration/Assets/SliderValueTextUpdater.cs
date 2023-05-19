using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueTextUpdater : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    
    private TextMeshProUGUI _tmp;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _tmp.text = $"{_slider.value:F2} s";
    }
}
