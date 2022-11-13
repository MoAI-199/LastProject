using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using COMMON_DATA;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary> キャラに関する情報群</summary>
public class Parameter {
    public float speed; //定数
    public Vector2 pos; //現在地
    public Vector2 force; //移動量
    public Parent my_parent;
    public bool is_moveing; //移動中か否か
    public string name;//Playerの名前

}
public class CharacterBase : MonoBehaviour {
    protected FamilyManager _family_manager;
    protected Transform _transform;
    protected Rigidbody2D _rigid_body;
    protected SpriteRenderer _sprite_renderer;
    protected Parameter _parameter;
    protected Action<GameObject>[ ] _hit_event;
    private CircleCollider2D _collider;

    private void Awake( ) {
        _family_manager = GameObject.Find( COMMON_DATA.SettingName.FAMILY_MANAGER ).GetComponent<FamilyManager>( );
        _rigid_body = GetComponent<Rigidbody2D>( );
        _sprite_renderer = GetComponentInChildren<SpriteRenderer>( );
        _collider = GetComponent<CircleCollider2D>( );
        _parameter = new Parameter( );
        _transform = this.transform;
        _hit_event = new Action<GameObject>[ ] { hitAllyParent,
                                                 hitAllyHenchman,
                                                 hitEnemyParent,
                                                 hitEnemyHenchman,
                                                 hitWildHenchman };
    }
    private void Start( ) {
        _collider.enabled = false;
        setup( );
    }
    private void Update( ) {
        if( !_collider.enabled ){ 
            //Yが５，Xが９
            float dis = Vector2.Distance( this.gameObject.transform.position, Vector2.zero );
            if( dis < 5  ){
                _collider.enabled = true;
            }
        }
        update( );
        
    }

    protected void deleteEvent( ) {
        StartCoroutine( deleteChara( ) );
    }

    private IEnumerator deleteChara( ) {
        yield return new WaitForSeconds( 0.1f );
        _family_manager.removeParent( this.gameObject );
        Destroy( this.gameObject );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {
        //接触者との関係性を取得し、イベントを呼び出すためのIDとして使用する
        int hit_event_idx = ( int )getRelationshipType( this.gameObject, collision.gameObject );
        _hit_event[ hit_event_idx ].Invoke( collision.gameObject );
    }

    /// <summary>ターゲットの関係性を取得する</summary>
    private COMMON_DATA.RELATIONSHIP_TYPE getRelationshipType( GameObject my_obj, GameObject target_obj ) {
        GameObject my_parent = _family_manager.getParentObject( my_obj );
        GameObject target_parent = _family_manager.getParentObject( target_obj );
        COMMON_DATA.TAG_NAME target_tag_name;
        Enum.TryParse( target_obj.tag, out target_tag_name );//タグを列挙に変換

        if( _family_manager == null ) {
            return COMMON_DATA.RELATIONSHIP_TYPE.NONE;
        }
        // 味方の親である。
        if( target_tag_name == COMMON_DATA.TAG_NAME.PARENT &&
            target_obj == my_parent ) {
            return COMMON_DATA.RELATIONSHIP_TYPE.ALLY_PARENT;
        }
        // 敵の親である。
        if( target_tag_name == COMMON_DATA.TAG_NAME.PARENT &&
            target_parent == null ) {
            return COMMON_DATA.RELATIONSHIP_TYPE.ENEMY_PARENT;
        }
        // 味方の子分である。
        if( target_tag_name == COMMON_DATA.TAG_NAME.HENCHMAN &&
           ( target_parent == my_parent || target_parent == my_obj ) ) {
            return COMMON_DATA.RELATIONSHIP_TYPE.ALLY_HENCHMAN;
        }
        // 味方以外の子分である。
        if( target_tag_name == COMMON_DATA.TAG_NAME.HENCHMAN &&
            target_parent != my_parent ) {
            //野生の子分である
            if( target_parent == null ) {
                return COMMON_DATA.RELATIONSHIP_TYPE.WILD_HENCHMAN;
            }
            //敵の子分である
            return COMMON_DATA.RELATIONSHIP_TYPE.ENEMY_HENCHMAN;
        }
        return COMMON_DATA.RELATIONSHIP_TYPE.NONE;
    }
    protected void assignHenchman( GameObject target ) {
        GameObject target_parent = _family_manager.getParentObject( target );
        if( target_parent != null ) {
            //targetが子分の場合
            _family_manager.assignPearentToHenchman( this.gameObject, target_parent );
            _parameter.my_parent = target_parent.GetComponent<Parent>( );
        } else {
            //targetが親の場合
            _family_manager.assignPearentToHenchman( this.gameObject, target );
            _parameter.my_parent = target.GetComponent<Parent>( );
        }
    }

    public Parameter getParameter( ) {
        return _parameter;
    }
    /// <summary>
    /// ステージに入っているキャラにあたり判定を付与する。
    /// </summary>
    private void inWall( Collider collision ) {
        //Vector2 wall_pos = new Vector2( collision.gameObject.transform.position.x,
        //                                collision.gameObject.transform.position.y );
        //Vector2 chara_pos = new Vector2( this.gameObject.transform.position.x, 
        //                                 this.gameObject.transform.position.y );
        switch( collision.gameObject.name ) {
            case "UpeerWall":
            case "UnderWall":
            case "Leftwall":
            case "RightWall":
                _collider.enabled = true;
            break;
        }
    }


    protected virtual void setup( ) {
    }
    protected virtual void update( ) {
    }
    /// <summary>味方の親と当たった時</summary>
    protected virtual void hitAllyParent( GameObject target ) {
    }
    /// <summary>味方の子分と当たった時</summary>
    protected virtual void hitAllyHenchman( GameObject target ) {
    }
    /// <summary>敵の親と当たった時</summary>
    protected virtual void hitEnemyParent( GameObject target ) {
    }
    /// <summary>敵の子分と当たった時</summary>
    protected virtual void hitEnemyHenchman( GameObject target ) {
    }
    /// <summary>野良の子分と当たった時,これだけ仮想ではないので注意 </summary>
    protected virtual void hitWildHenchman( GameObject target ) {
    }
}
