using System.Collections;
using System.Collections.Generic;
using COMMON_DATA;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImputName : MonoBehaviour {
    private TMP_InputField _input_field;

    [SerializeField] private Image gyaaKun;
    [SerializeField] private Image frame;

    private int _input_user_id;

    private Sprite[ ] user_num = new Sprite[ 3 ];
    private Sprite[ ] gyaa_color = new Sprite[ 2 ];

    private void Awake( ) {
        _input_field = GetComponentInChildren<TMP_InputField>( );

        user_num[ 0 ] = Resources.Load<Sprite>( "img/InputName/title_name_challenge_1p_sotowaku" );
        user_num[ 1 ] = Resources.Load<Sprite>( "img/InputName/title_name_pvp_1p_sotowaku" );
        user_num[ 2 ] = Resources.Load<Sprite>( "img/InputName/title_name_pvp_2p_sotowaku" );
        gyaa_color[ 0 ] = Resources.Load<Sprite>( "img/InputName/title_name_1p_icon" );
        gyaa_color[ 1 ] = Resources.Load<Sprite>( "img/InputName/title_name_2p_icon" );
    }

    private void Start( ) {
        _input_user_id = 0;

        if( GameManager.instatnce.getGameMode( ) == GAME_MODE.CHELLENGE ) {
            frame.sprite = user_num[ 0 ];//チャレンジモード用のフレームを入れる
        } else {
            frame.sprite = user_num[ 1 ];//1pの名前入力用を表示
        }

        gyaaKun.sprite = gyaa_color[ 0 ];//赤いギャー君のアイコンを入れる
    }

    private void Update( ) {
    }

    /// <summary>ボタンが推された時に呼ばれる関数</summary>
    public void onClick( ) {
        if( string.IsNullOrEmpty( _input_field.text ) ){
            return;
        }
            updateUserName( );
        switch( GameManager.instatnce.getGameMode( ) ) {
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                nextGame( );
                return;
            case COMMON_DATA.GAME_MODE.PVP:
                _input_field.text = "";
                _input_user_id++;
                frame.sprite = user_num[ 2 ];//2Pの名前入力用を表示
                gyaaKun.sprite = gyaa_color[ 1 ];//青いギャー君を表示
                if( _input_user_id >= 2 ) {
                    nextGame( );
                }
                break;
        }
        void nextGame( ) {
            GameManager.instatnce.doneGameStatus( );
            Destroy( this.gameObject );
        }
    }

    /// <summary>現在InputAreaに記載されている物を追加</summary>
    private void updateUserName( ) {
        GameManager.instatnce.getUserData( ).setUserName( _input_user_id, _input_field.text );
    }
}
