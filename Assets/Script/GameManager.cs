using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instatnce;

    private COMMON_DATA.GAME_MODE _game_mode = COMMON_DATA.GAME_MODE.NONE;

    private void Awake( ) {
        if( instatnce == null ) {
            instatnce = this;
            DontDestroyOnLoad( instatnce );
        } else {
            Destroy( this );
        }
    }
    private void Start( ) {
    }
    private void Update( ) {
    }

    public COMMON_DATA.GAME_MODE getGameMode( ){
        return _game_mode;
    }

}
