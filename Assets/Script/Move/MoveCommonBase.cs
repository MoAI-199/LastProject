using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>移動に関する共通項目（移動の基底クラス）</summary>
public class MoveCommonBase : MonoBehaviour {
    protected enum MOVE_TYPE {
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }

    public bool _moving = false;
    private float _update_time = 0.0f;
    private Parent _pearent;

    private MOVE_TYPE _settime_move_type;
    private float _settime_move_time;
    

    private void Awake( ) {
        _pearent = GetComponent<Parent>( );
    }
    private void Start( ) {
        setup( );
    }
    private void Update( ) {
        _update_time += Time.deltaTime;
        switch( GameManager.instatnce.getGameState( ) ) {
            case COMMON_DATA.GAME_STATE_TYPE.GUIDE:
            case COMMON_DATA.GAME_STATE_TYPE.RESULT:
            case COMMON_DATA.GAME_STATE_TYPE.NONE:
                break;
            case COMMON_DATA.GAME_STATE_TYPE.GAME_PLAYING:
                update( );
                updateMove( );
                break;
        }
    }

    protected virtual void setup( ) {
    }

    protected virtual void update( ) {
    }

    public bool isMoving( ) {
        return _moving;
    }

    /// <summary>
    /// これを使って動いてるかどうかのフラグを切り替える
    /// ※派生クラスでのみ呼び出し可能（やたらに切り替えないようにするため）
    /// </summary>
    protected void setMoving( bool moveing ) {
        _moving = moveing;
    }

    protected void doMove( MOVE_TYPE type ) {
        setMoving( true );
        float speed = _pearent.getParemeter( ).speed;
        Vector3 new_pos = new Vector3( );
        switch( type ) {
            case MOVE_TYPE.UP:
                new_pos = new Vector3( 0, -speed, 0 );
                break;
            case MOVE_TYPE.DOWN:
                new_pos = new Vector3( 0, speed, 0 );
                break;
            case MOVE_TYPE.LEFT:
                new_pos = new Vector3( -speed, 0, 0 );
                break;
            case MOVE_TYPE.RIGHT:
                new_pos = new Vector3( speed, 0, 0 );
                break;
        }
        this.gameObject.transform.position += new_pos;
    }

    /// <summary>
    /// 時間を指定した移動
    /// </summary>
    private void updateMove( ){
        if( _update_time > _settime_move_time){
            _settime_move_time = 0.0f;
            return;
        }
        doMove( _settime_move_type );
    }
    private void doMoveSetTimer( MOVE_TYPE type, float time ) {
        _update_time = 0.0f;
        doMove(type);
    }

}
