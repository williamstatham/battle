using System;
using UniBT;
using UnityEngine;
using UnityEngine.Serialization;
using Action = UniBT.Action;

namespace Game.Scripts.Systems.Arena.Actors.Behaviour
{
    [Serializable]
    public class MoveToTarget : Action
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
                return Status.Running;
            }

            return _actorView.MoveToTarget() ? Status.Running : Status.Success;
        }
    }
}