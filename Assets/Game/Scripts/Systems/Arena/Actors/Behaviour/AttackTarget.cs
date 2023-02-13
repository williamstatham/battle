using System;
using UniBT;
using Action = UniBT.Action;

namespace Game.Scripts.Systems.Arena.Actors.Behaviour
{
    [Serializable]
    public class AttackTarget : Action
    {
        private ActorView _actorView;
        
        public override void Awake()
        {
            _actorView = gameObject.GetComponent<ActorView>();
        }

        protected override Status OnUpdate()
        {
            if (!_actorView.CanAttackTarget())
            {
                return Status.Failure;
            }
            _actorView.AttackTarget();
            return Status.Running;
        }
    }
}