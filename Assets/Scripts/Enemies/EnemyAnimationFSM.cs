
using UnityEngine;

public class EnemyAnimationFSM : AnimationStateManager<EnemyAnimationFSM.EnemyAnimation>
{

    public enum EnemyAnimation
    {
        IDLE, WALK, HIT
    }

    void InitStates()
    {
        States.Add(EnemyAnimation.IDLE, new IdleEnemyState(AspectManager));
        States.Add(EnemyAnimation.WALK, new WalkEnemyState(AspectManager));
        States.Add(EnemyAnimation.HIT, new HitEnemyState(AspectManager));

    }

    private void Awake()
    {
        AspectManager = GetComponent<AnimationAspectManager>();
        EventManager.EnemyHitEvent += GetHit; 
        InitStates();
        CurrentState = States[EnemyAnimation.IDLE];
    }

    public Transform GetTransform() {
        return transform;
    }
    public void GetHit(Hittable enemy) 
    {
        if (gameObject.GetComponent<Hittable>() == enemy)
        {
            TransitionToState(EnemyAnimation.HIT);
        }
    }
}
