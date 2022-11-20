using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COMMON_DATA{
    public class Prefab{
        //その他
        public const string GAMEMANAGERS = "Prefab/GameManagers";
        //characterフォルダ
        public const string PARENT_PINK = "Prefab/Character/Parent";
        public const string HENCHMAN_PINK = "Prefab/Character/Henchman";
        public const string PARENT_BLUE = "Prefab/Character/ParentBlue";
        public const string HENCHMAN_BLUE = "Prefab/Character/HenchmanBlue";
        //Canvasフォルダ
        public const string CANVAS_INPUT_NAME = "Prefab/Canvas/InputName";
        public const string CANVAS_TIMER = "Prefab/Canvas/TimerCanvas";
        public const string CANVAS_RESULT_BASE = "Prefab/Canvas/ResultBase";
        public const string CANVAS_RESULT_CHALLENGE = "Prefab/Canvas/ResultChallenge";
        public const string CANVAS_RESULT_PVP = "Prefab/Canvas/ResultPvp";
        //stageフォルダ
        public const string STAGE_PVP = "Prefab/Stage/pvp";
        public const string STAGE_CHALLENGE = "Prefab/Stage/challenge";
    }

    public class CharaSprite{
        public const string COLOR_PINK_PARENT = "Animation/game_character_pink";
        public const string COLOR_PINK_HENCHMAN = "Animation/game_character_pink_child";
        public const string COLOR_BLUE_PARENT = "Animation/game_character_blue";
        public const string COLOR_BLUE_HENCHMAN = "Animation/game_character_blue_child";
    }

    public class SettingName{
        public const string FAMILY_MANAGER = "FamilyManager";
       
        public const float TIMER = 180.0f;
        
        public const string PVP_MANUAL = "img/Title/Operation/main_pvp_waku.png";
        public const string CHALLENGE_MANUAL = "img/Title/Operation/main_challeng_waku.png";
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

    /// <summary>
    /// 並び替え厳禁
    /// </summary>
    public enum TEXTURE_COLOR {
        PINK,
        BLUE,
        MAX,
    }
}

