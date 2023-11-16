using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GoblinAnimationFSM;

public class GoblinHitState : BaseAnimationState<GoblinAnimation>
{
    private Dictionary<Aspects, AnimationClip> hitState = new Dictionary<Aspects, AnimationClip>();

    private GoblinStateMachine _stateMachine;

    private SpriteRenderer sprite;
    private Transform enemyTransform;
    private float staggerTime;
    private float timer;

    private bool flash;

    private Material flashMaterial;
    private Material originalMaterial;
    public GoblinHitState(AnimationAspectManager _aspectManager, GoblinStateMachine stateMachine, GoblinAnimation key = GoblinAnimation.HIT)
        : base(key)
    {
        // TODO
        Id = 0;

        _stateMachine = stateMachine;

        //sprite renderer
        sprite = _aspectManager.gameObject.GetComponentInChildren<SpriteRenderer>();
        flashMaterial = Object.Instantiate(sprite.material);
        Color currentColor = flashMaterial.color;
        Color newColor = currentColor * 4;
        flashMaterial.color = newColor;
        originalMaterial = sprite.material;

        enemyTransform = _aspectManager.gameObject.GetComponent<Transform>();
        staggerTime = 0.5f;
        flash = true;
        AspectManager = _aspectManager;

        hitState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("Hit"), 0f);
    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = hitState[aspect].State;
        Duration = hitState[aspect].Duration;
    }

    public override void EnterState()
    {
        timer = 0;        
        ChangeAnimation(Aspects.FRONT);

        enemyTransform.Translate(100 * Time.deltaTime * -enemyTransform.forward, Space.World);
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        //if (hitState[AspectManager._currentAspectKey].State != Id)
        //{
        //    //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
        //    ChangeAnimation(AspectManager._currentAspectKey);
        //    AspectManager._changeAspect = true;
        //}

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

    public override GoblinAnimation GetNextState()
    {
        // TODO
        if (timer > staggerTime)
        {
            if (_stateMachine.isAttacking)
            {
                return GoblinAnimation.ATTACK;
            }

            if (_stateMachine.Agent.velocity.magnitude > 0)
            {
                return GoblinAnimation.WALK;
            }

            return GoblinAnimation.IDLE;
        }
        return GoblinAnimation.HIT;

    }
}
