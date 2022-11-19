using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DrawNumber : MonoBehaviour {
    [SerializeField] private int num = 123;

    private int[ ] digit_num = new int[ 3 ]{ 0,0,0};
    private string input_text = "";
    void Start( ) {
        int number = num >= 999 ? 999 : num;
        int idx = 0;
        while( idx < 3 ) {
            Debug.Log( number );

            digit_num[ idx ] = ( number % 10 );
            number /= 10;
            idx++;
        }
        input_text = $"<sprite={digit_num[ 2 ]}><sprite={digit_num[ 1 ]}><sprite={digit_num[ 0 ]}>";
        TextMeshProUGUI text = this.GetComponent<TextMeshProUGUI>( );
        text.text = input_text;
    }

}
