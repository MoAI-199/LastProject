using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FamilyManager : MonoBehaviour {
    private List<GameObject> _parent_list;
    /// <summary>子分をキーとして親をコンテンツとする</summary>
    private Dictionary<GameObject, GameObject> _henchman_obj_list;
    /// <summary>子分をキーとして親をコンテンツとする</summary>
    private Dictionary<GameObject, Parent> _henchman_list;
    private void Awake( ) {
        _parent_list = new List<GameObject>( );
        _henchman_obj_list = new Dictionary<GameObject, GameObject>( );
        _henchman_list = new Dictionary<GameObject, Parent>( );
    }

    private void Start( ) {
    }

    private void Update( ) {
        switch( GameManager.instatnce.getGameState( ) ) {
            case COMMON_DATA.GAME_STATE_TYPE.GUIDE:
            case COMMON_DATA.GAME_STATE_TYPE.INPUT_NAME:
            case COMMON_DATA.GAME_STATE_TYPE.RESULT:
            case COMMON_DATA.GAME_STATE_TYPE.NONE:
                return;
            case COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING:
                judgmentVictory( );
                break;
        }
        Debug.Log($"parent_num:{getParentCount( )}");
        Debug.Log($"henchman_num:{_henchman_list.Count}");
    }

    /// <summary>
    /// 勝敗判定と勝者の名前の保存
    /// </summary>
    private void judgmentVictory( ){
        switch( GameManager.instatnce.getGameMode( ) ) {
            case COMMON_DATA.GAME_MODE.PVP:
                if( _parent_list.Count == 1 ) {
                    //勝者の名前を保存
                    keepWinnerName( _parent_list[ 0 ].GetComponent<Parent>( ).getParemeter( ).name );
                 } else if( _parent_list.Count <= 0 ) {
                    //引き分け
                    keepWinnerName( "" );
                 }
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                if( GameManager.instatnce.getPlayer1( ) == null ) {
                    GameManager.instatnce.doneGameStatus( );
                }
                break;
        }
        //名前を保存して次のシーンに移動
        void keepWinnerName ( string name ){
            GameManager.instatnce.getUserData( ).setWinnerName( name );
            GameManager.instatnce.doneGameStatus( );
        }
    }

    /// <summary>新規親を追加</summary>
    public void addParentList( GameObject pearent ) {
        _parent_list.Add( pearent );
    }

    /// <summary>新規子分を追加</summary>
    public void addhenchmanList( GameObject henchman_go, GameObject parent_go ) {
        _henchman_obj_list.Add( henchman_go, parent_go );
        Parent parent = null;
        if( parent_go != null ) {
            parent = parent_go.GetComponent<Parent>( );
        }
        _henchman_list.Add( henchman_go, parent );
    }

    public void removeParent( GameObject pearent ) {
        _parent_list.Remove( pearent );
    }

    public void removeHenchman( GameObject henchman ) {
        _henchman_obj_list.Remove( henchman );
    }

    public GameObject getParentObject( GameObject henchman ) {
        if( !_henchman_obj_list.ContainsKey( henchman ) ) {
            return null;
        }
        return _henchman_obj_list[ henchman ];
    }

    /// <summary>指定した親に子分を配属する（親の変更）</summary>
    public void assignPearentToHenchman( GameObject henchman, GameObject parent ) {
        _henchman_obj_list[ henchman ] = parent;
        _henchman_list[ henchman ] = parent.GetComponent<Parent>( );
    }

    /// <summary>子分をキーに親を取得</summary>
    public Parent getParent( GameObject henchman ) {
        return _henchman_list[ henchman ];
    }

    public int getParentCount( ) {
        return _parent_list.Count;
    }

    public GameObject getWinnerParent( ) {
        return _parent_list[ 0 ];
    }
}
