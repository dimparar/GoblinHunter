using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private float sensitivity;
    [SerializeField] private float _rotationLimitUp = -35;
    [SerializeField] private float _rotationLimitDown = 45;

    protected Vector3 _lookDir;

    protected float _holderRotation;

    private Quaternion initialRotation;
    private Quaternion nextRotation;

    [SerializeField] private float tiltAmount;
    [SerializeField] private float tiltSpeed;


    private void Start()
    {
        initialRotation = _cameraHolder.localRotation;
        tiltAmount = 2f;
        tiltSpeed = 10f;
    }

    public virtual void Rotate(Vector2 input)
    {
        _lookDir.x = input.x * sensitivity * Time.deltaTime;
        _lookDir.y = input.y * sensitivity * Time.deltaTime;

        _holderRotation -= _lookDir.y;
        _holderRotation = Mathf.Clamp(_holderRotation, _rotationLimitDown, _rotationLimitUp);

        rotateVertical();
        rotateHorizontal();

        initialRotation = _cameraHolder.localRotation;
        PlayerProfile.UpdateRotation(gameObject.transform.rotation.eulerAngles);

    }

    protected virtual void rotateVertical()
    {
        Vector3 vec = _cameraHolder.localRotation.eulerAngles;
        _cameraHolder.localRotation = Quaternion.Euler(_holderRotation, 0f, vec.z);
    }


    protected virtual void rotateHorizontal()
    {
        transform.Rotate(Vector3.up* _lookDir.x);
    }

    public void CameraTilt(Vector2 input)
    {
        float rotationZ = input.x;
        Quaternion rotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, rotationZ * -tiltAmount);
        _cameraHolder.localRotation = Quaternion.Slerp(initialRotation, rotation, tiltSpeed * Time.deltaTime);

    }

}
