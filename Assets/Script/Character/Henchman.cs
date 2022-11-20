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
    [SerializeField]
    [Range( 0.0f, 1.0f )]
    private float SPEED = 0.05f;
    [SerializeField]
    [Range( 0.0f, 100.0f )]
    private float MAX_RANGE = 50.0f; //�X�s�[�h�̍Œ�l
    [SerializeField]
    private float MASS = 1.0f; // ����
    [SerializeField]
    private float RESET_TIME = 1.5f; //�e��~���ɐe�̑O�܂œ�������

    private float BACK_TIME = 1.5f;

    private float STOP_TIME = 1.0f;

    private bool _is_move = true;
    private float _stop_time = 0.0f;

    protected override void setup( ) {
        Application.targetFrameRate = 30;
        _rigid_body.mass = MASS;
        _parameter.my_parent = _family_manager.getParent( this.gameObject );
        //_sprite_renderer.color = new Color( 1, Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ) );
    }

    protected override void update( ) {
        _parameter.is_moveing = _is_move;
        
        move( );
       
    }
    protected override void hitAllyParent( GameObject target ) {
        _is_move = false; //�������~�߂�
    }
    protected override void hitAllyHenchman( GameObject target ) {
        setTexture( false, COMMON_DATA.TEXTURE_COLOR.PINK );

    }
    protected override void hitEnemyParent( GameObject target ) {

        if( _parameter.my_parent == null ) { //��������ǂ̂Ƃ��̏���
            base.assignHenchman( target );
            _parameter.my_parent.addHenchmanCount( );
            return;
        }
        _parameter.my_parent.addKillCount( target );

    }
    protected override void hitEnemyHenchman( GameObject target ) {
        if( _parameter.my_parent == null ) { //��������ǂ̏���
            base.assignHenchman( target );
            _parameter.my_parent.addHenchmanCount( );
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
      
        //�ړ��ʂ��擾
        Vector2 add_pos = movement( target_pos, move_type );

        //���W�̍X�V
        Vector2 pos = _transform.position;
        pos.x += add_pos.x;
        pos.y += add_pos.y;
        _transform.position = pos;

        //�ړ��ʂ̕ۑ�
        _parameter.force = add_pos; 

        //�ړI�n�Ǝ����̋���
        float distance = Vector2.Distance( _transform.position, target_pos );

        //���̋������ꂽ�瓮���o��
        if( distance > 2.0f ) {
            _is_move = true;
        }
        //��~���Ă���Ƃ��͓����Ă��Ȃ�
        if( _parameter.force.x == 0 && _parameter.force.y == 0 ) {
            _is_move = false;
        }
    }

    private Vector2 movement( Vector2 target_pos, MOVE_TARGET_TYPE move_type ) {
        float add_x = ( target_pos.x - _transform.position.x ) / ( MAX_RANGE * SPEED ); //�P�t���[���ł̈ړ���
        float add_y = ( target_pos.y - _transform.position.y ) / ( MAX_RANGE * SPEED ); //�P�t���[���ł̈ړ���
        //������~
        if( move_type == MOVE_TARGET_TYPE.PARENT &&
            !_parameter.my_parent.getParameter( ).is_moveing ) {
            if( _stop_time > RESET_TIME + BACK_TIME + STOP_TIME ) {
                add_x = 0.0f;
                add_y = 0.0f;
                _is_move = false;
            }
        }
        return new Vector2( add_x, add_y );
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
        } else if( _stop_time < RESET_TIME + BACK_TIME ) {
            return MOVE_TARGET_TYPE.BACK;�@//�e�̏������Ɍ�����
        } else {
            return MOVE_TARGET_TYPE.PARENT;
        }
    }
}
