using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

public class Henchman : MonoBehaviour {
    public struct Parameter {
        public Vector2 pos; //���ݒn
        public Vector2 velocity; //�ړ���
        public Parent my_parent;
    }

    private const float SPEED = 200.0f; //���l��������ƍő呬�x��������
    private const float MASS = 1.0f; // ����
    private const float RESET_TIME = 1.5f; //�e��~���ɐe�̑O�܂œ�������

    private Transform _transform;
    private FamilyManager _family_manager;
    private Rigidbody2D rigid_body;
    private SpriteRenderer _sprite_renderer;
    private Parameter _parameter;
    private bool _is_move = true;
    private float _stop_time = 0.0f;

    private void Awake( ) {
        _family_manager = GameObject.Find( "Manager" ).GetComponent<FamilyManager>( );
        rigid_body = GetComponent<Rigidbody2D>( );
        _sprite_renderer = GetComponent<SpriteRenderer>( );

    }
    private void Start( ) {
        _transform = this.transform;
        rigid_body.mass = MASS;
        _sprite_renderer.color = new Color( 1, Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ) );
        _parameter.my_parent = _family_manager.getParent( this.gameObject );
    }

    private void Update( ) {
        move( );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        FAMILY_DATA.RELATIONSHIP_TYPE type = examineRelationshipType( collision.gameObject );
        actionHitEvent( type, collision.gameObject );
    }

    private void move( ) {
        if( _transform == null || _parameter.my_parent == null ) {
            return;
        }
        //�ړ���̂��肩��
        bool convart_target = true;
        if( !_parameter.my_parent.getParemeter( ).is_moveing ) {
            convart_target = false;
            _stop_time += Time.deltaTime;
            if( _stop_time > RESET_TIME ) {
                convart_target = true;
            }
        }else{
            _stop_time = 0.0f;
        }

        Vector2 target_pos;
        if( convart_target ) {
            //�e�Ɍ����Ĉړ�����
            target_pos = _parameter.my_parent.getParemeter( ).pos; 
        } else {
            //�e�̏����O�Ɉړ�����
            var my_parent_parameter = _parameter.my_parent.getParemeter( );
            target_pos = my_parent_parameter.pos + my_parent_parameter.velocity;
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

    private void actionHitEvent( FAMILY_DATA.RELATIONSHIP_TYPE type, GameObject target_obj ) {
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
                hitEventEnemyHenchman( target_obj );
                break;
            case FAMILY_DATA.RELATIONSHIP_TYPE.WILD_HENCHMAN:
                hitEventWildHenchman( );
                break;
        }
    }

    /// <summary>�������~�߂�</summary>
    private void hitEventMyParent( ) {
        _is_move = false;
    }

    /// <summary>���ɂȂ�</summary>
    private void hitEventMyHenchman( ) {
        //Debug.Log( "MyHenchman" );
    }
    /// <summary>���ɂȂ�</summary>
    private void hitEventEnemyParent( ) {
        // Debug.Log( "EnemyParent" );
    }
    /// <summary>������������</summary>
    private void hitEventEnemyHenchman( GameObject target ) {
        // Debug.Log( "EnemyHenchman" );
        if( _family_manager.getParentObject( this.gameObject ) == null ) {
            //��ǂ̏ꍇ
            assignHenchman( target );
        } else {
            deleteHenchman( target );
        }
    }
    /// <summary>���ɂȂ�</summary>
    private void hitEventWildHenchman( ) {

    }

    /// <summary>�^�[�Q�b�g�̊֌W�����擾����</summary>
    private FAMILY_DATA.RELATIONSHIP_TYPE examineRelationshipType( GameObject target ) {
        GameObject my_parent = _family_manager.getParentObject( this.gameObject );
        GameObject target_parent = _family_manager.getParentObject( target );

        if( _family_manager == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
        }
        // �����̐e�ł���B
        if( target.tag == FAMILY_DATA.TAG_NAME.PARENT.ToString( ) &&
            target == my_parent ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_PARENT;
        }
        // �����ȊO�̐e�ł���B
        if( target.tag == FAMILY_DATA.TAG_NAME.PARENT.ToString( ) &&
            target_parent == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT;
        }
        // �e�������q���ł���B
        if( target.tag == FAMILY_DATA.TAG_NAME.HENCHMAN.ToString( ) &&
            target_parent == my_parent ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_HENCHMAN;
        }
        // �e���Ⴄ�q���ł���B
        if( target.tag == FAMILY_DATA.TAG_NAME.HENCHMAN.ToString( ) &&
            target_parent != my_parent ) {
            //�������쐶�̏ꍇ
            if( target_parent == null ) {
                return FAMILY_DATA.RELATIONSHIP_TYPE.WILD_HENCHMAN;
            }
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN;
        }
        return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
    }

    private void deleteHenchman( GameObject target ) {
        _family_manager.removeHenchman( this.gameObject );
        _family_manager.removeHenchman( target );
        Destroy( this.gameObject );
        Destroy( target );
    }
    private void assignHenchman( GameObject target ) {
        GameObject target_parent = _family_manager.getParentObject( target );
        _family_manager.assignPearentToHenchman( this.gameObject, target_parent );
        //�e���X�V����
        _parameter.my_parent = _family_manager.getParent( this.gameObject );
    }
}
