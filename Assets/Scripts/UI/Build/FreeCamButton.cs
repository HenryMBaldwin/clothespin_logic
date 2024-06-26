using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FreeCamButton : GMSubscribe
{
    private Button button;
    private bool isToggled;
    private GameManager.GameState prev;

    void Awake()
    {
        Subscribe();
    }

    void OnDestroy()
    {
        UnSubscribe();
    }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
        isToggled = false;
    }

    void Click()
    {
        isToggled = !isToggled;
        UpdateButtonColor();
        // Perform additional actions when the button is toggled
        if (isToggled)
        {
            prev = GameManager.GameState.FreeCam;
        }
        else
        {
            prev = GameManager.GameState.Build;
        }
    }

    public bool GetToggled()
    {
        return isToggled;
    }
    public void Shortcut()
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        prev = GameManager.Instance.State;
        GameManager.Instance.UpdateGameState(GameManager.GameState.Build);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.UpdateGameState(prev);
    }

}
