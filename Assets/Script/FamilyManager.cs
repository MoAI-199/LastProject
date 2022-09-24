using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyManager : MonoBehaviour {
    private List<GameObject> _parent_list;
    /// <summary>キーに子分、コンテンツに親 </summary>
    private Dictionary<GameObject,GameObject> _henchman_list;

    private void Awake( ) {
        _parent_list = new List<GameObject>( );
        _henchman_list = new Dictionary<GameObject, GameObject>( );
    }
    public void addParentList( GameObject pearent ) {
        _parent_list.Add( pearent );
    }

    public void addhenchmanList( GameObject henchman, GameObject pearent ) {
        _henchman_list.Add( henchman, pearent );
    }

    public void removeParent( GameObject pearent ) {
        _parent_list.Remove( pearent );
    }

    public void removeHenchman( GameObject henchman ) {
        _henchman_list.Remove( henchman );
    }
    
    public GameObject getObjectParent( GameObject henchman ){
        if( !_henchman_list.ContainsKey( henchman ) ){
            return null;
        }
        return _henchman_list[ henchman ];
    }
}
