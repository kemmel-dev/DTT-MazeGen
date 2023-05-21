using UnityEngine;
using UnityEngine.UI;

public class GoButton : MonoBehaviour
{

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;
    }

    public void RefreshEnabled(bool allPlaced)
    {
        _button.interactable = allPlaced;
    }
}
