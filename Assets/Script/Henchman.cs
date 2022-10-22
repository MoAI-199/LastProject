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
    private const float SPEED = 100.0f; //���l��������ƍő呬�x��������
    private const float MASS = 1.0f; // ����
    private const float RESET_TIME = 1.5f; //�e��~���ɐe�̑O�܂œ�������

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
    private void move( ) {
        if( _transform == null || _parameter.my_parent == null ) {
            return;
        }
        //�ړ���̃^�C�v�̐؂�ւ�
        MOVE_TARGET_TYPE move_type = updateTargetType( );

        //�ړ���̍��W�̐؂�ւ�
        Vector2 target_pos = _parameter.my_parent.getParemeter( ).pos;
        var my_parent_parameter = _parameter.my_parent.getParemeter( );
        switch( move_type ) {
            case MOVE_TARGET_TYPE.PARENT:
                //�e�Ɍ����Ĉړ�����
                target_pos = _parameter.my_parent.getParemeter( ).pos;
                break;
            case MOVE_TARGET_TYPE.FRONT:
                // �e�̏����O�Ɉړ�����
                target_pos = my_parent_parameter.pos + my_parent_parameter.velocity;
                break;
            case MOVE_TARGET_TYPE.BACK:
                // �e�̏������Ɉړ�����
                target_pos = my_parent_parameter.pos - (my_parent_parameter.velocity / 2.0f);
                break;
        }

        //�ړI�n�Ǝ����̋���
        float distance = Vector2.Distance( _transform.position, target_pos );
        //�ړ�����
        if( _is_move ) {
            _transform.position = Vector2.Lerp( _transform.position, target_pos, distance / SPEED );
        }
        //���̋������ꂽ�瓮���o��
        if( distance > 1.0f ) {
            _is_move = true;
        }
    }

    /// <summary> ��~���Ԃɉ����ĖړI�n��ύX����</summary>
    private MOVE_TARGET_TYPE updateTargetType( ) {
        if( _parameter.my_parent.getParemeter( ).is_moveing ) {
            _stop_time = 0.0f;
        } else {
            _stop_time += Time.deltaTime;
        }

        if( _stop_time <= 0.0f ) {
            return MOVE_TARGET_TYPE.PARENT; //�e�Ɍ�����
        } else if( _stop_time < RESET_TIME ) {
            return MOVE_TARGET_TYPE.FRONT; //�e��菭���O������
        } else if( _stop_time < RESET_TIME + 1.5f ) {
            return MOVE_TARGET_TYPE.BACK;�@//�e�̏������Ɍ�����
        } else {
            return MOVE_TARGET_TYPE.PARENT;
        }
    }

    private Vector2 

    /// <summary>�������~�߂�</summary>
    protected override void hitAllyParent( GameObject target ) {
        _is_move = false;
    }
    protected override void hitAllyHenchman( GameObject target ) {
    }
    protected override void hitEnemyParent( GameObject target ) {
        //��������ǂ̂Ƃ��̏���
        if( _parameter.my_parent == null ) {
            base.assignHenchman( target );
        }
    }
    protected override void hitEnemyHenchman( GameObject target ) {
        //��������ǂ̏���
        if( _parameter.my_parent == null ) {
            base.assignHenchman( target );
        } else {
            base.deleteEvent( );
        }
    }
    protected override void hitWildHenchman( GameObject target ) {
    }
}
