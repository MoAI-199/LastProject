using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public class Camera : MonoBehaviour {

    private GameObject _target_obj;
    private Vector3 _target_pos;
    private void Start( ) {
        _target_pos = Vector3.zero;
    }

    private void Update( ) {
        if( _target_obj == null ) {
            _target_obj = GameManager.instatnce.getPlayer1( );
        }
        if( _target_obj == null ) {
            return;
        }
        switch( GameManager.instatnce.getGameMode( ) ) {
            case COMMON_DATA.GAME_MODE.PVP:
                break;
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                Vector3 new_pos = _target_obj.transform.position;
                if( _target_obj.transform.position.x >= 17.5 || _target_obj.transform.position.x <= -17.5 ) {
                    new_pos.x = this.transform.position.x;
                }
                if( _target_obj.transform.position.y >= 9.5 || _target_obj.transform.position.y <= -9.5 ) {
                    new_pos.y = this.transform.position.y;
                }
                new_pos.z = this.transform.position.z;
                this.gameObject.transform.position = new_pos;
                break;
        }

    }


}
