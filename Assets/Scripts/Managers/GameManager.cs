using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public static GameManager Instance{get{return _Instance;}}

    //Components to link in editor
    public GameObject[] buildButtons;
    public Button freeCamButton;
    public GameObject spawnObject;
    void Awake(){
        _Instance = this;
    }

    public enum GameState{
        MainMenu,
        FreeCam,
        Build,
        Building,
        Run
    }
    
    void Start() {
        //initial state set to freecam
        UpdateGameState(GameState.FreeCam);
    }

    void Update(){
        //listen for key shortcuts
        if (Input.GetKeyDown(KeyCode.F)){
            switch (State)
            {
                case GameState.FreeCam:
                case GameState.Build:
                    freeCamButton.onClick.Invoke();
                    break;
                case GameState.Building:
                    //transition back to build before going into freecam, building cannot go directly to freecam
                    UpdateGameState(GameState.Build);
                    freeCamButton.onClick.Invoke();
                    break;
            }
        }
    }
    public void UpdateGameState(GameState newState){
        State = newState;

        switch(newState){
            case GameState.MainMenu:
                break;
            case GameState.FreeCam:
                break;
            case GameState.Build:
                HandleBuild();
                break;
            case GameState.Building:
                break;
            case GameState.Run:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleBuild()
    {
        //toggle off all build buttons
        foreach(GameObject btn in buildButtons)
        {
            if ((btn.GetComponent<BuildMenuButton>()).getToggled())
            {
                btn.GetComponent<BuildMenuButton>().Shortcut();
            }
        }
        (spawnObject.GetComponent<SpawnObject>()).SetPrefab(null, null);
    }
}
