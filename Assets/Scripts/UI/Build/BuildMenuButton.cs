using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuildMenuButton : GMSubscribe, IPointerEnterHandler, IPointerExitHandler
{
    //saves previous gamestate to return to after hover events
    public bool prev;
    
    public GameObject SpawnObject;

    void Awake(){
        Subscribe();
        SpawnObject = GameObject.Find("Spawn Object");
        prev = SpawnObject.activeSelf;
    }

    void OnDestroy(){
        UnSubscribe();
    }

    //makes sure when you're about to click on a button it prevents any other scripts from handling the click.
    //e.g. Don't wan't to place an object when toggling freecam.
    public void OnPointerEnter(PointerEventData eventData)
    {   
        prev = SpawnObject.activeSelf;
        SpawnObject.SetActive(false);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SpawnObject.SetActive(prev);
    }

    //Supplies default menu button behavior
    public override void GameManagerOnGameStateChanged(GameManager.GameState state){
        this.gameObject.SetActive(state == GameManager.GameState.Build);
    }
    
}
