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
        ENEMY,
    }
    private GameObject _prefab_parent;
    private GameObject _prefab_henchman;
    private FamilyManager _family_manager;
<<<<<<< HEAD
    
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
=======
    private void Awake( ) {
        _family_manager = GameObject.Find( COMMON_DATA.SettingName.FAMILY_MANAGER ).GetComponent<FamilyManager>( );
        loadResorces( );
    }

    private void Start( ) {
        //settingTimeAttackMode( );
        gamemodeSetup( );
        //debug野良の生成
>>>>>>> fa27c691d503a43239c8461bbcc2a857c9c3ea3d
        createHenchman( null, new Vector2( 0, 0 ) );
    }

    private void Update( ) {

    }

    private void loadResorces( ) {
#if UNITY_EDITOR
<<<<<<< HEAD
        _prefab_parent = Resources.Load( "Prefab/Parent" ) as GameObject;
        _prefab_henchman = Resources.Load( "Prefab/Henchman" ) as GameObject;
        _1p = Resources.Load("sprite_test/1P") as Sprite;
        _2p = Resources.Load("sprite_test/2P") as Sprite;
=======
        _prefab_parent = Resources.Load( COMMON_DATA.SettingName.PREFAB_PARENT_PATH ) as GameObject;
        _prefab_henchman = Resources.Load( COMMON_DATA.SettingName.PREFAB_HENCHMAN_PATH ) as GameObject;
>>>>>>> fa27c691d503a43239c8461bbcc2a857c9c3ea3d
#endif
    }

    void gamemodeSetup()
    {
        switch (GameManager.instatnce.getGameMode())
        {
            case COMMON_DATA.GAME_MODE.PVP:
                settingVsMode();
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                settingTimeAttackMode();
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
    }

    private GameObject createParent( PARENT_TYPE type, Vector2 pos ) {
<<<<<<< HEAD
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
=======
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
        addParentName( parent, type );
>>>>>>> fa27c691d503a43239c8461bbcc2a857c9c3ea3d
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
                parent.AddComponent<MoveEnemy>( );
                break;
        }
    }

    private void addParentName( GameObject parent, PARENT_TYPE type ) {
        Parent parent_script = parent.GetComponent<Parent>( );
        switch( type ) {
            case PARENT_TYPE.PLAYER1:
                parent_script.chaneParemeterName( "Player1" );
                break;
            case PARENT_TYPE.PLAYER2:
                parent_script.chaneParemeterName( "Player2" );
                break;
            case PARENT_TYPE.ENEMY:
                parent_script.chaneParemeterName( "Enemy" );
                break;
        }
    }

    private GameObject createHenchman( GameObject parent, Vector2 pos ) {
<<<<<<< HEAD
        //�I�u�W�F�N�g�̐���
        GameObject henchman = Instantiate( _prefab_henchman );
        //���W�ύX
        henchman.transform.position = pos;
        //���X�g�ǉ�
        _family_manager.addhenchmanList( henchman, parent );
        //�^�O�ύX
        henchman.tag = FAMILY_DATA.TAG_NAME.HENCHMAN.ToString( );
=======
        //オブジェクトの生成
        GameObject henchman = Instantiate( _prefab_henchman );
        //座標変更
        henchman.transform.position = pos;
        //リスト追加
        _family_manager.addhenchmanList( henchman, parent );
        //タグ変更
        henchman.tag = COMMON_DATA.TAG_NAME.HENCHMAN.ToString( );
>>>>>>> fa27c691d503a43239c8461bbcc2a857c9c3ea3d
        return henchman;
    }

    private void settingVsMode( ) {
        createFamiry( PARENT_TYPE.PLAYER1, new Vector2( -2.5f, 0.0f ) );
        createFamiry( PARENT_TYPE.PLAYER2, new Vector2( 2.5f, 0.0f ) );
    }

    private void settingTimeAttackMode( ) {
        createFamiry( PARENT_TYPE.PLAYER1, new Vector2( 0.0f, 0.0f ) );
        //createFamiry( PARENT_TYPE.ENEMY, new Vector2( Random.Range( -5.0f, 5.0f ), Random.Range( -3.0f, 3.0f )  ) );
        createFamiry( PARENT_TYPE.ENEMY, new Vector2(3,3) );
    }
}
