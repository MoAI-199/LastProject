using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveEnemyC : MoveCommonBase {
    GameObject _target;
    void Start( ) {
        _target = GameManager.instatnce.getPlayer1( );
    }

    protected override void setup( ) {
    }
    protected override void update( ) {
        move( );
    }

    private void move( ) {
        setMoving( true );
        if( _target == null ) {
            return;
        }
        Vector2 my_pos = new Vector2( this.gameObject.transform.position.x, this.gameObject.transform.position.y );
        Vector2 target_pos = new Vector2( _target.transform.position.x, _target.transform.position.y );
        if( my_pos.x < target_pos.x ){
            doMove( MOVE_TYPE.RIGHT);
        } else{
            doMove( MOVE_TYPE.LEFT);
        }
        if( my_pos.y < target_pos.y ) {
            doMove( MOVE_TYPE.DOWN );
        } else {
            doMove( MOVE_TYPE.UP );
        }

    }
}
