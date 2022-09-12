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

    void Start( ) {
        loadResorces( );
        createFamiry( PARENT_TYPE.PLAYER1 );
        createFamiry( PARENT_TYPE.PLAYER2 );
    }

    void Update( ) {

    }

    private void loadResorces( ) {
        _prefab_parent = Resources.Load( "Prefab/Parent" ) as GameObject;
        _prefab_henchman = Resources.Load( "Prefab/Henchman" ) as GameObject;
    }

    private void createFamiry( PARENT_TYPE type ) {
        GameObject parent_obj = createParent( type );
        for( int i = 0; i < INIT_CREATE_HENCHMAN_NUM; i++ ) {
            createHenchman( parent_obj );
        }

    }

    private GameObject createParent( PARENT_TYPE type ) {
        //オブジェクトの生成
        GameObject parent = Instantiate( _prefab_parent );
        //移動処理のアタッチ
        addMoveComponent( parent, type );
        return parent;
    }

    private void addMoveComponent( GameObject parent, PARENT_TYPE type )
    {
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

    private void createHenchman( GameObject parent ) {
        //オブジェクトの生成
        GameObject henchman = Instantiate( _prefab_henchman );
        henchman.GetComponent<Henchman>( ).setingParent( parent );
    }
}
