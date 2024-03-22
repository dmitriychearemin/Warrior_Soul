using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [SerializeField]private float Default_Speed = 250f;

    [SerializeField] Inventory _inventory;

    private float horizontalInput = 0, verticalInput = 0;
    private float halfScreenX = Screen.width / 2, halfScreenY = Screen.height / 2;
    private const int angleCoeff = 30;

    [SerializeField]private Transform attackCollider;

    private InputHandler input;
    //private new Transform transform;
    private Rigidbody2D rb;
    private Animator animatorContoller;

    private float animationTime = 0;
    private const float walkDuration = 0.08f; 
    private const float attackDuration = 1.2f;

    //private Vector3 Default_State;

    private void Awake()
    {
        Speed_Walk = Default_Speed;
        stats = GetComponent<CharacterStats>();
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animatorContoller = GetComponent<Animator>();
        Default_State =
            new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Start()
    {
        input = InputHandler.Instance;
        EnemyAttack.EnemyDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        EnemyAttack.EnemyDamage -= TakeDamage;
    }

    private void FixedUpdate()
    {
        AnimateMovement();
        AnimateAttack();
    }

    private void AnimateAttack()
    {
        if (input.AttackTriggered && stats.Stamina > 0
            && !(horizontalInput != 0 || verticalInput != 0))
        {
            var x = input.MousePosInput.x;
            var y = input.MousePosInput.y;

            float angle = (float)Math.Atan2(y - halfScreenY, x - halfScreenX) 
                * (float)(180 / Math.PI);
            if (angle > 0)
            {
                if (angle <= 90)
                {
                    if (angle < 45 - angleCoeff)
                        FlipSprite(1f, 0f);
                    else if (angle > 45 + angleCoeff)
                        FlipSprite(0f, 1f);
                    else
                        FlipSprite(1f, 1f);
                }
                else
                {
                    if (angle > 135 + angleCoeff)
                        FlipSprite(-1f, 0f);
                    else if (angle < 135 - 10)
                        FlipSprite(0f, 1f);
                    else
                        FlipSprite(-1f, 1f);
                }
            }
            else
            {
                if (angle > -90)
                {
                    if (angle > -45 + angleCoeff)
                        FlipSprite(1f, 0f);
                    else if (angle < -45 - angleCoeff)
                        FlipSprite(0f, -1f);
                    else
                        FlipSprite(1f, -1f);
                }
                else
                {
                    if (angle < -135 - angleCoeff)
                        FlipSprite(-1f, 0f);
                    else if (angle > -135 + angleCoeff)
                        FlipSprite(0f, -1f);
                    else
                        FlipSprite(-1f, -1f);
                }
            }
        }
    }

    protected override void AnimateMovement()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            FlipSprite(horizontalInput, verticalInput);
            float speed = Speed_Walk 
                * (InputHandler.RunTriggered && !stats.HoldShift ? 2 : 1);
            rb.velocity = speed * Time.deltaTime * new Vector2(horizontalInput, verticalInput);
        }
    }

    private void FlipAttackCollider()
    {
        switch (ViewSide)
        {
            case ViewSide.Up_Left:
                attackCollider.SetLocalPositionAndRotation(new Vector3(-0.59f, 0.89f),
                        Quaternion.Euler(new Vector3(0, 0, 50)));
                break;
            case ViewSide.Up_Right:
                attackCollider.SetLocalPositionAndRotation(new Vector3(-0.61f, 0.88f),
                        Quaternion.Euler(new Vector3(0, 0, 50)));
                break;
            case ViewSide.OnMe:
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.0073f, -1.08f),
                        Quaternion.Euler(new Vector3(0, 0, 0)));
                break;
            case ViewSide.Left:
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.87f, 0.14f),
                        Quaternion.Euler(new Vector3(0, 0, 90)));
                break;
            case ViewSide.Right:
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.87f, 0.14f),
                        Quaternion.Euler(new Vector3(0, 0, 90)));
                break;
            case ViewSide.Down_Left:
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.74f, -0.73f),
                        Quaternion.Euler(new Vector3(0, 0, 50)));
                break;
            case ViewSide.Down_Right:
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.986f, -0.65f),
                        Quaternion.Euler(new Vector3(0, 0, 50)));
                break;
            case ViewSide.OnScreen:
                attackCollider.SetLocalPositionAndRotation(new Vector3(0.0073f, 0.93f),
                        Quaternion.Euler(new Vector3(0, 0, 0)));
                break;
            default:
                break;
        }
    }

    protected override void ChangeAnimation()
    {
        FlipAttackCollider();
        if (InputHandler.RunTriggered && !stats.HoldShift)
        {
            MoveState = MoveState.Run;
            switch (ViewSide) 
            {
                case ViewSide.Up_Left:
                    animatorContoller.Play("Run_Up_Left");
                    break;
                case ViewSide.Up_Right:
                    animatorContoller.Play("Run_Up_Left");
                    break;
                case ViewSide.OnMe:
                    animatorContoller.Play("Run_Down");
                    break;
                case ViewSide.Left:
                    animatorContoller.Play("Run_Right");
                    break;
                case ViewSide.Right:
                    animatorContoller.Play("Run_Right");
                    break;
                case ViewSide.Down_Left:
                    animatorContoller.Play("Run_Down_Right");
                    break;
                case ViewSide.Down_Right:
                    animatorContoller.Play("Run_Down_Right");
                    break;
                case ViewSide.OnScreen:
                    animatorContoller.Play("Run_Up");
                    break;
                default:
                    break;
            }
            animationTime = walkDuration;
        }
        else if (input.AttackTriggered && !(horizontalInput != 0 || verticalInput != 0))
        {
            MoveState = MoveState.Attack;
            switch (ViewSide)
            {
                case ViewSide.Up_Left:
                    animatorContoller.Play("Attack_Up_Left");
                    break;
                case ViewSide.Up_Right:
                    animatorContoller.Play("Attack_Up_Left");
                    break;
                case ViewSide.OnMe:
                    animatorContoller.Play("Attack_Down");
                    break;
                case ViewSide.Left:
                    animatorContoller.Play("Attack_Right");
                    break;
                case ViewSide.Right:
                    animatorContoller.Play("Attack_Right");
                    break;
                case ViewSide.Down_Left:
                    animatorContoller.Play("Attack_Down_Right");
                    break;
                case ViewSide.Down_Right:
                    animatorContoller.Play("Attack_Down_Right");
                    break;
                case ViewSide.OnScreen:
                    animatorContoller.Play("Attack_Up");
                    break;
                default:
                    break;
            }
            animationTime = attackDuration;
        }
        else
        {
            MoveState = MoveState.Walk;
            switch (ViewSide)
            {
                case ViewSide.Up_Left:
                    animatorContoller.Play("Walk_Up_Left");
                    break;
                case ViewSide.Up_Right:
                    animatorContoller.Play("Walk_Up_Left");
                    break;
                case ViewSide.OnMe:
                    animatorContoller.Play("Move_Down");
                    break;
                case ViewSide.Left:
                    animatorContoller.Play("Walk_Right");
                    break;
                case ViewSide.Right:
                    animatorContoller.Play("Walk_Right");
                    break;
                case ViewSide.Down_Left:
                    animatorContoller.Play("Walk_Down_Right");
                    break;
                case ViewSide.Down_Right:
                    animatorContoller.Play("Walk_Down_Right");
                    break;
                case ViewSide.OnScreen:
                    animatorContoller.Play("Move_Up");
                    break;
                default:
                    break;
            }
            animationTime = walkDuration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = input.MoveInput.x;
        verticalInput = input.MoveInput.y;

        animationTime -= Time.deltaTime;

        if (animationTime <= 0)
        {
            rb.velocity = Vector2.zero;
            MoveState = MoveState.Idle;
            animatorContoller.Play("Idol_Animation");
        }
    }

    public void DeadAnimation()
    {
        transform.localScale = Default_State;
        animatorContoller.Play("Die_Player");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Loot_Item")
        {

            string _nameobject = collision.gameObject.name;
            Sprite _sptriteobject = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            _inventory.Add_Element_In_Cell(_nameobject,_sptriteobject);
        }
    }

}
