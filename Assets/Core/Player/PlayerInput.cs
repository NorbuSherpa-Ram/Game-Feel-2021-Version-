using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public struct FrameInput
{
    public Vector2 Move;
    public bool Jump;
    public bool Jetpack;
    public bool StopJet;
}

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction _playeInputAction;

    public FrameInput frameInput { get; private set; }

    private InputAction move, jump, jetpack,StopJet;

    private void Awake()
    {
        _playeInputAction = new PlayerInputAction();
        move = _playeInputAction.Player.Move;
        jump = _playeInputAction.Player.Jump;
        jetpack = _playeInputAction.Player.RightMouse;
        StopJet = _playeInputAction.Player.RightMouse;
    }
    private void OnEnable()
    {
        _playeInputAction.Enable();
    }
    private void OnDisable()
    {
        _playeInputAction.Disable();
    }

    private void Update()
    {
        frameInput = GatherInput();
    }

    private FrameInput GatherInput()
    {
        return new FrameInput
        {
            Move = move.ReadValue<Vector2>(),
            Jump = jump.WasPerformedThisFrame(),
            Jetpack = jetpack.WasPerformedThisFrame(),
            StopJet = jetpack.WasReleasedThisFrame() 
        };
    }
}
