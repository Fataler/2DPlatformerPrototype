using UnityEngine;

public class StandingState : GroundedState
{
    private bool jump;

    public StandingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        speed = character.MovementSpeed;
        jump = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        jump = Input.GetButtonDown("Jump");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }
    }
}