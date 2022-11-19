using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;

public class Parent : CharacterBase {
	[SerializeField]private float SPEED = 0.1f; // 移動速度
	[SerializeField]private float MASS = 100.0f; // 質量
	private MoveCommonBase _move_compornent;
	private Vector2 _befor_pos;
	private TextMeshProUGUI _NamePlate;
	private int _kill_num;
	private GameObject _kill_parent;
	private int _added_henchman_count;
	protected override void setup( ) {
		_rigid_body.mass = MASS;
		_parameter.speed = SPEED;
		_move_compornent = GetComponent<MoveCommonBase>( );
		_parameter.pos = transform.position;
		_befor_pos = _parameter.pos;
		GameObject obj = transform.Find("Canvas/NamePlate").gameObject;
		_NamePlate = obj.GetComponent<TextMeshProUGUI>();
		_NamePlate.text = getParemeter( ).name != "Enemy" ? getParemeter( ).name : "";
		_kill_num = 0;
		_added_henchman_count = 0;
	}

	protected override void update( ) {
		_parameter.pos = transform.position;
		_parameter.is_moveing = _move_compornent.isMoving( );
		//forceの計算
		if( Vector2.Distance( _parameter.pos, _befor_pos ) > 0.01f ) {
			_parameter.force = ( _parameter.pos - _befor_pos ) * 20;
		}
		_befor_pos = transform.position;

		if( GameManager.instatnce.getGameState( ) == COMMON_DATA.GAME_STATE_TYPE.RESULT ) {
			GameManager.instatnce._result_save_data._kill_num = _kill_num;
			GameManager.instatnce._result_save_data._added_henchman_count = _added_henchman_count;
		}
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
	
	/// <summary>基本子分で呼ぶ。kill数を記録</summary>
	public void addKillCount( GameObject enemy_parent ){
		if( _kill_parent == enemy_parent ){
			return; //同じ親はカウントしない
		}
		_kill_parent = enemy_parent;
		_kill_num++;
	}

	public void addHenchmanCount() {
		_added_henchman_count++;
		Debug.Log( $"add:{_added_henchman_count}" );
	}

}


