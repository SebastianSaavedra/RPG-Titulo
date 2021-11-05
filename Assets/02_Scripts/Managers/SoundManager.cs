using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public static class SoundManager
{

    public enum Sound
    {
        Attack,
        Defense,
        CharacterHit,
        CharacterDamaged,
        CharacterDead,
        AllyDead,
        Coin,
        SpecialAbility,
        BattleTransition,
        BattleWin,
        Heal,
        Talking,
        Dash,
        ButtonPress,
        ButtonOver,
        PlayerMove,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    public static int masterVolume;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        masterVolume = 5;
        soundTimerDictionary[Sound.PlayerMove] = 0f;
    }

    public static void PlaySound(Sound sound, float destroyTime)
    {
        PlaySound(sound, Camera.main.transform.position, destroyTime);
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        PlaySound(sound, position, GetAudioClip(sound).length);
    }

    public static void PlaySound(Sound sound, Vector3 position, float destroyTime)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.volume = (masterVolume / 10f) * GetSoundVolume(sound);
            audioSource.Play();

            Object.Destroy(soundGameObject, destroyTime);
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.volume = (masterVolume / 10f) * GetSoundVolume(sound);
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .15f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                //break;
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        return GetSoundAudioClip(sound).audioClip;
    }

    private static float GetSoundVolume(Sound sound)
    {
        return GetSoundAudioClip(sound).volumen;
    }

    private static GameAssets.SoundAudioClip GetSoundAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.audioClipsArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip;
            }
        }
        Debug.LogError("Sonido " + sound + " no encontrado!");
        return null;
    }

    public static void AddButtonSounds(this Button_UI buttonUI)
    {
        buttonUI.ClickFunc += () => SoundManager.PlaySound(Sound.ButtonPress);
        buttonUI.MouseOverOnceFunc += () => SoundManager.PlaySound(Sound.ButtonOver);
    }
}
