using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Henchman : MonoBehaviour {

    public struct Parameter{
        public float speed{ get;set; } //定数
        public Vector2 pos; //現在地
        public Vector2 velocity; //移動量
        public Parent my_parent;
    }

    private const float SPEED = 200.0f; //数値をあげると最大速度が下がる
    private const float MASS = 1.0f; // 質量

    private Transform _transform;
    private FamilyManager _family_manager;
    private Rigidbody2D rigid_body;
    private SpriteRenderer _sprite_renderer;
    private bool _is_move = true;
    private Parameter _parameter;

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
        Vector2 target_pos = _parameter.my_parent.getParemeter( ).pos;
        float distance = Vector2.Distance( _transform.position, target_pos );
        if( _is_move ) {
            _transform.position = Vector2.Lerp( _transform.position, target_pos, distance / SPEED );
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
            case FAMILY_DATA.RELATIONSHIP_TYPE.WILD_HENCHMAN:
                hitEventWildHenchman( );
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
    private void hitEventEnemyHenchman( GameObject target) {
        // Debug.Log( "EnemyHenchman" );
        if( _family_manager.getParentObject( this.gameObject ) == null){
            //野良の場合
            assignHenchman(target);
        } else {
            deleteHenchman( target );
		}
    }
    /// <summary>特になし</summary>
    private void hitEventWildHenchman( ){

    }

    /// <summary>ターゲットの関係性を取得する</summary>
    private FAMILY_DATA.RELATIONSHIP_TYPE examineRelationshipType( GameObject target ) {
        GameObject my_parent = _family_manager.getParentObject(this.gameObject);
        GameObject target_parent = _family_manager.getParentObject(target);
        
        if ( _family_manager == null ) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
        }
        // 自分の親である。
        if (target.tag == FAMILY_DATA.TAG_NAME.PARENT.ToString() && 
            target == my_parent) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_PARENT;
        }
        // 自分以外の親である。
        if( target.tag == FAMILY_DATA.TAG_NAME.PARENT.ToString( ) &&
            target_parent == null) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT;
        }
        // 親が同じ子分である。
        if(target.tag == FAMILY_DATA.TAG_NAME.HENCHMAN.ToString() && 
            target_parent == my_parent) {
            return FAMILY_DATA.RELATIONSHIP_TYPE.MY_HENCHMAN;
        }
        // 親が違う子分である。
        if(target.tag == FAMILY_DATA.TAG_NAME.HENCHMAN.ToString() &&
            target_parent != my_parent) {
            //自分が野生の場合
            if( target_parent == null )
			{
                return FAMILY_DATA.RELATIONSHIP_TYPE.WILD_HENCHMAN;
			}
            return FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN;
        }
        return FAMILY_DATA.RELATIONSHIP_TYPE.NONE;
    }

    private void deleteHenchman( GameObject target ){
        _family_manager.removeHenchman(this.gameObject);
        _family_manager.removeHenchman(target);
        Destroy(this.gameObject);
        Destroy(target);
    }
    private void assignHenchman( GameObject target ){
        GameObject target_parent = _family_manager.getParentObject( target );
        _family_manager.assignPearentToHenchman(this.gameObject, target_parent );
        //親を更新する
        _parameter.my_parent = _family_manager.getParent(this.gameObject);
    }
}
