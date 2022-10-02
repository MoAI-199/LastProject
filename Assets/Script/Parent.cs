using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Parent : MonoBehaviour {
    public struct Parameter {
        public float speed; //�萔
        public Vector2 pos; //���ݒn
        public Vector2 velocity; //�ړ���
    }

    private const float SPEED = 0.03f; // �ړ����x
    private const float MASS = 100.0f; // ����

    FamilyManager _family_manager;
    Rigidbody2D _rigid_body;
    Parameter _parameter;
    
    private void Awake( ) {
        _rigid_body = GetComponent<Rigidbody2D>( );
    }
    void Start( ) {
        _rigid_body.mass = MASS;
        _family_manager = GameObject.Find( "Manager" ).GetComponent<FamilyManager>( );
        _parameter.speed = SPEED;
    }

    void Update( ) {
        _parameter.pos = transform.position;
        _parameter.velocity = _rigid_body.velocity;
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        FAMILY_DATA.RELATIONSHIP_TYPE type = examineRelationshipType( collision.gameObject );
        actionHitEvent( type, collision.gameObject );
    }

    private void actionHitEvent( FAMILY_DATA.RELATIONSHIP_TYPE type, GameObject target ) {
        switch( type ) {
            case FAMILY_DATA.RELATIONSHIP_TYPE.MY_HENCHMAN:
                hitEventMyHenchman( );
                break;
            case FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT:
                hitEventEnemyParent( target );
                break;
            case FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN:
                hitEventEnemyHenchman( );
                break;
        }
    }

    private void hitEventMyHenchman( ) {
    }

    private void hitEventEnemyParent( GameObject target ) {
        _family_manager.removeParent( this.gameObject );
        _family_manager.removeParent( target );
        Destroy( this.gameObject );
        Destroy( target );
    }

    private void hitEventEnemyHenchman( ) {
        _family_manager.removeParent( this.gameObject );
        Destroy( this.gameObject );
    }

    /// <summary>�^�[�Q�b�g�̊֌W�����擾����</summary>
    private FAMILY_DATA.RELATIONSHIP_TYPE examineRelationshipType( GameObject obj ) {
        if( _family_manager == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
        }
        GameObject target_parent = _family_manager.getParentObject( obj );
        // �G�̐e�ł���B�^�[�Q�b�g�̐e�I�u�W�F�N�g����NULL
        if( obj.tag == FAMILY_DATA.TAG_NAME.PARENT.ToString( ) &&
            _family_manager.getParentObject( obj ) == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT;
        }
        // �����̎q���ł���B�^�[�Q�b�g�̐e�I�u�W�F�N�g==�����̃I�u�W�F�N�g
        if( obj.tag == FAMILY_DATA.TAG_NAME.HENCHMAN.ToString( ) &&
            _family_manager.getParentObject( obj ) == this.gameObject ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_HENCHMAN;
        }
        // �e���Ⴄ�q���ł���B�^�[�Q�b�g�̐e�I�u�W�F�N�g�I�������̃I�u�W�F�N�g
        if( obj.tag == FAMILY_DATA.TAG_NAME.HENCHMAN.ToString( ) &&
            _family_manager.getParentObject( obj ) != this.gameObject &&
            _family_manager.getParentObject( obj ) != null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN;
        }
        return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
    }

    public Parameter getParemeter(){
        return _parameter;
    }
}


