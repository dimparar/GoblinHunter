using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HitEnemyState : BaseAnimationState<EnemyAnimationFSM.EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> hitState = new Dictionary<Aspects, AnimationClip>();

    private SpriteRenderer sprite;
    private Transform enemyTransform;
    private float staggerTime;
    private float timer;

    private bool flash;

    private Material flashMaterial;
    private Material originalMaterial;
    public HitEnemyState(AnimationAspectManager _aspectManager, EnemyAnimationFSM.EnemyAnimation key = EnemyAnimationFSM.EnemyAnimation.HIT)
        : base(key)
    {
        // TODO
        Id = 0;

        //sprite renderer
        sprite = _aspectManager.gameObject.GetComponentInChildren<SpriteRenderer>();
        flashMaterial = Object.Instantiate(sprite.material);
        Color currentColor = flashMaterial.color;
        Color newColor = currentColor * 4;
        flashMaterial.color = newColor;
        originalMaterial = sprite.material;
       
        enemyTransform = _aspectManager.gameObject.GetComponentInChildren<Transform>();
        staggerTime = 0.5f;
        flash = true;
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

        enemyTransform.Translate(100 * Time.deltaTime * -enemyTransform.forward, Space.World);
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

        if (flash)
        {
            sprite.material = flashMaterial;
            flash = false;
        }
        else 
        {
            sprite.material = originalMaterial;
            flash = true;
        }

    }

    public override void ExitState()
    {
        sprite.material = originalMaterial;
    }

    public override EnemyAnimationFSM.EnemyAnimation GetNextState()
    {
        // TODO
        if (timer > staggerTime)
        {
            return EnemyAnimationFSM.EnemyAnimation.IDLE;
        }
        return EnemyAnimationFSM.EnemyAnimation.HIT;

    }
}
