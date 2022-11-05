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
    [SerializeField]private float SPEED = 0.05f; //���l��������ƍő呬�x�������� 
    [SerializeField]private float MASS = 1.0f; // ����
    [SerializeField]private float RESET_TIME = 1.5f; //�e��~���ɐe�̑O�܂œ�������

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
        _is_move = false; //�������~�߂�
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

    private void move( ) {
        if( _transform == null || _parameter.my_parent == null ) {
            return;
        }
        //�ړ���̃^�C�v�̐؂�ւ�
        MOVE_TARGET_TYPE move_type = updateTargetType( );

        //�ړ���̍��W�̐؂�ւ�
        Vector2 target_pos = changeMoveTargetPosition( move_type, _parameter.my_parent.getParemeter( ) );
        //�ړI�n�Ǝ����̋���
        float distance = Vector2.Distance( _transform.position, target_pos );

        /*
        //�ړ�����
        if( _is_move ) {
            _transform.position = Vector2.Lerp( _transform.position, target_pos, distance  * SPEED  );
        }
        */
        //test code
        float add_x = ( target_pos.x - _transform.position.x ) / 100.0f * SPEED; //�P�t���[���ł̈ړ���
        float add_y = ( target_pos.y - _transform.position.y ) / 100.0f * SPEED; //�P�t���[���ł̈ړ���
        Vector2 pos = _transform.position;
        pos.x += add_x;
        pos.y += add_y;
        _transform.position = pos;

        //���̋������ꂽ�瓮���o��
        if( distance > 2.0f ) {
            _is_move = true;
        }
    }

    private Vector2 changeMoveTargetPosition( MOVE_TARGET_TYPE target_type, Parameter parameter ) {
        switch( target_type ) {
            case MOVE_TARGET_TYPE.PARENT:
                //�e�Ɍ����Ĉړ�����
                return _parameter.my_parent.getParemeter( ).pos;
            case MOVE_TARGET_TYPE.FRONT:
                // �e�̏����O�Ɉړ�����
                return parameter.pos + parameter.force;
            case MOVE_TARGET_TYPE.BACK:
                // �e�̏������Ɉړ�����
                return parameter.pos - ( parameter.force / 2.0f );
        }
        return Vector2.zero;
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

   
}
