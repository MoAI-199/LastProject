using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Parent : CharacterBase {
    private const float SPEED = 0.1f; // à⁄ìÆë¨ìx
    private const float MASS = 100.0f; // éøó 
    private MoveCommonBase _move_compornent;
    private Vector2 _befor_pos;
    private Action[] sFunc; //ÉXÉLÉãÇÃä÷êîÇäiî[Ç∑ÇÈîzóÒ

    protected override void setup( ) {
        _rigid_body.mass = MASS;
        _parameter.speed = SPEED;
        _move_compornent = GetComponent< MoveCommonBase >( );
    }

    protected override void update( ) {
        _parameter.pos = transform.position;
        _parameter.is_moveing = _move_compornent.isMoving( );
        //velocityÇÃåvéZ
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
                hitEventEnemyParent( );
                break;
            case FAMILY_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN:
                hitEventEnemyHenchman( );
                break;
        }
    }

    private void hitEventMyHenchman( ) {
    }

    private void hitEventEnemyParent( ) {
        StartCoroutine( deleteEvent( ) );
    }

    private void hitEventEnemyHenchman( ) {
        _family_manager.removeParent( this.gameObject );
        Destroy( this.gameObject );
    }
    private IEnumerator deleteEvent( ){
        yield return new WaitForSeconds( 0.1f );
        _family_manager.removeParent( this.gameObject );
        Destroy( this.gameObject );
    }

    public Parameter getParemeter(){
        return _parameter;
    }

   
}


