using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class OpenCloseObject : Interactable
{

    [SerializeField] bool locked = false;
    private bool _objectOpen = false;
    private bool _interact = false;
    private string prompt_message;
    public string prompt_message_close = "Press E to close the door";
    public string prompt_message_open = "Press E to open the door";
    public string closed = "Closed";
    public string close = "Close";
    public string opened = "Opened";
    public string open = "Open";

    // ---------- //
    // Animations //
    // ---------- //

    private int CLOSED;
    private int CLOSE;
    private int OPEN;
    private int OPENED;
    
    private Animator _animator;

    private float _lockedStateTime;

    private int _currentState;

    // Start is called before the first frame update
    void Start()
    {
       // _promptMessage = "Press E to open the door";
        _animator = GetComponent<Animator>();
        CLOSED = Animator.StringToHash(closed);
        CLOSE = Animator.StringToHash(close);
        OPENED = Animator.StringToHash(opened);
        OPEN = Animator.StringToHash(open);
    }
    /*
    protected override void Interact()
    {
        _interact = true;
        Debug.Log("Interact with " + gameObject.name);
    }
    */
    private int LockState(int state, float lt)
    {
        this._lockedStateTime = Time.time + lt;
        return state;
    }

    private int GetState()
    {
        if (Time.time < _lockedStateTime) return _currentState;

        if (!_objectOpen)
        {
            prompt_message = prompt_message_open;
            if (_interact)
            {
                _interact = false;
                _objectOpen = !_objectOpen;

                return LockState(OPEN, 0.9f);

            }
            return CLOSED;
        }
        else
        {
            prompt_message = prompt_message_close;
            if (_interact)
            {
                _interact = false;
                _objectOpen = !_objectOpen;

                return LockState(CLOSE, 0.9f);

            }
            return OPENED;
        }
    }

    public void ChangeAnimationState(int state)
    {
        if (state == this._currentState) return;

        _animator.CrossFadeInFixedTime(state, 0, 0);
        this._currentState = state;
    }


    void Update()
    {
        var state = this.GetState();
        this.ChangeAnimationState(state);

    }

    public override void Interact()
    {
        if (locked) 
        {
            if (PlayerProfile.gameData.currentArea.HasKey())
            {
                _interact = true;
            }
            else
            {
                prompt_message = "You need a Key";
            }
        }
        else 
        {
            _interact = true;
        }

    }

    public override string GetPromptMessage()
    {
        return prompt_message;
    }
}