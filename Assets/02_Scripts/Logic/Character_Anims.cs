using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using CodeMonkey;
using CodeMonkey.Utils;
using UnityEngine;

[Serializable]
public class Animations
{

    public WeaponAnims[] weaponAnimsArray;

    [Serializable]
    public struct WeaponAnims
    {
        public string weaponName;
        public List<AnimationClip> anims;
    }
}

public class Character_Anims : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer characterSpriteRenderer;
    [HideInInspector] public Animator anim;
    //public Animations anims;

    //IMPORTANTE    //IMPORTANTE    //IMPORTANTE
    //                                                                                  //Listo
    // Recuerda poner corutinas para que se termine las animaciones entre ataques!      //Listo
    //                                                                                  //Listo
    //IMPORTANTE    //IMPORTANTE    //IMPORTANTE


    private void Awake()
    {
        anim = GetComponent<Animator>();
        characterSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

    }

    public void PlayAnimAttack(Action onHit, Action onAttackComplete)
    {
        Timing.RunCoroutine(_WaitUntilAnimComplete("Base Layer.ATTACK", onHit, onAttackComplete));
    }
    public void PlaySpecialAttack(Action onHit, Action onAttackComplete)
    {
        Timing.RunCoroutine(_WaitUntilAnimComplete("Base Layer.SPECIAL", onHit, onAttackComplete));

    }

    IEnumerator<float> _WaitUntilAnimComplete(string name,Action onHIT, Action onATTACKCOMPLETE)
    {
        anim.Play(name);
        yield return Timing.WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        onHIT();
        yield return Timing.WaitForOneFrame;
        onATTACKCOMPLETE();
        yield break;
    }

    public void PlayAnimIdle()
    {
        anim.Play("Base Layer.IDLE");
    }
    public void PlayAnimStarter()
    {
        anim.Play("Base Layer.START");
    }
}
