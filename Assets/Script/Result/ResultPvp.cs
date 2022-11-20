using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ResultPvp : MonoBehaviour {

    GameObject _winner_name;
    GameObject _player1_name;
    GameObject _player2_name;
    GameObject _judge_icon_win;
    GameObject _judge_icon_drow;

    UserData _user_data;

    private void Awake( ) {
        _winner_name = this.transform.Find( "WinnerNamePlate/Name" ).gameObject;
        _player1_name = this.transform.Find( "Player1Frame/NameFrame/Name" ).gameObject;
        _player2_name = this.transform.Find( "Player2Frame/NameFrame/Name" ).gameObject;
        _judge_icon_win = this.transform.Find( "Judge/Win" ).gameObject;
        _judge_icon_drow = this.transform.Find( "Judge/Drow" ).gameObject;

        _judge_icon_win.SetActive( false );
        _judge_icon_drow.SetActive( false );

        _user_data = GameManager.instatnce.getUserData( );
    }

    void Start( ) {
        //勝敗アイコンの表示
        if( string.IsNullOrEmpty( _user_data.getWinnerName( ) ) ) {
            //勝者の名前がない場合引き分け（Drow）
            _judge_icon_drow.SetActive( true );
        } else {
            _judge_icon_win.SetActive( true );
            TextMeshProUGUI winner_text = _winner_name.GetComponent<TextMeshProUGUI>( );
            winner_text.text = _user_data.getWinnerName( );
        }
        //ユーザー名の表示
        TextMeshProUGUI player1_text = _player1_name.GetComponent<TextMeshProUGUI>( );
        player1_text.text = _user_data.getUserName( 0 );
        TextMeshProUGUI player2_text = _player2_name.GetComponent<TextMeshProUGUI>( );
        player2_text.text = _user_data.getUserName( 1 );
    }

    void Update( ) {

    }
}
