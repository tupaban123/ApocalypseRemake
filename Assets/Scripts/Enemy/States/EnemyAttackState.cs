using UnityEngine;
using Apocalypse.StatesMachine;
using Apocalypse.Enemy;
using Apocalypse.CodeBase;
using System.Collections;

public class EnemyAttackState : State
{
    private EnemyModel _enemy;

    private ICoroutineRunner _coroutineRunner;

    public EnemyAttackState(StateMachine stateMachine, EnemyModel enemy, ICoroutineRunner coroutineRunner) 
        : base(stateMachine)
    {
        _enemy = enemy;
        _coroutineRunner = coroutineRunner;

        Debug.Log(_coroutineRunner);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    private void StartAttack()
    {

    }

    private IEnumerator Attacking()
    {
        while(true)
        {
            Debug.Log("Attack!");
            yield return new WaitForSeconds(0.35f);
        }
    }
}
