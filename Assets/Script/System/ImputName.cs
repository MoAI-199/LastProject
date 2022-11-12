using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImputName : MonoBehaviour {
    private TMP_InputField _input_field;
    private TMP_Text _user_namber_text;

    private string[ ] _user_input_name;
    private int _input_user_id;
    private COMMON_DATA.GAME_STATE_TYPE _game_state_type = COMMON_DATA.GAME_STATE_TYPE.NONE;

    private void Awake( ) {
        _input_field = GetComponentInChildren<TMP_InputField>( );
        _user_namber_text = GetComponentInChildren<TMP_Text>( );
    }

    private void Start( ) {
        _user_namber_text.text = "Player1��\n�Ȃ܂������߂܂�";
        _user_input_name = new string[ ] { "player1", "player2" }; //default�̖��O�ݒ�
        _input_user_id = 0;
    }

    private void Update( ) {

    }

    /// <summary>�{�^���������ꂽ���ɌĂ΂��֐�</summary>
    public void onClick( ) {
        updateUserName( );
        switch( GameManager.instatnce.getGameMode( ) ) {
            case COMMON_DATA.GAME_MODE.CHELLENGE:
                nextGame( );
                return;
            case COMMON_DATA.GAME_MODE.PVP:
                _input_field.text = "";
                _input_user_id++;
                _user_namber_text.text = "Player2��\n�Ȃ܂������߂܂�";
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

    /// <summary>����InputArea�ɋL�ڂ���Ă��镨��ǉ�</summary>
    private void updateUserName( ) {
        Debug.Log( _input_field.text );
        GameManager.instatnce.getUserData( ).setUserName( _input_user_id, _input_field.text );
    }
}
