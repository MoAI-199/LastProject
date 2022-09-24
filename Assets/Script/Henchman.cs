using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Henchman : MonoBehaviour {
    private const float SPEED = 100.0f; //数値をあげると最大速度が下がる
    private const float MASS = 1.0f; // 質量

    private Transform _transform;
    private Transform _transform_parent;
    private FamilyManager _family_manager;
    private Rigidbody2D rigid_body;
    private SpriteRenderer _sprite_renderer;
    private bool _is_move = true;

    private void Awake( ) {
        _family_manager = GameObject.Find( "Manager" ).GetComponent<FamilyManager>( );
        rigid_body = GetComponent<Rigidbody2D>( );
        _sprite_renderer = GetComponent<SpriteRenderer>( );

    }
    private void Start( ) {
        _transform = this.transform;
        _transform_parent = _family_manager.getObjectParent( this.gameObject ).transform;
        rigid_body.mass = MASS;
        _sprite_renderer.color = new Color( 1, Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ) );
    }

    private void Update( ) {
        move( );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        FAMILY_DATA.RELATIONSHIP_TYPE type = examineRelationshipType( collision.gameObject );
        actionHitEvent( type, collision.gameObject );
    }

    private void move( ) {
        if( _transform == null || _transform_parent == null ){
            return;
        }
        float distance = Vector2.Distance( _transform.position, _transform_parent.position );
        if( _is_move ) {
            _transform.position = Vector2.Lerp( _transform.position, _transform_parent.position, distance / SPEED );
        }
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
                hitEventEnemyHenchman( target_obj);
                break;
        }
    }

    /// <summary>動きを止める</summary>
    private void hitEventMyParent( ) {
        _is_move = false;
    }

    /// <summary>特になし</summary>
    private void hitEventMyHenchman( ) {
        //Debug.Log( "MyHenchman" );
    }
    /// <summary>特になし</summary>
    private void hitEventEnemyParent( ) {
       // Debug.Log( "EnemyParent" );
    }
    /// <summary>自分が消える</summary>
    private void hitEventEnemyHenchman( GameObject target_obj) {
       // Debug.Log( "EnemyHenchman" );
        _family_manager.removeHenchman( this.gameObject );
        _family_manager.removeHenchman( target_obj );
        Destroy( this.gameObject );
        Destroy( target_obj );
    }

    /// <summary>ターゲットの関係性を取得する</summary>
    private FAMILY_DATA.RELATIONSHIP_TYPE examineRelationshipType( GameObject obj ) {
        if( _family_manager == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
        }
        // 自分の親である。ターゲットのオブジェクト＝＝自分の親オブジェクト
        if( obj == _family_manager.getObjectParent( this.gameObject ) ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_PARENT;
        }
        // 自分以外の親である。ターゲットの親オブジェクト＝＝NULL
        if( _family_manager.getObjectParent( obj ) == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT;
        }
        // 親が同じ子分である。ターゲットの親オブジェクト==自分の親オブジェクト
        if( _family_manager.getObjectParent( obj ) == _family_manager.getObjectParent( this.gameObject ) ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_HENCHMAN;
        }
        // 親が違う子分である。ターゲットの親オブジェクト！＝自分の親オブジェクト
        if( _family_manager.getObjectParent( obj ) != _family_manager.getObjectParent( this.gameObject ) ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN;
        }
        return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
    }
}
