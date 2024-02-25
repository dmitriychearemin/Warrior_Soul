using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character: MonoBehaviour
{
    protected float Speed_Walk;

    protected CharacterStats playerStats;
    public MoveState MoveState { get; set; } = MoveState.Idle;
    public ViewSide ViewSide { get; set; } = ViewSide.OnMe;

    protected abstract void AnimateAttack();
    protected abstract void AnimateMovement();
    protected abstract void FlipSprite(float horizontalInput, float verticalInput);
    protected abstract void ChangeAnimation();
}

public enum MoveState
{
    Idle,
    Walk,
    Run,
    Attack,
    Menu_Open
}

public enum ViewSide
{
    Left,
    Right,
    OnScreen,
    OnMe,
    Up_Right,
    Up_Left,
    Down_Left,
    Down_Right
}
