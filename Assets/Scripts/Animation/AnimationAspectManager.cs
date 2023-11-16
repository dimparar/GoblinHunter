using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Aspects
{
    FRONT, LEFT_FRONT, LEFT, LEFT_BACK, BACK, RIGHT_BACK, RIGHT, RIGHT_FRONT
}

public class AnimationAspectManager : MonoBehaviour
{
    public Aspects _currentAspectKey;
    public bool _changeAspect = false;

    private Vector3 _targetPos;
    private Vector3 _targetDir;
    private Transform _player;

    public bool disableAspects = false;

    public float angle;

    private void Start()
    {
        _player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        UpdateAnimationAspect();
    }

    public void UpdateAnimationAspect()
    {
        _targetPos = new Vector3(_player.position.x, transform.position.y, _player.position.z);
        _targetDir = _targetPos - transform.position;

        angle = Vector3.SignedAngle(_targetDir, transform.forward, transform.up);

        _currentAspectKey = GetAspectFromAngle(angle);
    }

    public Aspects GetAspectFromAngle(float angle)
    {
        if (disableAspects)
        {
            return Aspects.FRONT;
        }

        if (angle > -22.5f && angle < 22.6f) return Aspects.FRONT;
        if (angle >= 22.5f && angle < 67.5f) return Aspects.LEFT_FRONT;
        if (angle >= 67.5f && angle < 112.5f) return Aspects.RIGHT;
        if (angle >= 112.5f && angle < 157.5f) return Aspects.LEFT_BACK;
        if (angle <= -157.5f || angle >= 157.5f) return Aspects.BACK;
        if (angle >= -157.5 && angle < -112.5f) return Aspects.RIGHT_BACK;
        if (angle >= -112.5 && angle < -67.5f) return Aspects.LEFT;
        if (angle >= -67.5 && angle < 22.5f) return Aspects.RIGHT_FRONT;

        return Aspects.FRONT;


    }


    /*
    public Aspects GetAspectFromAngle(float angle)
    {

        // Front
        if (angle <= 45f && angle >= 0f || (angle > -45f && angle <= 0f))
        {
            return Aspects.FRONT;
        }

        // Left
        if (angle > 45 && angle <= 135)
        {
            return Aspects.RIGHT;
        }

        // Back
        if (angle > 135f ||( angle >= -180f && angle <= -165f))
        {
            return Aspects.BACK;
        }

        // Right
        if (angle > -165f && angle <= -45f)
        {
            return Aspects.LEFT;
        }

        else return Aspects.FRONT;
    }
    */

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, _targetPos);
    }

}
