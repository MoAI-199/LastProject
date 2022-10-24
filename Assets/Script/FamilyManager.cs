using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FamilyManager : MonoBehaviour {
    private List<GameObject> _parent_list;
    /// <summary>主に親のGameObjectの取得に使用　※キーに子分、コンテンツに親 </summary>
    private Dictionary<GameObject,GameObject> _henchman_obj_list;
    /// <summary>主に親のオブジェクトの取得に使用　※キーに子分、コンテンツに親</summary>
    private Dictionary<GameObject, Parent> _henchman_list;
    private void Awake( ) {
        _parent_list = new List<GameObject>( );
        _henchman_obj_list = new Dictionary<GameObject, GameObject>( );
        _henchman_list = new Dictionary<GameObject, Parent>( );
    }

    /// <summary>�V�K�e��ǉ�</summary>
    public void addParentList( GameObject pearent ) {
        _parent_list.Add( pearent );
    }

    /// <summary>�V�K�q����ǉ�</summary>
    public void addhenchmanList( GameObject henchman_go, GameObject parent_go ) {
        _henchman_obj_list.Add( henchman_go, parent_go );
        Parent parent = null;
        if ( parent_go != null)
        {
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
    
    public GameObject getParentObject( GameObject henchman ){
        if( !_henchman_obj_list.ContainsKey( henchman ) ){
            return null;
        }
        return _henchman_obj_list[ henchman ];
    }

    /// <summary>�w�肵���e�Ɏq����z������i�e�̕ύX�j</summary>
    public void assignPearentToHenchman( GameObject henchman, GameObject parent ){
        _henchman_obj_list[ henchman ] = parent; //親を更新
        _henchman_list[ henchman ] = parent.GetComponent< Parent >( );
    }

    /// <summary>�q�����L�[�ɐe���擾</summary>
    public Parent getParent( GameObject henchman ) {
        return _henchman_list[ henchman ];
    }

    public int getParentCount()
    {
        return _parent_list.Count;
    }

    public GameObject getWinnerParent()
    {
        return _parent_list[0];
    }
}
