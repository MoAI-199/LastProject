using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COMMON_DATA{

    public class SettingName{
        public const string FAMILY_MANAGER = "FamilyManager";
        public const string PREFAB_PARENT_PATH = "Prefab/Parent";
        public const string PREFAB_HENCHMAN_PATH = "Prefab/Henchman";
        public const string PREFAB_PATH_CANVAS_INPUT_NAME = "Prefab/Canvas/InputName";
        public const string PREFAB_PATH_CANVAS_RESULT = "Prefab/Canvas/Result";
        public const float TIMER = 180.0f;
        
        public const string PVP_MANUAL = "/img/PVPManual";
        public const string CHALLENGE_MANUAL = "/img/challengeManual";
    }
    public enum SCENE_TYPE{
        TITLE,
        GAME,
    }

    public enum TITLE_TYPE{
        /// <summary>タイトルの表示</summary>
        NORMAL,
        /// <summary>選択画面</summary>
        MODE_SELECT,
        /// <summary>遊び方画面</summary>
        MANUAL,
    }
   
    public enum GAME_STATE_TYPE{
        /// <summary>操作説明を表示する</summary>
        GUIDE,
        /// <summary>名前の入力</summary>
        INPUT_NAME,
        /// <summary>メインゲーム</summary>
        GAME_PLAYING, //
        /// <summary>リザルト画面</summary>
        RESULT,
        NONE
    }
    public enum GAME_MODE {
        PVP,
        CHELLENGE,
        NONE,
    }
    //並び替え禁止
    public enum RELATIONSHIP_TYPE{
        ALLY_PARENT,
        ALLY_HENCHMAN,
        ENEMY_PARENT,
        ENEMY_HENCHMAN,
        WILD_HENCHMAN,
        NONE,
    }
    public enum TAG_NAME{
        PARENT,
        HENCHMAN,
    }
}

