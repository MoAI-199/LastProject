using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveEnemyA : MoveCommonBase {
    private enum MOVE_PATTERN {
        CIRCLE,
        REVERCE_CIRCLE,
        STOP,
        MAX,
    }

    private MOVE_PATTERN _move_pattern;
    private Parent _pearent;
    private float _time;
    private float _change_time;
    private bool _is_appear;
    void Start( ) {
        _pearent = GetComponent<Parent>( );
        _time = 0;
        _change_time = 0;
        _is_appear = true;
    }

    protected override void setup( ) {
        _pearent = GetComponent<Parent>( );
    }
    protected override void update( ) {
        _time += Time.deltaTime;
        //初期移動
        if( _is_appear ){
            gameObject.transform.position += new Vector3( 0,0,1); 
            moveAppear( );
            return;
        }
        move( );
    }

    /// <summary>
    /// 特例処理のため、直接座標を変更。登場時の移動処理。画面内に突如生成されないようにする。
    /// </summary>
    void moveAppear( ) {
        Vector3 target_pos = Vector3.zero;
        Vector2 now_pos = new Vector2( this.gameObject.transform.position.x, this.gameObject.transform.position.y );
        //画面中央に移動し続ける
        Debug.Log( $"pos:{now_pos}");
        float distance = Vector2.Distance( now_pos, target_pos );
        if( distance < 0 ) {
            _is_appear = false;
        }
        if( now_pos.x < -2 ) {
            //doMove( MOVE_TYPE.RIGHT );
        } else if( now_pos.x > 2 ) {
            doMove( MOVE_TYPE.LEFT );
        } 
        //if( now_pos.y < -2 ) {
        //    doMove( MOVE_TYPE.DOWN );
        //} else if( now_pos.y > 2){
        //    doMove( MOVE_TYPE.UP );
        //}
        
    }
    private void move( ) {

    }
}
