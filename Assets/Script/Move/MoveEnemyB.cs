using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveEnemyB : MoveCommonBase {
    GameObject _target;
    Parent _target_parent;
    void Start( ) {
        _target = GameManager.instatnce.getPlayer1( );
        _target_parent = _target.GetComponent<Parent>();
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
        var dis = Vector2.Distance( my_pos, target_pos );


        if( dis < 4 || _target_parent.isMove( ) ) { //一定距離でプレイヤーの逆に動く
            if( my_pos.x < target_pos.x ) {
                doMove( MOVE_TYPE.LEFT );
                return;
            } else if( my_pos.x > target_pos.x ) {
                doMove( MOVE_TYPE.RIGHT );
            }else if( my_pos.y < target_pos.y ) {
                doMove( MOVE_TYPE.UP );
            } else {
                doMove( MOVE_TYPE.DOWN );
            }
            return;
        }
        //Playerに向かう
        if( my_pos.x < target_pos.x ) {
            doMove( MOVE_TYPE.RIGHT );
        } else {
            doMove( MOVE_TYPE.LEFT );
        }
        if( my_pos.y < target_pos.y ) {
            doMove( MOVE_TYPE.DOWN );
        } else {
            doMove( MOVE_TYPE.UP );
        }
    }
}
