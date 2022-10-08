using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Parent : MonoBehaviour {
    public struct Parameter {
        public float speed; //定数
        public Vector2 pos; //現在地
        public Vector2 velocity; //移動量
        public bool is_moveing; //移動中か否か
    }

    private const float SPEED = 0.03f; // 移動速度
    private const float MASS = 100.0f; // 質量

    FamilyManager _family_manager;
    Rigidbody2D _rigid_body;
    MoveCommonBase _move_compornent;
    Parameter _parameter;
    Vector2 _befor_pos;
    
    private void Awake( ) {
        _rigid_body = GetComponent<Rigidbody2D>( );
    }
    void Start( ) {
        _rigid_body.mass = MASS;
        _parameter.speed = SPEED;
        _family_manager = GameObject.Find( "Manager" ).GetComponent<FamilyManager>( );
        _move_compornent = GetComponent< MoveCommonBase >( );
    }

    void Update( ) {
        _parameter.pos = transform.position;
        _parameter.is_moveing = _move_compornent.isMoving( );
        //velocityの計算
        Debug.Log( "velo:"+ _parameter.velocity );
        if( Vector2.Distance( _parameter.pos, _befor_pos ) > 0.01f ) {
            _parameter.velocity = ( _parameter.pos - _befor_pos ) * 50;
        }
        _befor_pos = transform.position;

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

    /// <summary>ターゲットの関係性を取得する</summary>
    private FAMILY_DATA.RELATIONSHIP_TYPE examineRelationshipType( GameObject obj ) {
        if( _family_manager == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
        }
        GameObject target_parent = _family_manager.getParentObject( obj );
        // 敵の親である。ターゲットの親オブジェクト＝＝NULL
        if( obj.tag == FAMILY_DATA.TAG_NAME.PARENT.ToString( ) &&
            _family_manager.getParentObject( obj ) == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT;
        }
        // 自分の子分である。ターゲットの親オブジェクト==自分のオブジェクト
        if( obj.tag == FAMILY_DATA.TAG_NAME.HENCHMAN.ToString( ) &&
            _family_manager.getParentObject( obj ) == this.gameObject ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_HENCHMAN;
        }
        // 親が違う子分である。ターゲットの親オブジェクト！＝自分のオブジェクト
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


