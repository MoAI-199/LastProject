using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public static GameManager instatnce;

    private COMMON_DATA.GAME_MODE _game_mode;

    private void Awake( ) {
        if( instatnce == null ){
            instatnce = this;
            DontDestroyOnLoad( instatnce );
        }else{
            Destroy( this );
        }
        
    }
    private void Start() {
    }
    private void Update() {
    }

    



}
