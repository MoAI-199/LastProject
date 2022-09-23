using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Henchman : MonoBehaviour {
    private const float SPEED = 100.0f; //���l��������ƍő呬�x��������
    private const float MASS = 1.0f; // ����

    private Transform _transform = null;
    private Transform _transform_parent = null;
    private FamilyManager _family_manager = null;
    private Rigidbody2D rigid_body;
    private bool _is_move = true;

    private void Awake( ) {
        _family_manager = GameObject.Find( "Manager" ).GetComponent<FamilyManager>( );
        rigid_body = GetComponent<Rigidbody2D>( );

    }
    private void Start( ) {
        _transform = this.transform;
        _transform_parent = _family_manager.getObjectParent( this.gameObject ).transform;
        rigid_body.mass = MASS;

    }

    private void Update( ) {
        move( );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        FAMILY_DATA.RELATIONSHIP_TYPE type = examineRelationshipType( collision.gameObject );
        actionHitEvent( type );
    }

    private void move( ) {
        float distance = Vector2.Distance( _transform.position, _transform_parent.position );
        if( _is_move ) {
            _transform.position = Vector2.Lerp( _transform.position, _transform_parent.position, distance / SPEED );
        }
        if( distance > 1.0f ) {
            _is_move = true;
        }
    }

    private void actionHitEvent( FAMILY_DATA.RELATIONSHIP_TYPE type ) {
        switch( type ) {
            case FAMILY_DATA.RELATIONSHIP_TYPE.MY_PARENT:
                hitEventMyParent( );
                break;
            case FAMILY_DATA.RELATIONSHIP_TYPE.MY_HENCHMAN:
                hitEventMyHenchman( );
                break;
            case FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT:
                hitEventEnemyParent( );
                break;
            case FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN:
                hitEventEnemyHenchman( );
                break;
        }
    }

    private void hitEventMyParent( ) {
        _is_move = false;
    }
    private void hitEventMyHenchman( ) {
        //Debug.Log( "MyHenchman" );
    }
    private void hitEventEnemyParent( ) {
       // Debug.Log( "EnemyParent" );
    }
    private void hitEventEnemyHenchman( ) {
       // Debug.Log( "EnemyHenchman" );
    }

    /// <summary>�^�[�Q�b�g�̊֌W�����擾����</summary>
    private FAMILY_DATA.RELATIONSHIP_TYPE examineRelationshipType( GameObject obj ) {
        if( _family_manager == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
        }
        // �����̐e�ł���B�^�[�Q�b�g�̃I�u�W�F�N�g���������̐e�I�u�W�F�N�g
        if( obj == _family_manager.getObjectParent( this.gameObject ) ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_PARENT;
        }
        // �����ȊO�̐e�ł���B�^�[�Q�b�g�̐e�I�u�W�F�N�g����NULL
        if( _family_manager.getObjectParent( obj ) == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT;
        }
        // �e�������q���ł���B�^�[�Q�b�g�̐e�I�u�W�F�N�g==�����̐e�I�u�W�F�N�g
        if( _family_manager.getObjectParent( obj ) == _family_manager.getObjectParent( this.gameObject ) ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_HENCHMAN;
        }
        // �e���Ⴄ�q���ł���B�^�[�Q�b�g�̐e�I�u�W�F�N�g�I�������̐e�I�u�W�F�N�g
        if( _family_manager.getObjectParent( obj ) != _family_manager.getObjectParent( this.gameObject ) ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN;
        }
        return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
    }
}
