using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�ړ��Ɋւ��鋤�ʍ��ځi�ړ��̊��N���X�j</summary>
public class MoveCommonBase : MonoBehaviour {
    public bool _moving = false;

    public bool isMoving( ){
        return _moving;
    }

    /// <summary>
    /// ������g���ē����Ă邩�ǂ����̃t���O��؂�ւ���
    /// ���h���N���X�ł̂݌Ăяo���\�i�ނ�݂₽��ɐ؂�ւ��Ȃ��悤�ɂ��邽�߁j
    /// </summary>
    protected void setMoving( bool moveing ){
        _moving = moveing;
    }

}
