using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveEnemyA : MoveCommonBase {
    float _move_change_time = 0;
    int _move_type = 0;
    void Start( ) {
    }

    protected override void setup( ) {
    }
    protected override void update( ) {
        _move_change_time += Time.deltaTime;
        _move_change_time %= 3.0f;
        move( );
    }

    private void move( ) {
        setMoving( true );
        if ( _move_change_time >= 2 ) {
            _move_change_time = 0;
            _move_type = Random.Range( 0, 4 );
        }
        doMove( ( MOVE_TYPE )_move_type );
    }
}
