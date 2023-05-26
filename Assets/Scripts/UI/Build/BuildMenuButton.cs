using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildMenuButton : GMSubscribe
{
    void Awake(){
        Subscribe();
    }

    void OnDestroy(){
        UnSubscribe();
    }

    //Override to supply default menu button behavior
    public override void GameManagerOnGameStateChanged(GameManager.GameState state){
        this.gameObject.SetActive(state == GameManager.GameState.Build);
    }
    
}
