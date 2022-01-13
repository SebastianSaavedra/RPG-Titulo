using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCharacter : MonoBehaviour
{
    [SerializeField] private AbilityAnimData abilityAnimationContainer;

    private void Awake()
    {
        abilityAnimationContainer = GameObject.Find("HabilidadAnimationContainer").GetComponent<AbilityAnimData>();
    }

    public void PlaySoundEffect()
    {
        switch (Battle.GetInstance().activeCharacterBattle.GetCharacterType())
        {
            case Character.Type.Suyai:
                switch (BattleUI.instance.command)
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.Kultrun);
                        break;

                    case "Special":
                        Debug.Log("Suyai special");
                        SoundManager.PlaySound(SoundManager.Sound.Heal);
                        break;
                }
                break;
            case Character.Type.Chillpila:
                switch (BattleUI.instance.command)
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.SlashAtk);
                        break;

                    case "Special":
                        SoundManager.PlaySound(SoundManager.Sound.Thunder);

                        List<CharacterBattle> characterBattleList = Battle.GetInstance().GetAliveTeamCharacterBattleList(false);

                        for (int i = 0; i < characterBattleList.Count; i++)
                        {
                            abilityAnimationContainer.GetRayosList()[i].SetActive(true);
                            abilityAnimationContainer.GetRayosList()[i].transform.position = characterBattleList[i].GetComponent<Transform>().position;
                            abilityAnimationContainer.GetRayosList()[i].transform.localScale = new Vector3(1.5f,1.5f,1f);
                        }
                        break;
                }
                break;
            case Character.Type.Pedro:
                switch (BattleUI.instance.command)
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.SlashAtk);
                        break;

                    case "Special":
                        switch (Battle.GetInstance().GetRandomPedroNumber())
                        {
                            case 0:
                                abilityAnimationContainer.GetMonedaCruz().SetActive(true);
                                abilityAnimationContainer.GetMonedaCruz().transform.position = new Vector3(9f,6f,0f);
                                break;
                            case 1:
                                abilityAnimationContainer.GetMonedaCara().SetActive(true);
                                abilityAnimationContainer.GetMonedaCara().transform.position = new Vector3(9f, 6f, 0f);                                
                                break;
                        }
                        break;
                }
                break;
            case Character.Type.Arana:
                switch (BattleUI.instance.command)
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.MeleeAtk2);
                        break;

                    case "Special":
                        SoundManager.PlaySound(SoundManager.Sound.MeleeAtk2);
                        break;
                }
                break;
            case Character.Type.Antay:
                switch (BattleUI.instance.command)
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.HeavyAtk);
                        break;

                    case "Special":
                        SoundManager.PlaySound(SoundManager.Sound.HeavyAtk);

                        switch (Battle.GetInstance().GetRandomNumber())
                        {
                            case 0:
                                abilityAnimationContainer.GetMineralBronce().SetActive(true);
                                break;
                            case 1:
                                abilityAnimationContainer.GetMineralCobre().SetActive(true);
                                break;
                            case 2:
                                abilityAnimationContainer.GetMineralPlata().SetActive(true);
                                break;
                            case 3:
                                abilityAnimationContainer.GetMineralOro().SetActive(true);
                                break;
                        }
                        break;
                }
                break;
            case Character.Type.Anchimallen:
                switch (Battle.GetInstance().GetEnemyCommand())
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.SlapAtk);
                        break;
                }
                break;
            case Character.Type.Piuchen:
                switch (Battle.GetInstance().GetEnemyCommand())
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.SlapAtk);
                        break;

                    case "Special":
                        SoundManager.PlaySound(SoundManager.Sound.SlapAtk);
                        break;
                }
                break;
            case Character.Type.Guirivilo:
                switch (Battle.GetInstance().GetEnemyCommand())
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.SlapAtk);
                        break;

                    case "Special":
                        SoundManager.PlaySound(SoundManager.Sound.MeleeAtk2);
                        break;
                }
                break;
            case Character.Type.Fusilero:
                switch (Battle.GetInstance().GetEnemyCommand())
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.GunAtk);
                        break;

                    case "Special":
                        SoundManager.PlaySound(SoundManager.Sound.GunAtk);
                        break;
                }
                break;
            case Character.Type.Lancero:
                switch (Battle.GetInstance().GetEnemyCommand())
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.SlashAtk);
                        break;

                    case "Special":
                        SoundManager.PlaySound(SoundManager.Sound.SlashAtk);
                        break;
                }
                break;
            case Character.Type.CaiCai:
                switch (Battle.GetInstance().GetEnemyCommand())
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.MeleeAtk1);
                        break;

                    case "Special":
                        SoundManager.PlaySound(SoundManager.Sound.TsunamiAtk);
                        break;
                }
                break;
        }
    }
}
