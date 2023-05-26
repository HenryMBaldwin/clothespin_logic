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

    public Button freeCamButton;

    void Awake(){
        _Instance = this;
    }

    public enum GameState{
        MainMenu,
        FreeCam,
        Build,
        Run
    }
    
    void Start(){
        freeCamButton = GameObject.Find("Free Cam").GetComponent<Button>();
        UpdateGameState(GameState.Build);
    }

    void Update(){
        //listen for key shortcuts
        if(Input.GetKeyDown(KeyCode.F) && (this.State == GameState.Build || this.State == GameState.FreeCam)){
            freeCamButton.onClick.Invoke();
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
                break;
            case GameState.Run:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}
