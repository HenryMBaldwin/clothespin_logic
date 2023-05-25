using UnityEngine;
using UnityEngine.UI;

public class FreeCamButton : MonoBehaviour
{
    private Button button;
    private bool isToggled;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleButton);
        isToggled = false;
    }

    void ToggleButton()
    {
        isToggled = !isToggled;
        UpdateButtonColor();
        // Perform additional actions when the button is toggled
        if (isToggled)
        {
            // Button is toggled on
            // Perform actions related to enabling free camera mode
        }
        else
        {
            // Button is toggled off
            // Perform actions related to disabling free camera mode
        }
    }

    void UpdateButtonColor()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = isToggled ? new Color(1f, 0.8f, 0.8f) : Color.white;
        colors.highlightedColor = colors.normalColor; // Set the highlighted color to white
        colors.pressedColor = colors.normalColor; // Set the pressed color to white
        colors.selectedColor = colors.normalColor; // Set the selected color to white
        colors.disabledColor = colors.normalColor; // Set the disabled color to white
        button.colors = colors;
    }

}
