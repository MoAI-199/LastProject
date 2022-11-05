using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instatnce;

    private COMMON_DATA.GAME_MODE _game_mode = COMMON_DATA.GAME_MODE.CHELLENGE;
    
    private COMMON_DATA.GAME_STATE_TYPE _game_state = COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING;

    private void Awake( ) {
        if( instatnce == null ) {
            instatnce = this;
            DontDestroyOnLoad( instatnce );
        } else {
            Destroy( this );
        }
    }
    private void Start( ) {
        settingDefine( );
    }
    private void Update( ) {
    }

    public COMMON_DATA.GAME_MODE getGameMode( ){
        return _game_mode;
    }
    public COMMON_DATA.GAME_STATE_TYPE getGameState( ) {
        return _game_state;
    }
    public void setGameState( COMMON_DATA.GAME_STATE_TYPE gameState ) {
        _game_state = gameState;
    }
    public void setGameMode( COMMON_DATA.GAME_MODE game_mode ) {
        _game_mode = game_mode;
    }

    public void settingDefine(){
        //ゲームステータスの設定
#if GAME_PLAYING
        _game_state = COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING;
#elif GAME_SETTING
        _game_state = COMMON_DATA.GAME_STATE_TYPE.GAME_SETTING;
#endif

        //ゲームモードの設定
#if CHELLENGE
        _game_mode =  COMMON_DATA.GAME_MODE.CHELLENGE;
#elif PVP
        _game_mode =  COMMON_DATA.GAME_MODE.PVP;
#endif

    }

}
