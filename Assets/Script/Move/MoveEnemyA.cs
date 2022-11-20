using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveEnemyA : MoveCommonBase {
    float _move_change_time = 0;
    void Start( ) {
        _move_change_time = 0;
    }

    protected override void setup( ) {
    }
    protected override void update( ) {
        _move_change_time += Time.deltaTime;
        move( );
    }

    private void move( ) {
        setMoving( true );
        int move_type =0;
        if ( _move_change_time <= 3f ) {
            doMove( MOVE_TYPE.LEFT );
        }else if( _move_change_time < 6f){
            doMove( MOVE_TYPE.UP );
        }else if( _move_change_time < 9f ) {
            doMove( MOVE_TYPE.RIGHT );
        } else if( _move_change_time < 12f ) {
            doMove( MOVE_TYPE.DOWN );
        }else{
            _move_change_time = 0.0f;
        }
    }
}