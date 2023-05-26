using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public static GameManager Instance{get{return _Instance;}}
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
        UpdateGameState(GameState.Build);
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
