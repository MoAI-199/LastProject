using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COMMON_DATA{

    public class SettingName{
        public const string FAMILY_MANAGER = "FamilyManager";
        public const string PREFAB_PARENT_PATH = "Prefab/Parent";
        public const string PREFAB_HENCHMAN_PATH = "Prefab/Henchman";
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
        GAME_SETTING,
        GAME_PLAYING,
        GAME_END,
        RESULT,
    }
    public enum GAME_MODE {
        PVP,
        CHELLENGE,
        NONE,
    }
    //•À‚Ñ‘Ö‚¦Œµ‹Ö
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

