using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void PlayCoinSound()
    {
        switch (Battle.GetInstance().GetRandomPedroNumber())
        {
            case 0:
                SoundManager.PlaySound(SoundManager.Sound.Error);
                break;
            case 1:
                SoundManager.PlaySound(SoundManager.Sound.Coin);
                break;
        }
    }
}
