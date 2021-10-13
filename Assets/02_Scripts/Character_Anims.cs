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
    //
    // Recuerda poner corutinas para que se termine las animaciones entre ataques!
    //
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
        //if (!isPlayerTeam)//flip)
        //{
        //    characterSpriteRenderer.flipX = true;
        //}
        Timing.RunCoroutine(_WaitUntilAnimComplete("Base Layer.TEST_ATTACK", onHit, onAttackComplete));
    }
    public void PlaySpecialAttack(Action onHit, Action onAttackComplete)
    {
        //if (!isPlayerTeam)//flip)
        //{
        //    characterSpriteRenderer.flipX = true;
        //}
        Timing.RunCoroutine(_WaitUntilAnimComplete("Base Layer.TEST_ATTACK", onHit, onAttackComplete));

    }

    IEnumerator<float> _WaitUntilAnimComplete(string Name,Action onHIT, Action onATTACKCOMPLETE)
    {
        anim.Play(Name);
        yield return Timing.WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        onHIT();
        yield return Timing.WaitForOneFrame;
        onATTACKCOMPLETE();
        yield break;
    }

    public void PlayAnimIdle()
    {
        //if (!isPlayerTeam)//flip)
        //{
        //    characterSpriteRenderer.flipX = true;
        //}
        anim.Play("Base Layer.TEST_IDLE");
    }
    public void PlayAnimStarter()
    {
        //if (!isPlayerTeam)//flip)
        //{
        //    characterSpriteRenderer.flipX = true;
        //}
        anim.Play("Base Layer.TEST_START");
    }
}
