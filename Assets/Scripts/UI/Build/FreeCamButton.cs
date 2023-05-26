using UnityEngine;
using UnityEngine.UI;

public class FreeCamButton : BuildMenuButton
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
            GameManager.Instance.UpdateGameState(GameManager.GameState.FreeCam);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Build);
        }
    }

    //Override because this button shouldn't dissapear in FreeCam
    public override void GameManagerOnGameStateChanged(GameManager.GameState state){}

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
