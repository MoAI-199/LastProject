using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Factory : MonoBehaviour {

    const int INIT_CREATE_HENCHMAN_NUM = 8;

    private enum PARENT_TYPE {
        PLAYER1,
        PLAYER2,
        ENEMY,
    }
    private GameObject _prefab_parent;
    private GameObject _prefab_henchman;
    private FamilyManager _family_manager;

    void Awake( ) {
        _family_manager = GameObject.Find( "Manager" ).GetComponent<FamilyManager>( );
    }

    void Start( ) {
        loadResorces( );
        createFamiry( PARENT_TYPE.PLAYER1 );
        createFamiry( PARENT_TYPE.PLAYER2 );
    }

    private void loadResorces( ) {
 #if UNITY_EDITOR
        _prefab_parent = Resources.Load( "Prefab/Parent" ) as GameObject;
        _prefab_henchman = Resources.Load( "Prefab/Henchman" ) as GameObject;
 #endif
    }

    private void createFamiry( PARENT_TYPE type ) {
        GameObject parent_obj = createParent( type );
       
        for( int i = 0; i < INIT_CREATE_HENCHMAN_NUM; i++ ) {
            GameObject henchman_obj = createHenchman( parent_obj );
            _family_manager.addhenchmanList( henchman_obj, parent_obj );
        }
    }

    private GameObject createParent( PARENT_TYPE type ) {
        //オブジェクトの生成
        GameObject parent = Instantiate( _prefab_parent );
        //移動処理のアタッチ
        addMoveComponent( parent, type );
        //生成座標
        parent.transform.position = new Vector2( Random.Range(-10.0f,1.0f), Random.Range( 10.0f, 5.0f ) );
        //リストへの追加
        _family_manager.addParentList( parent );
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
                break;
        }
    }

    private GameObject createHenchman( GameObject parent ) {
        //オブジェクトの生成
        GameObject henchman = Instantiate( _prefab_henchman );
        //座標変更
        henchman.transform.position = parent.transform.position;

        return henchman;
    }
}
