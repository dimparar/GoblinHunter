
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyAnimationFSM : AnimationStateManager<EnemyAnimationFSM.EnemyAnimation>
{
    public enum EnemyAnimation
    {
        IDLE, WALK, HIT, TO_DEATH
    }

    void InitStates()
    {
        States.Add(EnemyAnimation.IDLE, new IdleEnemyState(AspectManager));
        States.Add(EnemyAnimation.WALK, new WalkEnemyState(AspectManager));
        States.Add(EnemyAnimation.HIT, new HitEnemyState(AspectManager));
        States.Add(EnemyAnimation.TO_DEATH, new ToDeathEnemyState(AspectManager));

    }

    private void Awake()
    {
        AspectManager = GetComponent<AnimationAspectManager>();
        EventManager.EnemyHitEvent += GetHit;
        EventManager.EnemyDeathEvent += ToDeathAnimation;
        EventManager.EnemyAttackEvent += ToAttackAnimation;
        InitStates();
        CurrentState = States[EnemyAnimation.WALK];
    }

    public Transform GetTransform() {
        return transform;
    }

    public void GetHit(Hittable enemy)
    {
        if (gameObject.GetComponent<Hittable>() == enemy && CurrentState.StateKey != EnemyAnimation.TO_DEATH)
        {
            TransitionToState(EnemyAnimation.HIT);
        }
    }

    public void ToDeathAnimation(Hittable enemy)
    {
        if (gameObject.GetComponent<Hittable>() == enemy && !TerminateFSM)
        {
            TransitionToState(EnemyAnimation.TO_DEATH);
            TerminateFSM = true;
        }
        
    }

    public void ToAttackAnimation()
    {
        TransitionToState(EnemyAnimation.IDLE);
    }
}
