using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private int _idleHash;
    public PlayerIdleState(PlayerStateMachine playerStateMachine) :  base(playerStateMachine)
    {
        if (_idleHash == 0)
        {
            _idleHash = _playerStateMachine._player.PlayerAnimationData.IdleParameterHash;
        }
    }

    public override void Enter()
    {
        Debug.Log("Idle");
        _playerStateMachine.moveSpeedModifier = 0f;
        base.Enter();
        StartAnimation(_idleHash);
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Idle Exit");
        StopAnimation(_idleHash);
        // 애니메이션 해제
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (_playerStateMachine.moveInput != (Vector2)_playerStateMachine._player.transform.position)
        {
            OnMove();
            return;
        }
    }

    protected override void OnMove()
    {
        base.OnMove();
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }
    protected override void OnDie()
    {
        base.OnDie();
    }
}