using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return _i;
        }
    }


    [Header("Mini Assets para Debug")]
    public Sprite s_White;
    public Sprite s_Circle;
    public Material m_White;
    //////////////////////////

    [Header("Prefabs")]
    [Space(5)]

    public Transform Map;
    public Transform pfCharacterBattle;
    public Transform pfNPCOvermap;
    public Transform pfEnemyOvermap;
    public Transform pfFollowerOvermap;
    public Transform damagePopup;
    public Transform dmgDebuff, blindDebuff, healthDebuff;
    public Transform pfMedicinalHerbs;
    public Transform pfWeaponTEST;
    public Transform pfChatBubbleUI;
    public Transform pfChatOption;
    public RectTransform pf_ItemSlot;
    //public Transform pfPopUpWindow;
    //public Transform pfPopUpWindowButton;

    [Header("Animators")]
    [Space(5)]

    public RuntimeAnimatorController suyaiBATTLEANIM;
    public RuntimeAnimatorController pedroBATTLEANIM, aranaBATTLEANIM, antayBATTLEANIM,chillpilaBATTLEANIM,suyaiOVERWORLDANIM, pedroOVERWORLDANIM, aranaOVERWORLDANIM, antayOVERWORLDANIM, chillpilaOVERWORLDANIM;
    public RuntimeAnimatorController testEnemyANIM,fusileroANIM,lanceroANIM;

    [Header("Sprites")]
    [Space(5)]

    public Sprite testEnemySprite;
    public Sprite fusileroOWSprite,lanceroOWSprite;
    public Sprite questNpc_1, warriorNPC,npc_SHOP;
    public Sprite spriteOWPedro, spriteOWArana, spriteOWChillpila, spriteOWAntay;
    public Sprite splashSuyai, splashPedro, splashChillpila, splashArana, splashAntay;
    public Sprite playerDialogueSprite, npc_1DialogueSprite, warriorNpcDialogueSprite;
    public Sprite item_Herb,item_Weapon1,item_Helmet1,item_Armor1;
    public List<Sprite> enemies = new List<Sprite>();

    public Quest[] questArray;

    //public AudioMixerGroup audioMixer;

    public SoundAudioClip[] audioClipsArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
        [Range(0f, 2f)]
        public float volumen;
    }

}
