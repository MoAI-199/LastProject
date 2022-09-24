using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Factory : MonoBehaviour {

    const int INIT_CREATE_HENCHMAN_NUM = 5;

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
        createFamiry( PARENT_TYPE.PLAYER1, new Vector2( 2.5f, 0.0f ) );
        createFamiry( PARENT_TYPE.PLAYER2, new Vector2( -2.5f, 0.0f ) );
    }

    private void loadResorces( ) {
#if UNITY_EDITOR
        _prefab_parent = Resources.Load( "Prefab/Parent" ) as GameObject;
        _prefab_henchman = Resources.Load( "Prefab/Henchman" ) as GameObject;
#endif
    }

    private void createFamiry( PARENT_TYPE type, Vector2 pos ) {
        GameObject parent_obj = createParent( type, pos );
        for( int i = 0; i < INIT_CREATE_HENCHMAN_NUM; i++ ) {
            GameObject henchman_obj = createHenchman( parent_obj,pos );
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
        parent.tag = FAMILY_DATA.TAG_NAME.PARENT.ToString( );
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

    private GameObject createHenchman( GameObject parent, Vector2 pos ) {
        //オブジェクトの生成
        GameObject henchman = Instantiate( _prefab_henchman );
        //座標変更
        henchman.transform.position = pos;
        //リスト追加
        _family_manager.addhenchmanList( henchman, parent );
        //タグ変更
        henchman.tag = FAMILY_DATA.TAG_NAME.HENCHMAN.ToString( );

        return henchman;
    }
}
