using System;
using UniBT;
using Action = UniBT.Action;

namespace Game.Scripts.Systems.Arena.Actors.Behaviour
{
    [Serializable]
    public class SearchForTarget : Action
    {
        private ActorView _actorView;
        
        public override void Awake()
        {
            _actorView = gameObject.GetComponent<ActorView>();
        }
        
        protected override Status OnUpdate()
        {
            if (!_actorView.TargetExists())
            {
                _actorView.SearchForTarget();
                return Status.Running;
            }

            return Status.Success;
        }
    }
}