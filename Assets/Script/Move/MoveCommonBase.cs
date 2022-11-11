using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>移動に関する共通項目（移動の基底クラス）</summary>
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
    /// これを使って動いてるかどうかのフラグを切り替える
    /// ※派生クラスでのみ呼び出し可能（やたらに切り替えないようにするため）
    /// </summary>
    protected void setMoving( bool moveing ){
        _moving = moveing;
    }
}
