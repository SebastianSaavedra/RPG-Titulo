using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCharacter : MonoBehaviour
{
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
                        break;
                }
                break;
            case Character.Type.Pedro:
                switch (BattleUI.instance.command)
                {
                    case "Attack":
                        SoundManager.PlaySound(SoundManager.Sound.SlashAtk);
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
                        break;
                }
                break;
            //case Character.Type.Anchimallen:
            //    switch (Battle.GetInstance().GetEnemyCommand())
            //    {
            //        case "Attack":
            //            SoundManager.PlaySound(SoundManager.Sound.MeleeAtk1);
            //            break;

            //        case "Special":
            //            SoundManager.PlaySound(SoundManager.Sound.Thunder);
            //            break;
            //    }
            //    break;
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
        }
    }
}
