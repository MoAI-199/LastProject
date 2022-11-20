using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class Factory : MonoBehaviour {

    const int INIT_CREATE_HENCHMAN_NUM = 8;

    private enum PARENT_TYPE {
        PLAYER1,
        PLAYER2,
        ENEMY_FIRST,
        //ENEMY,
        ENEMY_A,
        ENEMY_B,
        ENEMY_C,
        ENEMY_MAX, //エネミーの動きの最大数
    }
    private GameObject _prefab_parent_pink;
    private GameObject _prefab_parent_blue;
    private GameObject _prefab_henchman_pink;
    private GameObject _prefab_henchman_blue;
    private FamilyManager _family_manager;
    private int _create_time;
    private float _now_time;
    private float _create_challenge_time;
    private float _create_pvp_time;
    private void Awake( ) {
        _family_manager = GameObject.Find( COMMON_DATA.SettingName.FAMILY_MANAGER ).GetComponent<FamilyManager>( );
        loadResorces( );
    }

    private void Start( ) {
        gamemodeSetup( );

        _create_time = 0;
        _create_challenge_time = 0.0f;
        _now_time = 0.0f;
        _create_pvp_time = 0.0f;
    }

    private void Update( ) {
        _now_time += Time.deltaTime;
        _create_challenge_time += Time.deltaTime;
        _create_pvp_time += Time.deltaTime;
        switch( GameManager.instatnce.getGameMode( ) ) {
            case COMMON_DATA.GAME_MODE.PVP:
                updateCreateWildHenchman( );
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                if( GameManager.instatnce.getGameState( ) == COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING ) {
                    updateCreateEnemy( );
                }
                break;
            case COMMON_DATA.GAME_MODE.NONE:
                break;
        }
    }

    private void updateCreateWildHenchman( ) {
        float random_x;
        float random_y;
        Vector2 create_pos;
        random_x = UnityEngine.Random.Range( -8, 8 );
        random_y = UnityEngine.Random.Range( -4, 4 );
        create_pos = new Vector2( random_x, random_y );
        //野良の生成
        if( _create_pvp_time > 5.0f ) { //5秒に１回生成
            _create_pvp_time = 0;
            int add_num = (int)(_now_time / 30.0f) + 1;
            for( int i = 0; i < add_num; i++ ) { //最低１体。３０秒ごとに１体増える
                createHenchman( PARENT_TYPE.ENEMY_A, null, create_pos );

            }
        }
    }

    private void updateCreateEnemy( ) {
        if( _family_manager.getParentCount( ) > COMMON_DATA.COMMON_VALUE.MAX_CREATE ) {
            //生成数に制限をかける
            return;
        }
        if( _create_challenge_time >= _create_time ) {
            _create_challenge_time = 0;

            _create_time = UnityEngine.Random.Range( 1, 5 );
            Vector2 player1_pos = GameManager.instatnce.getPlayer1( ).transform.position;

            float random_x;
            float random_y;
            Vector2 create_pos;
            int repeat_count = 0;
            while( true ) {
                repeat_count++;
                if( repeat_count > 100 ) {
                    //無限ループ対策
                    random_x = 0.0f;
                    random_y = 0.0f;
                    create_pos = new Vector2( random_x, random_y );
                    break;
                }
                random_x = UnityEngine.Random.Range( COMMON_DATA.COMMON_VALUE.FIELD_LEFT_MAX, COMMON_DATA.COMMON_VALUE.FIELD_RIGHT_MAX );
                random_y = UnityEngine.Random.Range( COMMON_DATA.COMMON_VALUE.FIELD_BUTTOM_MAX, COMMON_DATA.COMMON_VALUE.FIELD_TOP_MAX );
                create_pos = new Vector2( random_x, random_y );
                var dis = Vector2.Distance( create_pos, player1_pos );
                //Player１に近すぎる箇所には生成しない。
                if( dis > COMMON_DATA.COMMON_VALUE.CREATE_LIMITTE ) {
                    break;
                }
            }

            int create_type = UnityEngine.Random.Range( ( int )PARENT_TYPE.ENEMY_FIRST + 1, ( int )PARENT_TYPE.ENEMY_MAX );
            //createFamiry( ( PARENT_TYPE )create_type, create_pos );
            createFamiry( PARENT_TYPE.ENEMY_A, create_pos );
        }
    }

    private void loadResorces( ) {
        _prefab_parent_pink = Resources.Load( COMMON_DATA.Prefab.PARENT_PINK ) as GameObject;
        _prefab_henchman_pink = Resources.Load( COMMON_DATA.Prefab.HENCHMAN_PINK ) as GameObject;
        _prefab_parent_blue = Resources.Load( COMMON_DATA.Prefab.PARENT_BLUE ) as GameObject;
        _prefab_henchman_blue = Resources.Load( COMMON_DATA.Prefab.HENCHMAN_BLUE ) as GameObject;
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

        switch( type ) {
            case PARENT_TYPE.PLAYER1:
                for( int i = 0; i < INIT_CREATE_HENCHMAN_NUM; i++ ) {
                    createHenchman( type, parent_obj, pos );
                }
                GameManager.instatnce.setPlayer1( parent_obj );
                break;
            case PARENT_TYPE.PLAYER2:
                for( int i = 0; i < INIT_CREATE_HENCHMAN_NUM; i++ ) {
                    createHenchman( type, parent_obj, pos );
                }
                break;
           // case PARENT_TYPE.ENEMY:
            case PARENT_TYPE.ENEMY_A:
            case PARENT_TYPE.ENEMY_B:
            case PARENT_TYPE.ENEMY_C:
                int add_time_num = ( int )( _now_time / 10.0f ); //最低８体、時間経過と共に増えていく(10秒に１体ずつ)(最大17体)
                int random_max = ( int )( _now_time / 5.0f ); //時間経過と共にランダムの最大値が上昇(5秒に１体ずつ)（最大34体）
                int add_random_num = UnityEngine.Random.Range( 1, 5 + random_max );

                for( int i = 0; i < INIT_CREATE_HENCHMAN_NUM + add_time_num + add_random_num; i++ ) {
                    createHenchman( type, parent_obj, pos );
                }
                break;
        }
    }

    private GameObject createParent( PARENT_TYPE type, Vector2 pos ) {
        //オブジェクトの生成
        GameObject create_go = _prefab_parent_pink;
        switch( type ) {
            case PARENT_TYPE.PLAYER1:
                create_go = _prefab_parent_pink;
                break;
            case PARENT_TYPE.PLAYER2:
            //case PARENT_TYPE.ENEMY:
            case PARENT_TYPE.ENEMY_A:
            case PARENT_TYPE.ENEMY_B:
            case PARENT_TYPE.ENEMY_C:
                create_go = _prefab_parent_blue;
                break;
        }
        GameObject parent = Instantiate( create_go );
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
            //case PARENT_TYPE.ENEMY:
            //    parent.AddComponent<MoveEnemy>( );
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
           // case PARENT_TYPE.ENEMY:
            case PARENT_TYPE.ENEMY_A:
            case PARENT_TYPE.ENEMY_B:
            case PARENT_TYPE.ENEMY_C:
                parent_script.chaneParemeterName( "Enemy" );
                break;
        }
    }

    private GameObject createHenchman( PARENT_TYPE type, GameObject parent, Vector2 pos ) {
        //オブジェクトの生成
        GameObject create_go = _prefab_henchman_pink;
        switch( type ) {
            case PARENT_TYPE.PLAYER1:
                create_go = _prefab_henchman_pink;
                break;
            case PARENT_TYPE.PLAYER2:
            //case PARENT_TYPE.ENEMY:
            case PARENT_TYPE.ENEMY_A:
            case PARENT_TYPE.ENEMY_B:
            case PARENT_TYPE.ENEMY_C:
                create_go = _prefab_henchman_blue;
                break;
        }
        GameObject henchman = Instantiate( create_go );
        //座標変更
        pos += new Vector2( UnityEngine.Random.Range( 0,2), UnityEngine.Random.Range( 0, 2 ) );
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
