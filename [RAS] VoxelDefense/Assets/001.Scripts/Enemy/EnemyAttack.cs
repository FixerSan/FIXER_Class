using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyAttack 
{
    protected EnemyController controller;
    protected Defender attackDefender;
    public abstract bool CheckAttack();
    public abstract void StartAttack();
    public abstract void Attack();
    public abstract void EndAttack();
}

namespace EnemyAttacks
{
    public class Zero : EnemyAttack
    {
        public Zero(EnemyController _controller)
        {
            controller = _controller;
        }

        public override void Attack()
        {
            attackDefender?.Hit(controller.data.attackForce);
        }

        public override bool CheckAttack()
        {
            Collider[] colliders = Physics.OverlapSphere(controller.attackTrans.position, controller.attackTrans.localScale.x, controller.attackLayer);
            if (colliders.Length == 0) return false;

            attackDefender = colliders[0].GetComponent<Defender>();
            controller.ChangeState(Define.EnemyState.Attack);
            return true;
        }

        public override void EndAttack()
        {

        }

        public override void StartAttack()
        {

        }
    }
}
