using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class Parent : CharacterBase {
	private const float SPEED = 0.1f; // ˆÚ“®‘¬“x
	private const float MASS = 100.0f; // Ž¿—Ê
	private MoveCommonBase _move_compornent;
	private Vector2 _befor_pos;
	private string NAME;

	protected override void setup( ) {
		_rigid_body.mass = MASS;
		_parameter.speed = SPEED;
		_move_compornent = GetComponent<MoveCommonBase>( );
		_parameter.pos = transform.position;
		_befor_pos = _parameter.pos;
		GameObject obj = transform.Find("Canvas/NamePlate").gameObject;
		TextMeshProUGUI NamePlate = obj.GetComponent<TextMeshProUGUI>();
		NamePlate.text = getParemeter().name;

		if (getParemeter().name == "Enemy")
		{
			foreach ( Transform n in transform )
			{
				Destroy(n.gameObject);
			}
		}
	}

	protected override void update( ) {
		_parameter.pos = transform.position;
		_parameter.is_moveing = _move_compornent.isMoving( );
		//force‚ÌŒvŽZ
		if( Vector2.Distance( _parameter.pos, _befor_pos ) > 0.01f ) {
			_parameter.force = ( _parameter.pos - _befor_pos ) * 20;
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
	}
	public void chaneParemeterName(string pleyer_name)
	{
		_parameter.name = pleyer_name;
	}

	public Parameter getParemeter( ) {
		return _parameter;
	}

}


