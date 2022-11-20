using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveEnemyC : MoveCommonBase {
    float _move_change_time;
    void Start( ) {
    }

    protected override void setup( ) {
        _move_change_time = 0;
    }
    protected override void update( ) {
        _move_change_time += Time.deltaTime;
        move( );
    }

    private void move( ) {
        setMoving( true );
        if( _move_change_time <= 3f ) {
            doMove( MOVE_TYPE.DOWN );
        } else if( _move_change_time < 6f ) {
            doMove( MOVE_TYPE.UP );
        } else {
            _move_change_time = 0.0f;
        }
    }
}
