using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>移動に関する共通項目（移動の基底クラス）</summary>
public class MoveCommonBase : MonoBehaviour {
    public bool _moving = false;

    public bool isMoving( ){
        return _moving;
    }

    /// <summary>
    /// これを使って動いてるかどうかのフラグを切り替える
    /// ※派生クラスでのみ呼び出し可能（むやみやたらに切り替えないようにするため）
    /// </summary>
    protected void setMoving( bool moveing ){
        _moving = moveing;
    }

}
