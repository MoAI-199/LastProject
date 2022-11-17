using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public class Camera : MonoBehaviour{

    private GameObject _target_obj;
    private Vector3 _target_pos;
    private void Start( ) {
        _target_pos = Vector3.zero;
    }

    private void Update( ) {
        if( _target_obj == null ){
            _target_obj = GameManager.instatnce.getPlayer1( ); 
        }
        if( _target_obj == null ) {
            return;
        }
        switch( GameManager.instatnce.getGameMode( ) ) {
        case COMMON_DATA.GAME_MODE.PVP:
            break;
        case COMMON_DATA.GAME_MODE.CHELLENGE:
            this.gameObject.transform.position = new Vector3( _target_obj.transform.position.x,
                                                              _target_obj.transform.position.y,
                                                              this.gameObject.transform.position.z );

            break;
        }

    }

    
}
