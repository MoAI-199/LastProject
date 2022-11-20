using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveEnemyB : MoveCommonBase {
    GameObject _target;
    Parent _target_parent;
    float _now_time = 0;
    MOVE_TYPE _type = MOVE_TYPE.LEFT;
    void Start( ) {
        _target = GameManager.instatnce.getPlayer1( );
        _target_parent = _target.GetComponent<Parent>( );
    }

    protected override void setup( ) {
    }
    protected override void update( ) {
        _now_time += Time.deltaTime;
        move( );
    }

    private void move( ) {
        setMoving( true );
        if( _target == null ) {
            return;
        }
        Vector2 my_pos = new Vector2( this.gameObject.transform.position.x, this.gameObject.transform.position.y );
        Vector2 target_pos = new Vector2( _target.transform.position.x, _target.transform.position.y );
        doMove( _type );
        if( _now_time < 1.0f ) {
            return;
        }
        _now_time = 0;

        if( _target_parent.isMove( ) ) { //一定距離でプレイヤーの逆に動く
            if( my_pos.x < target_pos.x ) {
                _type = MOVE_TYPE.LEFT;
            } else if( my_pos.x > target_pos.x ) {
                _type = MOVE_TYPE.RIGHT;
            } else if( my_pos.y < target_pos.y ) {
                _type = MOVE_TYPE.UP;
            } else {
                _type = MOVE_TYPE.DOWN;
            }
        } else {
            //Playerに向かう
            if( my_pos.x < target_pos.x ) {
                _type = MOVE_TYPE.RIGHT;
            } else if( my_pos.y < target_pos.y ) {
                _type = MOVE_TYPE.DOWN;
            } else if( my_pos.x > target_pos.x ) {
                _type = MOVE_TYPE.LEFT;
            } else {
                _type = MOVE_TYPE.UP;
            }
        }
    }
}
