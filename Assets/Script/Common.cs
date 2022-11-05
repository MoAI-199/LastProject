using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COMMON_DATA{

    public class SettingName{
        public const string FAMILY_MANAGER = "FamilyManager";
        public const string PREFAB_PARENT_PATH = "Prefab/Parent";
        public const string PREFAB_HENCHMAN_PATH = "Prefab/Henchman";
        public const float TIMER = 180.0f;
        
        public const string PVP_MANUAL = "/img/PVPManual";
        public const string CHALLENGE_MANUAL = "/img/challengeManual";
    }
    public enum SCENE_TYPE{
        TITLE,
        GAME,
    }

    public enum TITLE_TYPE{
        NORMAL,
        MODE_SELECT,
        MANUAL,
    }
   
    public enum GAME_STATE_TYPE{
        GAME_SETTING, //
        GAME_READY, //
        GAME_PLAYING, //
        GAME_END, //
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

