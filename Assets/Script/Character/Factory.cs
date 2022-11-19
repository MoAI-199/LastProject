using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class Factory : MonoBehaviour {

    const int INIT_CREATE_HENCHMAN_NUM = 8;

    private enum PARENT_TYPE {
        PLAYER1 = 0,
        PLAYER2 = 1,
        ENEMY_FIRST = 2,
        ENEMY = 3,
        ENEMY_A = 4,
        ENEMY_B = 5,
        ENEMY_C = 6,
        ENEMY_MAX = 7, //エネミーの動きの最大数
    }
    private GameObject _prefab_parent;
    private GameObject _prefab_henchman;
    private FamilyManager _family_manager;
    private int _create_time;
    private float _now_time;
    private void Awake( ) {
        _family_manager = GameObject.Find( COMMON_DATA.SettingName.FAMILY_MANAGER ).GetComponent<FamilyManager>( );
        loadResorces( );
    }

    private void Start( ) {
        gamemodeSetup( );
        //debug野良の生成
        for( int i = 0; i < 10; i++ ) {
            createHenchman( null, new Vector2( 0, 0 ) );
        }
        _create_time = 0;
    }

    private void Update( ) {
        _now_time += Time.deltaTime;
        switch( GameManager.instatnce.getGameMode( ) ) {
            case COMMON_DATA.GAME_MODE.PVP:
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                if( GameManager.instatnce.getGameState( ) == COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING ){
                    updateCreateEnemy( );
                }
                break;
            case COMMON_DATA.GAME_MODE.NONE:
                break;
        }
    }

    private void updateCreateEnemy( ) {
        if( _now_time >= _create_time ) {
            _now_time = 0;
            _create_time = 5;
            Vector2 create_pos = new Vector2(3,3);
            createFamiry( PARENT_TYPE.ENEMY_C, create_pos );
        }
    }

    private void loadResorces( ) {
        _prefab_parent = Resources.Load( COMMON_DATA.Prefab.PARENT ) as GameObject;
        _prefab_henchman = Resources.Load( COMMON_DATA.Prefab.HENCHMAN ) as GameObject;
    }

    void gamemodeSetup( ) {
        switch( GameManager.instatnce.getGameMode( ) ) {
            case COMMON_DATA.GAME_MODE.PVP:
                settingVsMode( );
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                settingChallangeMode( );
                break;

            default:
                break;
        }
    }
    private void createFamiry( PARENT_TYPE type, Vector2 pos ) {
        GameObject parent_obj = createParent( type, pos );
        pos += new Vector2( 1, 1 );
        for( int i = 0; i < INIT_CREATE_HENCHMAN_NUM; i++ ) {
            createHenchman( parent_obj, pos );
        }
        if( type == PARENT_TYPE.PLAYER1 ){
            GameManager.instatnce.setPlayer1( parent_obj );
        }
    }

    private GameObject createParent( PARENT_TYPE type, Vector2 pos ) {
        //オブジェクトの生成
        GameObject parent = Instantiate( _prefab_parent );
        //移動処理のアタッチ
        addMoveComponent( parent, type );
        //生成座標
        parent.transform.position = pos;
        //リストへの追加
        _family_manager.addParentList( parent );
        //タグ変更
        parent.tag = COMMON_DATA.TAG_NAME.PARENT.ToString( );
        //名付け
        addParentDefaultName( parent, type );
        return parent;
    }

    private void addMoveComponent( GameObject parent, PARENT_TYPE type ) {
        switch( type ) {
            case PARENT_TYPE.PLAYER1:
                parent.AddComponent<MovePlayer1>( );
                break;
            case PARENT_TYPE.PLAYER2:
                parent.AddComponent<MovePlayer2>( );
                break;
            case PARENT_TYPE.ENEMY:
                parent.AddComponent<MoveEnemy>( );
                break;
            case PARENT_TYPE.ENEMY_A:
                parent.AddComponent<MoveEnemyA>( );
                break;
            case PARENT_TYPE.ENEMY_B:
                parent.AddComponent<MoveEnemyB>( );
                break;
            case PARENT_TYPE.ENEMY_C:
                parent.AddComponent<MoveEnemyC>( );
                break;
        }
    }

    private void addParentDefaultName( GameObject parent, PARENT_TYPE type ) {
        Parent parent_script = parent.GetComponent<Parent>( );
        switch( type ) {
            case PARENT_TYPE.PLAYER1:
                string player1_name = GameManager.instatnce.getUserData( ).getUserName( ( int )PARENT_TYPE.PLAYER1 );
                parent_script.chaneParemeterName( player1_name );
                break;
            case PARENT_TYPE.PLAYER2:
                string player2_name = GameManager.instatnce.getUserData( ).getUserName( ( int )PARENT_TYPE.PLAYER2 );
                parent_script.chaneParemeterName( player2_name );
                break;
            case PARENT_TYPE.ENEMY:
            case PARENT_TYPE.ENEMY_A:
            case PARENT_TYPE.ENEMY_B:
            case PARENT_TYPE.ENEMY_C:
                parent_script.chaneParemeterName( "Enemy" );
                break;
        }
    }

    private GameObject createHenchman( GameObject parent, Vector2 pos ) {
        //オブジェクトの生成
        GameObject henchman = Instantiate( _prefab_henchman );
        //座標変更
        henchman.transform.position = pos;
        //リスト追加
        _family_manager.addhenchmanList( henchman, parent );
        //タグ変更
        henchman.tag = COMMON_DATA.TAG_NAME.HENCHMAN.ToString( );
        return henchman;
    }

    private void settingVsMode( ) {
        createFamiry( PARENT_TYPE.PLAYER1, new Vector2( -2.5f, 0.0f ) );
        createFamiry( PARENT_TYPE.PLAYER2, new Vector2( 2.5f, 0.0f ) );
    }

    private void settingChallangeMode( ) {
        createFamiry( PARENT_TYPE.PLAYER1, new Vector2( 0.0f, 0.0f ) );
    }
}
