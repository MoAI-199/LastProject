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
    private bool _is_update  = false;
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
        float speed = _pearent.getParemeter( ).speed;
        setMoving( false );
        Vector3 new_pos = new Vector3( );
        switch( type ) {
            case MOVE_TYPE.UP:
                new_pos = new Vector3( 0, -speed, 0 );
                setMoving( true );
                break;
            case MOVE_TYPE.DOWN:
                new_pos = new Vector3( 0, speed, 0 );
                setMoving( true );
                break;
            case MOVE_TYPE.LEFT:
                new_pos = new Vector3( -speed, 0, 0 );
                setMoving( true );
                break;
            case MOVE_TYPE.RIGHT:
                new_pos = new Vector3( speed, 0, 0 );
                setMoving( true );
                break;
        }
        this.gameObject.transform.position += new_pos;
    }

    private void settingMove(){
    } 
}
