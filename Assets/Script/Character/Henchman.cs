using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

enum MOVE_TARGET_TYPE {
    PARENT,
    FRONT,
    BACK,
};

public class Henchman : CharacterBase {
    [SerializeField]private float SPEED = 0.05f; //数値をあげると最大速度が下がる 
    [SerializeField]private float MASS = 1.0f; // 質量
    [SerializeField]private float RESET_TIME = 1.5f; //親停止時に親の前まで動く時間

    private bool _is_move = true;
    private float _stop_time = 0.0f;
    protected override void setup( ) {
        Application.targetFrameRate = 30;
        _rigid_body.mass = MASS;
        _parameter.my_parent = _family_manager.getParent( this.gameObject );
        _sprite_renderer.color = new Color( 1, Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ) );
    }

    protected override void update( ) {
        move( );
    }
    protected override void hitAllyParent( GameObject target ) {
        _is_move = false; //動きを止める
    }
    protected override void hitAllyHenchman( GameObject target ) {
    }
    protected override void hitEnemyParent( GameObject target ) {
        //自分が野良のときの処理
        if( _parameter.my_parent == null ) {
            base.assignHenchman( target );
        }
    }
    protected override void hitEnemyHenchman( GameObject target ) {
        //自分が野良の処理
        if( _parameter.my_parent == null ) {
            base.assignHenchman( target );
        } else {
            base.deleteEvent( );
        }
    }
    protected override void hitWildHenchman( GameObject target ) {
    }

    private void move( ) {
        if( _transform == null || _parameter.my_parent == null ) {
            return;
        }
        //移動先のタイプの切り替え
        MOVE_TARGET_TYPE move_type = updateTargetType( );

        //移動先の座標の切り替え
        Vector2 target_pos = changeMoveTargetPosition( move_type, _parameter.my_parent.getParemeter( ) );
        //目的地と自分の距離
        float distance = Vector2.Distance( _transform.position, target_pos );

        /*
        //移動処理
        if( _is_move ) {
            _transform.position = Vector2.Lerp( _transform.position, target_pos, distance  * SPEED  );
        }
        */
        //test code
        float add_x = ( target_pos.x - _transform.position.x ) / 100.0f * SPEED; //１フレームでの移動量
        float add_y = ( target_pos.y - _transform.position.y ) / 100.0f * SPEED; //１フレームでの移動量
        Vector2 pos = _transform.position;
        pos.x += add_x;
        pos.y += add_y;
        _transform.position = pos;

        //一定の距離離れたら動き出す
        if( distance > 2.0f ) {
            _is_move = true;
        }
    }

    private Vector2 changeMoveTargetPosition( MOVE_TARGET_TYPE target_type, Parameter parameter ) {
        switch( target_type ) {
            case MOVE_TARGET_TYPE.PARENT:
                //親に向けて移動する
                return _parameter.my_parent.getParemeter( ).pos;
            case MOVE_TARGET_TYPE.FRONT:
                // 親の少し前に移動する
                return parameter.pos + parameter.force;
            case MOVE_TARGET_TYPE.BACK:
                // 親の少し後ろに移動する
                return parameter.pos - ( parameter.force / 2.0f );
        }
        return Vector2.zero;
    }

    /// <summary> 停止時間に応じて目的地を変更する</summary>
    private MOVE_TARGET_TYPE updateTargetType( ) {
        if( _parameter.my_parent.getParemeter( ).is_moveing ) {
            _stop_time = 0.0f;
        } else {
            _stop_time += Time.deltaTime;
        }

        if( _stop_time <= 0.0f ) {
            return MOVE_TARGET_TYPE.PARENT; //親に向かう
        } else if( _stop_time < RESET_TIME ) {
            return MOVE_TARGET_TYPE.FRONT; //親より少し前向かう
        } else if( _stop_time < RESET_TIME + 1.5f ) {
            return MOVE_TARGET_TYPE.BACK;　//親の少し後ろに向かう
        } else {
            return MOVE_TARGET_TYPE.PARENT;
        }
    }

   
}
