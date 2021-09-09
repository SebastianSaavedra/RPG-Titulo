using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using CodeMonkey;
using CodeMonkey.Utils;
using UnityEngine;

public class Character_Anims : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer characterSpriteRenderer;
    [HideInInspector] public Animator anim;
    //public bool isPlayerTeam;
    StateMachineBehaviour machineBehaviour;

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
        Timing.RunCoroutine(_PlayAnimAttack(onHit,onAttackComplete));
    }

    IEnumerator<float> _PlayAnimAttack(Action onHIT, Action onATTACKCOMPLETE)
    {
        anim.Play("Base Layer.TEST_ATTACK");
        yield return Timing.WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        onHIT();
        yield return Timing.WaitForOneFrame;
        onATTACKCOMPLETE();
    }

    public void PlayAnimIdle(Action onIdleComplete)
    {
        //if (!isPlayerTeam)//flip)
        //{
        //    characterSpriteRenderer.flipX = true;
        //}
        anim.Play("Base Layer.TEST_IDLE");

        onIdleComplete();
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
