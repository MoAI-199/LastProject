using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ResultChallenge : MonoBehaviour {
    private enum EVALUATION {
        A,
        B,
        C
    }

    GameObject _kill_number;
    GameObject _added_number;
    GameObject _evaluation_a;
    GameObject _evaluation_b;
    GameObject _evaluation_c;

    public float base_kill = 50.0f;
    public float base_added = 150.0f;

    private void Awake( ) {
        _kill_number = this.transform.Find( "KillCount" ).gameObject;
        _added_number = this.transform.Find( "AddedCount" ).gameObject;
        _evaluation_a = this.transform.Find( "Evaluation/Text_A" ).gameObject;
        _evaluation_b = this.transform.Find( "Evaluation/Text_B" ).gameObject;
        _evaluation_c = this.transform.Find( "Evaluation/Text_C" ).gameObject;
        _evaluation_a.SetActive( false );
        _evaluation_b.SetActive( false );
        _evaluation_c.SetActive( false );

        base_kill = 50.0f;
        base_added = 150.0f;


    }
    private void Start( ) {
        //仲間にしたナイトの数
        var added_text = _added_number.GetComponentInChildren<TextMeshProUGUI>( );
        int added_num = GameManager.instatnce._result_save_data._added_count;
        showNumber( added_num, added_text );
        //倒したキングの数
        var kill_text = _kill_number.GetComponentInChildren<TextMeshProUGUI>( );
        int kill_num = GameManager.instatnce._result_save_data._kill_num;
        showNumber( kill_num, kill_text );
        //総評
        float kill_point = ((float)kill_num / base_kill) * 100.0f;
        float added_point = ((float )added_num / base_added )* 100.0f;
        if( kill_point >= 100 && added_point >= 100 ){
            _evaluation_a.SetActive( true );
        } else if( kill_point >= 100 || added_point >= 100 ) {
            _evaluation_b.SetActive( true );
        }else{
            _evaluation_c.SetActive( true );
        }
    }

    private void Update( ) {

    }

    private void showNumber( int num, TextMeshProUGUI text ) {
        num = num >= 999 ? 999 : num;
        int[ ] digit_num = new int[ 3 ];
        int idx = 0;
        while( idx < 3 ) {
            Debug.Log( num );
            digit_num[ idx ] = ( num % 10 );
            num /= 10;
            idx++;
        }
        string input_text = $"<sprite={digit_num[ 2 ]}><sprite={digit_num[ 1 ]}><sprite={digit_num[ 0 ]}>";
        text.text = input_text;
    }
}
