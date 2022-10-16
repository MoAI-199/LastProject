using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Parent : CharacterBase {
   

    private const float SPEED = 0.1f; // ˆÚ“®‘¬“x
    private const float MASS = 100.0f; // Ž¿—Ê
    MoveCommonBase _move_compornent;
    Vector2 _befor_pos;
    
    protected override void setup( ) {
        _rigid_body.mass = MASS;
        _parameter.speed = SPEED;
        _move_compornent = GetComponent< MoveCommonBase >( );
    }

    protected override void update( ) {
        _parameter.pos = transform.position;
        _parameter.is_moveing = _move_compornent.isMoving( );
        //velocity‚ÌŒvŽZ
        Debug.Log( "velo:"+ _parameter.velocity );
        if( Vector2.Distance( _parameter.pos, _befor_pos ) > 0.01f ) {
            _parameter.velocity = ( _parameter.pos - _befor_pos ) * 50;
        }
        _befor_pos = transform.position;
    }
    private void OnCollisionEnter2D( Collision2D collision ) {
        FAMILY_DATA.RELATIONSHIP_TYPE type = examineRelationshipType( this.gameObject, collision.gameObject );
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


    public Parameter getParemeter(){
        return _parameter;
    }
}


