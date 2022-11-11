using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�ړ��Ɋւ��鋤�ʍ��ځi�ړ��̊��N���X�j</summary>
public class MoveCommonBase : MonoBehaviour {
    public bool _moving = false;

    private void Start( ) {
        setup( );
    }
    private void Update( ) {
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

    protected virtual void setup(){
    }

    protected virtual void update(){
    }

    public bool isMoving( ){
        return _moving;
    }

    /// <summary>
    /// ������g���ē����Ă邩�ǂ����̃t���O��؂�ւ���
    /// ���h���N���X�ł̂݌Ăяo���\�i�₽��ɐ؂�ւ��Ȃ��悤�ɂ��邽�߁j
    /// </summary>
    protected void setMoving( bool moveing ){
        _moving = moveing;
    }
}
