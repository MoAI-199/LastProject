using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public class ResultSaveData {
        public int _kill_num {
            get; set;
        }
        public int _added_count {
            get; set;
        }
    }
    public static GameManager instatnce;

    public ResultSaveData _result_save_data;
    private COMMON_DATA.GAME_MODE _game_mode = COMMON_DATA.GAME_MODE.CHELLENGE;
    private COMMON_DATA.GAME_STATE_TYPE _game_state = COMMON_DATA.GAME_STATE_TYPE.NONE;
    private UserData _user_data;
    private GameObject _prefab_input_name;
    private GameObject _prefab_stage_pvp;
    private GameObject _prefab_stage_challenge;
    private GameObject _prefab_timer_manager;
    private GameObject _prefab_gamemanaers;
    private GameObject _prefab_result_base;
    private GameObject _prefab_result_challenge;
    private GameObject _prefab_result_pvp;

    public List<Texture> _texture_parent_list;
    public List<Texture> _texture_henchman_list;

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
        _result_save_data = new ResultSaveData( );
        loadResources( );

    }
    private void Update( ) {
        if( _player1_parent != null ) {
            var player1 = _player1_parent.gameObject.GetComponent<Parent>( );
            _result_save_data._kill_num = player1.getKillCount( );
            _result_save_data._added_count = player1.getAddedCount( );
        }
    }

    public void doneGameStatus( ) {
        switch( _game_state ) {
            case COMMON_DATA.GAME_STATE_TYPE.NONE:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.GUIDE;
                doneGameStatusToNone( );
                break;
            case COMMON_DATA.GAME_STATE_TYPE.GUIDE:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.INPUT_NAME;
                doneGameStatusToGuide( );
                break;
            case COMMON_DATA.GAME_STATE_TYPE.INPUT_NAME:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING;
                doneGameStatusToInputname( );
                break;
            case COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.RESULT;
                doneGameStatusToPlaying( );
                break;
            case COMMON_DATA.GAME_STATE_TYPE.RESULT:
                _game_state = COMMON_DATA.GAME_STATE_TYPE.GUIDE;
                doneGameStatusToResult( );
                break;
        }
        Debug.Log( $"now_Status:{_game_state}" );
    }

    private void doneGameStatusToNone( ) {
    }
    private void doneGameStatusToGuide( ) {
        Instantiate( _prefab_input_name );
    }
    private void doneGameStatusToInputname( ) {
        switch( _game_mode ) {
            case COMMON_DATA.GAME_MODE.PVP:
                Instantiate( _prefab_stage_pvp );
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                Instantiate( _prefab_stage_challenge );
                Instantiate( _prefab_timer_manager );
                break;
        }
        Instantiate( _prefab_gamemanaers );
    }
    private void doneGameStatusToPlaying( ) {
        Instantiate( _prefab_result_base );
        switch( _game_mode ) {
            case COMMON_DATA.GAME_MODE.PVP:
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:

                Instantiate( _prefab_result_challenge );
                break;
        }
    }
    private void doneGameStatusToResult( ) {
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

    public GameObject getPlayer1( ) {
        return _player1_parent;
    }
    public void setPlayer1( GameObject player1 ) {
        _player1_parent = player1;
    }
    private void loadResources( ) {
        _prefab_timer_manager = ( GameObject )Resources.Load( COMMON_DATA.Prefab.CANVAS_TIMER );
        _prefab_input_name = ( GameObject )Resources.Load( COMMON_DATA.Prefab.CANVAS_INPUT_NAME );
        _prefab_gamemanaers = ( GameObject )Resources.Load( COMMON_DATA.Prefab.GAMEMANAGERS );
        _prefab_result_base = ( GameObject )Resources.Load( COMMON_DATA.Prefab.CANVAS_RESULT_BASE );
        _prefab_stage_pvp = ( GameObject )Resources.Load( COMMON_DATA.Prefab.STAGE_PVP );
        _prefab_stage_challenge = ( GameObject )Resources.Load( COMMON_DATA.Prefab.STAGE_CHALLENGE );
        _prefab_result_challenge = ( GameObject )Resources.Load( COMMON_DATA.Prefab.CANVAS_RESULT_CHALLENGE );
        _prefab_result_pvp = ( GameObject )Resources.Load( COMMON_DATA.Prefab.CANVAS_RESULT_PVP );

        _texture_parent_list = new List<Texture>( );
        _texture_henchman_list = new List<Texture>( );
        //親のためのテクスチャ※色は列挙の順番
        _texture_parent_list.Add( Resources.Load<Texture>( COMMON_DATA.CharaSprite.COLOR_PINK_PARENT ) ); //pink
        _texture_parent_list.Add( Resources.Load<Texture>( COMMON_DATA.CharaSprite.COLOR_BLUE_PARENT ) ); //blue
        //子分のためのテクスチャ※色は列挙の順番
        _texture_henchman_list.Add( Resources.Load<Texture>( COMMON_DATA.CharaSprite.COLOR_PINK_HENCHMAN ) ); //pink
        _texture_henchman_list.Add( Resources.Load<Texture>( COMMON_DATA.CharaSprite.COLOR_BLUE_HENCHMAN ) ); //blue
    }


}
