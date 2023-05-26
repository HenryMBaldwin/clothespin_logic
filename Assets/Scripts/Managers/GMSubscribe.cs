using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMSubscribe : MonoBehaviour
{
    

    // Start is called before the first frame update
    public void Subscribe(){
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    public void UnSubscribe(){
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    public virtual void GameManagerOnGameStateChanged(GameManager.GameState state){}
}

