using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Transform pfCoin;
    public Transform pfChatBubbleUI;
    public Transform pfChatOption;

    [Header("Animators")]
    [Space(5)]

    public RuntimeAnimatorController suyaiANIM;
    public RuntimeAnimatorController pedroANIM, aranaANIM, antayANIM,chillpilaANIM;
    public RuntimeAnimatorController enemyANIM;

    [Header("Sprites")]
    [Space(5)]

    public Sprite spriteEnemy;
    public Sprite questNpc_1, warriorNPC;
    public Sprite spriteOWPedro, spriteOWArana, spriteOWChillpila, spriteOWAntay;
    public Sprite splashSuyai, splashPedro, splashChillpila, splashArana, splashAntay;
    public Sprite playerDialogueSprite, npc_1DialogueSprite, warriorNpcDialogueSprite;
    public List<Sprite> enemies = new List<Sprite>();

    public Quest[] questArray;

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
