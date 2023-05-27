using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildMenuButton : GMSubscribe, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager.GameState prev;
    private Button button;
    private bool isToggled;

    //expose prefabs and spawn object to inspector
    public GameObject prefab;
    public GameObject blueprint;

    public GameObject spawnObject;

    void Awake(){
        Subscribe();
    }

    void OnDestroy(){
        UnSubscribe();
    }

    

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
        isToggled = false;
    }

    public bool getToggled()
    {
        return isToggled;
    }

    public void Click()
    {
        isToggled = !isToggled;
        UpdateButtonColor();
        // Perform additional actions when the button is toggled
        if (isToggled)
        {
            prev = GameManager.GameState.Building;
            (spawnObject.GetComponent<SpawnObject>()).SetPrefab(prefab,blueprint);
        }
        else
        {
            prev = GameManager.GameState.Build;
            (spawnObject.GetComponent<SpawnObject>()).SetPrefab(null, null);
        }
    }

    public void Shortcut()
    {
        isToggled = !isToggled;
        UpdateButtonColor();
        if (isToggled)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Building);
            (spawnObject.GetComponent<SpawnObject>()).SetPrefab(prefab, blueprint);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Build);
            (spawnObject.GetComponent<SpawnObject>()).SetPrefab(null, null);
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

    //makes sure when you're about to click on a button it prevents any other scripts from handling the click.
    //e.g. Don't wan't to place an object when toggling freecam.
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Build);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(prev == GameManager.GameState.Building)
        {
            (spawnObject.GetComponent<SpawnObject>()).SetPrefab(prefab, blueprint);
        }
        GameManager.Instance.UpdateGameState(prev);
    }

    
    
}
