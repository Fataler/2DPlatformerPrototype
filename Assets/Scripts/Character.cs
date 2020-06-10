using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Variables

    public StateMachine movementSM;
    public StandingState standing;
    public AtackingState atacking;
    public JumpingState jumping;

    private float collisionOverlapRadius = .1f;
    private Rigidbody2D rb;
    private int horizonalMoveParam = Animator.StringToHash("HorizontalSpeed");
    public Transform colCheck;

    [SerializeField]
    private CharacterData data;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private LayerMask whatIsGround;

    #endregion Variables

    #region Properties

    public float NormalColliderHeight => data.normalColliderHeight;
    public float JumpForce => data.jumpForce;
    public float MovementSpeed => data.movementSpeed;
    public float CollisionOverlapRadius => collisionOverlapRadius;

    public float ColliderSize
    {
        get => GetComponent<CircleCollider2D>().radius;

        set
        {
            GetComponent<CircleCollider2D>().radius = value;
            Vector3 center = GetComponent<CircleCollider2D>().transform.position;
            center.y = value / 2f;
            GetComponent<CircleCollider2D>().transform.position = center;
        }
    }

    #endregion Properties

    #region Methods

    public void Move(float speed)
    {
        Vector2 dir = speed * transform.right * Time.deltaTime;
        rb.velocity = new Vector2(dir.x * MovementSpeed, rb.velocity.y);
        anim.SetFloat(horizonalMoveParam, Mathf.Abs(rb.velocity.x));
        var side = (speed >= 0) ? 1 : -1;
        if (speed != 0)
        {
            Flip(side);
        }
    }

    public void Flip(int side)
    {
        if (side == -1 && GetComponent<SpriteRenderer>().flipX)
            return;

        if (side == 1 && !GetComponent<SpriteRenderer>().flipX)
            return;

        bool state = (side == 1) ? false : true;
        GetComponent<SpriteRenderer>().flipX = state;
    }

    public void ResetMoveParams()
    {
        rb.angularVelocity = 0;
        anim.SetFloat(horizonalMoveParam, 0f);
    }

    public void ApplyImpulse(Vector2 force)
    {
        GetComponent<Rigidbody2D>().AddForce(force);
    }

    public void SetAnimationBool(int param, bool value)
    {
        anim.SetBool(param, value);
    }

    public void TriggerAnimation(int param)
    {
        anim.SetTrigger(param);
    }

    public bool CheckCollisionOverlap(Vector2 point)
    {
        return Physics2D.OverlapCircleAll(point, CollisionOverlapRadius, whatIsGround).Length > 0;
    }

    public bool isGrounded()
    {
        return rb.velocity.y == 0f ? true : false;
    }

    #endregion Methods

    #region Callbacks

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //callbacks
    private void Start()
    {
        movementSM = new StateMachine();
        standing = new StandingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);

        movementSM.Init(standing);
    }

    private void Update()
    {
        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();
    }

    #endregion Callbacks
}