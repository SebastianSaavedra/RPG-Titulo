using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

//[Serializable]
//public class Animations
//{

//    public WeaponAnims[] weaponAnimsArray;

//    [Serializable]
//    public struct WeaponAnims
//    {
//        public string weaponName;
//        public List<AnimationClip> anims;
//    }
//}

public class Character_Anims : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer characterSpriteRenderer;
    [HideInInspector] public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        characterSpriteRenderer = GetComponent<SpriteRenderer>();
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
        yield return Timing.WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
        onHIT();
        yield return Timing.WaitForOneFrame;
        onATTACKCOMPLETE();
        yield break;
    }

    public int GetCurrentAnimatorClipInfoLength()
    {
        return anim.GetCurrentAnimatorClipInfo(0).Length;
    }

    public void PlayAnimIdle()
    {
        anim.Play("Base Layer.IDLE");
    }

    public void PlayAnimMoving(Vector3 moveDir)
    {
        if (moveDir.x < 0)
        {
            characterSpriteRenderer.flipX = false;
        }
        else if(moveDir.x > 0)
        {
            characterSpriteRenderer.flipX = true;
        }
        anim.Play("Base Layer.WALKING");
    }

    //public void PlayAnimStarter()
    //{
    //    anim.Play("Base Layer.START");
    //}
}
