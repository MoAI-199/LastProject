using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Parent : CharacterBase {
	private const float SPEED = 0.1f; // ˆÚ“®‘¬“x
	private const float MASS = 100.0f; // Ž¿—Ê
	private MoveCommonBase _move_compornent;
	private Vector2 _befor_pos;


	protected override void setup( ) {
		_rigid_body.mass = MASS;
		_parameter.speed = SPEED;
		_move_compornent = GetComponent<MoveCommonBase>( );
	}

	protected override void update( ) {
		_parameter.pos = transform.position;
		_parameter.is_moveing = _move_compornent.isMoving( );
		//velocity‚ÌŒvŽZ
		if( Vector2.Distance( _parameter.pos, _befor_pos ) > 0.01f ) {
			_parameter.velocity = ( _parameter.pos - _befor_pos ) * 50;
		}
		_befor_pos = transform.position;
	}

	protected override void hitAllyParent( GameObject target ) {
		base.deleteEvent( );
	}
	protected override void hitAllyHenchman( GameObject target ) {
	}
	protected override void hitEnemyParent( GameObject target ) {
	}
	protected override void hitEnemyHenchman( GameObject target ) {
		// Debug.Log( "EnemyHenchman" );
		base.deleteEvent( );
	}
	protected override void hitWildHenchman( GameObject target ) {
		base.assignHenchman( target );
	}
	public void chaneParemeterName(string pleyer_name)
	{
		_parameter.name = pleyer_name;
	}

	public Parameter getParemeter( ) {
		return _parameter;
	}


}


