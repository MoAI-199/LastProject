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
    private GameObject _prefab_input_name;
    private GameObject _prefab_play_scene;
    private GameObject _prefab_result;
    private GameObject _prefab_stage_pvp;
    private GameObject _prefab_stage_challenge;
    
    private GameObject _player1_parent;

    private void Awake( ) {
        if( instatnce == null ) {
            instatnce = this;
            DontDestroyOnLoad( instatnce );
        } else {
            Destroy( this );
        }
    }
    private void Start( ) {
        _user_data = new UserData( );
        _game_state = COMMON_DATA.GAME_STATE_TYPE.NONE;
        loadResources( );

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
                Instantiate( _prefab_input_name );
                break;
            case COMMON_DATA.GAME_STATE_TYPE.INPUT_NAME:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING;
                switch( _game_mode ) {
                    case COMMON_DATA.GAME_MODE.PVP:
                        Instantiate( _prefab_stage_pvp );
                        break;
                    case COMMON_DATA.GAME_MODE.CHELLENGE:
                        Instantiate( _prefab_stage_challenge );
                        break;
                }
                Instantiate( _prefab_play_scene );

                break;
            case COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.RESULT;
                Instantiate( _prefab_result );
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
    
    public void setGameMode( COMMON_DATA.GAME_MODE game_mode ) {
        _game_mode = game_mode;
    }

    public UserData getUserData( ) {
        return _user_data;
    }

    public GameObject getPlayer1( ){
        return _player1_parent;
    }
    public void setPlayer1( GameObject player1 ) {
        _player1_parent = player1;
    }
    private void loadResources( ){
        _prefab_input_name = ( GameObject )Resources.Load( COMMON_DATA.Prefab.CANVAS_INPUT_NAME );
        _prefab_play_scene = ( GameObject )Resources.Load( COMMON_DATA.Prefab.PLAY_SCENE );
        _prefab_result = ( GameObject )Resources.Load( COMMON_DATA.Prefab.CANVAS_RESULT );
        _prefab_stage_pvp = ( GameObject )Resources.Load( COMMON_DATA.Prefab.STAGE_PVP );
        _prefab_stage_challenge = ( GameObject )Resources.Load( COMMON_DATA.Prefab.STAGE_CHALLENGE );
    }


}
