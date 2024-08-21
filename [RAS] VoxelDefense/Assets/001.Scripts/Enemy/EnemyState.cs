using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    namespace Zero
    {
        public class Create : State<EnemyController>
        {
            public override void StartState(EnemyController _entity)
            {
                base.StartState(_entity);

            }

            public override void EndState()
            {

            }

            public override void FixedUpdateState()
            {

            }

            public override void UpdateState()
            {

            }
        }

        public class Idle : State<EnemyController>
        {
            public override void StartState(EnemyController _entity)
            {
                base.StartState(_entity);

            }

            public override void EndState()
            {

            }

            public override void FixedUpdateState()
            {
                if (entity.attack.CheckAttack()) return;
            }

            public override void UpdateState()
            {

            }
        }
    }
}
