using UnityEngine;

public class GroundedState : State
{
    protected float speed;

    private float horizontalInput;

    public GroundedState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        horizontalInput = 0.0f;
        speed = character.MovementSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        //character.ResetMoveParams();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        horizontalInput = Input.GetAxis("Horizontal");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        character.Move(horizontalInput * speed);
    }
}