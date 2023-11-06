using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class HitEnemyState : BaseAnimationState<EnemyAnimationFSM.EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> hitState = new Dictionary<Aspects, AnimationClip>();

    private float staggerTime;
    private float timer;


    public HitEnemyState(AnimationAspectManager _aspectManager, EnemyAnimationFSM.EnemyAnimation key = EnemyAnimationFSM.EnemyAnimation.HIT)
        : base(key)
    {
        // TODO
        Id = 0;


        staggerTime = 0.5f;
        AspectManager = _aspectManager;

        hitState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("Idle"), 0f);
        hitState[Aspects.LEFT] = new AnimationClip(Animator.StringToHash("IdleLeft"), 0f);
        hitState[Aspects.RIGHT] = new AnimationClip(Animator.StringToHash("IdleRight"), 0f);
        hitState[Aspects.BACK] = new AnimationClip(Animator.StringToHash("IdleBack"), 0f);

        hitState[Aspects.LEFT_FRONT] = new AnimationClip(Animator.StringToHash("IdleLeftFront"), 0f);
        hitState[Aspects.LEFT_BACK] = new AnimationClip(Animator.StringToHash("IdleLeftBack"), 0f);
        hitState[Aspects.RIGHT_FRONT] = new AnimationClip(Animator.StringToHash("IdleRightFront"), 0f);
        hitState[Aspects.RIGHT_BACK] = new AnimationClip(Animator.StringToHash("IdleRightBack"), 0f);

    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = hitState[aspect].State;
        Duration = hitState[aspect].Duration;
    }

    public override void EnterState()
    {
        timer = 0;
        if ( hitState[AspectManager._currentAspectKey].State != Id)
        {
            //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
            ChangeAnimation(AspectManager._currentAspectKey);
        }
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (hitState[AspectManager._currentAspectKey].State != Id)
        {
            //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
            ChangeAnimation(AspectManager._currentAspectKey);
            AspectManager._changeAspect = true;
        }

    }

    public override void ExitState()
    {
        /*sprite.material = originalMaterial;*/
    }

    public override EnemyAnimationFSM.EnemyAnimation GetNextState()
    {
        // TODO
        if (timer > staggerTime)
        {
            return EnemyAnimationFSM.EnemyAnimation.WALK;
        }
        return EnemyAnimationFSM.EnemyAnimation.HIT;

    }
}
