using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    public EnemyData data;
    public EnemyAttack attack;
    public Dictionary<Define.EnemyState, State<EnemyController>> states = new Dictionary<Define.EnemyState, State<EnemyController>>();
    public StateMachin<EnemyController> fsm;

    public Define.EnemyState currentState;

    public LayerMask attackLayer;
    public Transform attackTrans;
    public float canAttackDistance;
    public float attackRange;
    public float attackSpeed;


    public void Init(int _enemyIndex)
    {
        attack = new EnemyAttacks.Zero(this);
        states = new Dictionary<Define.EnemyState, State<EnemyController>>();
        states.Add(Define.EnemyState.Create, new EnemyStates.Zero.Create());
        states.Add(Define.EnemyState.Idle, new EnemyStates.Zero.Idle());
        fsm = new StateMachin<EnemyController>(this, states[Define.EnemyState.Create]);
    }

    public void ChangeState(Define.EnemyState _changeState, bool _isCanChangeSameState = false)
    {
        if (currentState == _changeState && !_isCanChangeSameState) return;
        fsm.ChangeState(states[_changeState]);
    }

    private void Update()
    {
        fsm.UpdateState();
    }

    private void FixedUpdate()
    {
        fsm.FixedUpdateState();
    }
}
