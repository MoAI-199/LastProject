using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;



public class Henchman : CharacterBase {
	private const float SPEED = 100.0f; //数値をあげると最大速度が下がる
	private const float MASS = 1.0f; // 質量
	private const float RESET_TIME = 1.5f; //親停止時に親の前まで動く時間

	private bool _is_move = true;
	private float _stop_time = 0.0f;

	protected override void setup( ) {
		Application.targetFrameRate = 30;
		_rigid_body.mass = MASS;
		_parameter.my_parent = _family_manager.getParent( this.gameObject );
		_sprite_renderer.color = new Color( 1, Random.Range( 0.0f, 1.0f ), Random.Range( 0.0f, 1.0f ) );
	}

	protected override void update( ) {
		move( );
	}
	private void move( ) {
		if( _transform == null || _parameter.my_parent == null ) {
			return;
		}
		//移動先のきりかえ
		bool convart_target = true;
		if( !_parameter.my_parent.getParemeter( ).is_moveing ) {
			convart_target = false;
			_stop_time += Time.deltaTime;
			if( _stop_time > RESET_TIME ) {
				convart_target = true;
			}
		} else {
			_stop_time = 0.0f;
		}

		Vector2 target_pos;
		if( convart_target ) {
			//親に向けて移動する
			target_pos = _parameter.my_parent.getParemeter( ).pos;
		} else {
			//親の少し前に移動する
			var my_parent_parameter = _parameter.my_parent.getParemeter( );
			target_pos = my_parent_parameter.pos + my_parent_parameter.velocity;
		}
		//目的地と自分の距離
		float distance = Vector2.Distance( _transform.position, target_pos );
		//移動処理
		if( _is_move ) {
			_transform.position = Vector2.Lerp( _transform.position, target_pos, distance / SPEED );
		}
		//一定の距離離れたら動き出す
		if( distance > 1.0f ) {
			_is_move = true;
		}
	}
	/// <summary>動きを止める</summary>
	protected override void hitAllyParent( GameObject target ) {
		_is_move = false;
	}
	protected override void hitAllyHenchman( GameObject target ) {
	}
	protected override void hitEnemyParent( GameObject target ) {
		//自分が野良のときの処理
		if( _parameter.my_parent == null ) {
			base.assignHenchman(target);
		}
	}
	protected override void hitEnemyHenchman( GameObject target ) {
		//自分が野良の処理
		if( _parameter.my_parent == null ) {
			base.assignHenchman(target);
		}else{
			base.deleteEvent();
		}
	}
	protected  override void hitWildHenchman( GameObject target ) {
	}
}
