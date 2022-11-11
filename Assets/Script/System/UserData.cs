using System.Collections;
using System.Collections.Generic;
using COMMON_DATA;
using UnityEngine;

public class UserData {
    private string[ ] _user_name = new string[ 2 ];
    private string _winner_name = "";
    public void setUserName( int idx, string name ) {
        _user_name[ idx ] = name;
    }
    public string getUserName( int idx ) {
        return _user_name[ idx ];
    }

    public void setWinnerName( string name ) {
        _winner_name = name;
    }
    public string getWinnerName( ){
        return string.IsNullOrEmpty( _winner_name ) ? null : _winner_name; //勝者がいない場合、NULLを返す
    }
}