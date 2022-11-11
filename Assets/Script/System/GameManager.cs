using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instatnce;

    private COMMON_DATA.GAME_MODE _game_mode = COMMON_DATA.GAME_MODE.CHELLENGE;
    private COMMON_DATA.GAME_STATE_TYPE _game_state = COMMON_DATA.GAME_STATE_TYPE.NONE;

    private UserData _user_data;

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
        _user_data = new UserData( );
        _game_state = COMMON_DATA.GAME_STATE_TYPE.NONE;

    }
    private void Update( ) {

    }

    public void doneGameStatus( ) {
        switch( _game_state ) {
            case COMMON_DATA.GAME_STATE_TYPE.NONE:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.GUIDE;
                break;
            case COMMON_DATA.GAME_STATE_TYPE.GUIDE:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.INPUT_NAME;
                GameObject input_name_prefab = ( GameObject )Resources.Load( COMMON_DATA.SettingName.PREFAB_PATH_CANVAS_INPUT_NAME );
                Instantiate( input_name_prefab );
                break;
            case COMMON_DATA.GAME_STATE_TYPE.INPUT_NAME:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING;
                break;
            case COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.RESULT;
                GameObject result_prefab = ( GameObject ) Resources.Load( COMMON_DATA.SettingName.PREFAB_PATH_CANVAS_RESULT );
                Instantiate( result_prefab );
                break;
            case COMMON_DATA.GAME_STATE_TYPE.RESULT:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.GUIDE;
                break;
        }
        Debug.Log( $"now_Status:{_game_state}" );

    }

    public COMMON_DATA.GAME_MODE getGameMode( ) {
        return _game_mode;
    }
    public COMMON_DATA.GAME_STATE_TYPE getGameState( ) {
        return _game_state;
    }
    //public void setGameState( COMMON_DATA.GAME_STATE_TYPE gameState ) {
    //    _game_state = gameState;
    //}
    public void setGameMode( COMMON_DATA.GAME_MODE game_mode ) {
        _game_mode = game_mode;
    }

    public UserData getUserData( ) {
        return _user_data;
    }

    public void settingDefine( ) {
        //ゲームステータスの設定
#if GAME_PLAYING
        _game_state = COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING;
#elif GAME_SETTING
        _game_state = COMMON_DATA.GAME_STATE_TYPE.GUIDE;
#endif

        //ゲームモードの設定
#if CHELLENGE
        _game_mode = COMMON_DATA.GAME_MODE.CHELLENGE;
#elif PVP
        _game_mode =  COMMON_DATA.GAME_MODE.PVP;
#endif

    }
}
