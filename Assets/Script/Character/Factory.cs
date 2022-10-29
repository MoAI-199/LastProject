using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers; 
using Unity.VisualScripting;
using UnityEngine;

public class Factory : MonoBehaviour {

    const int INIT_CREATE_HENCHMAN_NUM = 10;

    private enum PARENT_TYPE {
        PLAYER1,
        PLAYER2,
        ENEMY,
    }
    private GameObject _prefab_parent;
    private GameObject _prefab_henchman;
    private FamilyManager _family_manager;
    
    private Sprite _1p;
    private Sprite _2p;
    private Parent _parent;
    void Awake( ) {
        _family_manager = GameObject.Find( "Manager" ).GetComponent<FamilyManager>( );
    }

    void Start( ) {
        loadResorces( );
        createFamiry( PARENT_TYPE.PLAYER1, new Vector2( 2.5f, 0.0f ) );
       // createFamiry( PARENT_TYPE.PLAYER2, new Vector2( -2.5f, 0.0f ) );
        //debug��ǂ̐���
        createHenchman( null, new Vector2( 0, 0 ) );
    }

    private void loadResorces( ) {
#if UNITY_EDITOR
        _prefab_parent = Resources.Load( "Prefab/Parent" ) as GameObject;
        _prefab_henchman = Resources.Load( "Prefab/Henchman" ) as GameObject;
        _1p = Resources.Load("sprite_test/1P") as Sprite;
        _2p = Resources.Load("sprite_test/2P") as Sprite;
#endif
    }

    private void createFamiry( PARENT_TYPE type, Vector2 pos ) {
        GameObject parent_obj = createParent( type, pos );
        for( int i = 0; i < INIT_CREATE_HENCHMAN_NUM; i++ ) {
            createHenchman( parent_obj,pos );
        }
    }

    private GameObject createParent( PARENT_TYPE type, Vector2 pos ) {
        //�I�u�W�F�N�g�̐���
        GameObject parent = Instantiate( _prefab_parent );
        //�ړ������̃A�^�b�`
        addMoveComponent( parent, type );
        //�������W
        parent.transform.position = pos;
        //���X�g�ւ̒ǉ�
        _family_manager.addParentList( parent );
        //�^�O�ύX
        parent.tag = FAMILY_DATA.TAG_NAME.PARENT.ToString( );
        return parent;
    }

    private void addMoveComponent( GameObject parent, PARENT_TYPE type ) {
        switch( type ) {
            case PARENT_TYPE.PLAYER1:
                parent.AddComponent<MovePlayer1>( );
                _parent = parent.GetComponent<Parent>();
                var test = _parent.getParemeter();
                test.playernum = _1p;
                break;
            case PARENT_TYPE.PLAYER2:
                parent.AddComponent<MovePlayer2>( );
                _parent = parent.GetComponent<Parent>();
               // _parent.getParemeter().playernum =_2p ;
                _parent.setParemeter(_2p);
                break;
            case PARENT_TYPE.ENEMY:
                break;
        }
    }

    private GameObject createHenchman( GameObject parent, Vector2 pos ) {
        //�I�u�W�F�N�g�̐���
        GameObject henchman = Instantiate( _prefab_henchman );
        //���W�ύX
        henchman.transform.position = pos;
        //���X�g�ǉ�
        _family_manager.addhenchmanList( henchman, parent );
        //�^�O�ύX
        henchman.tag = FAMILY_DATA.TAG_NAME.HENCHMAN.ToString( );
        return henchman;
    }
}
