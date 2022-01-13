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
    public Transform pfTrenTren;
    public Transform pfCaiCai;
    public Transform pfPelotonLanceros,pfPelotonFusileros,pfPelotoFusileroYLancero;
    public Transform pfMineralBronce, pfMineralCobre, pfMineralPlata, pfMineralOro;
    public Transform anchimallenFire;
    public RectTransform pf_ItemSlot;
    //public Transform pfPopUpWindow;
    //public Transform pfPopUpWindowButton;

    [Header("Animators")]
    [Space(5)]

    public RuntimeAnimatorController suyaiBATTLEANIM;
    public RuntimeAnimatorController pedroBATTLEANIM, aranaBATTLEANIM, antayBATTLEANIM,chillpilaBATTLEANIM,suyaiOVERWORLDANIM, pedroOVERWORLDANIM, aranaOVERWORLDANIM, antayOVERWORLDANIM, chillpilaOVERWORLDANIM;
    public RuntimeAnimatorController testEnemyANIM,fusileroANIM,lanceroANIM,anchimallenBattleANIM, anchimallenOverworldANIM, guiriviloANIM,piuchenANIM,trentrenBattleAnim,caicaiBattleAnim;


    public Sprite testEnemySprite;
    public Sprite fusileroOWSprite, lanceroOWSprite, anchimallenOWSprite, guiriviloOWSprite, piuchenOWSprite, trenTrenSprite, caiCaiSprite;

    public Sprite questNpc_1, warriorNPC,npc_SHOP,npc_ViejaMachi,npc_HombreMapuche,npc_MujerMapuche,npc_NinoMapuche,npc_NinaMapuche;
    public Sprite spriteOWPedro, spriteOWArana, spriteOWChillpila, spriteOWAntay;

    public Sprite splashSuyai, splashPedro, splashChillpila, splashArana, splashAntay;
    public Sprite playerDialogueSprite, npc_1DialogueSprite, warriorNpcDialogueSprite;

    public Sprite item_Herb,item_Weapon1,item_Helmet1,item_Armor1,item_EscamaMarina;

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
