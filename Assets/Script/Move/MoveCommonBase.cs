using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�ړ��Ɋւ��鋤�ʍ��ځi�ړ��̊��N���X�j</summary>
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
    /// ������g���ē����Ă邩�ǂ����̃t���O��؂�ւ���
    /// ���h���N���X�ł̂݌Ăяo���\�i�₽��ɐ؂�ւ��Ȃ��悤�ɂ��邽�߁j
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
    /// ���Ԃ��w�肵���ړ�
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
