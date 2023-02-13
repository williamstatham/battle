using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Systems.Arena.Team;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Scripts.Systems.Arena.Actors
{
    public class ActorManager : IStartable, IDisposable
    {
        private ActorList _actorList;

        private readonly Dictionary<TeamSettings, ActorView> _sharedAITargets = new();
        private readonly HashSet<ActorView> _deadActors = new();

        public event Action BattleEnded = () => { };

        [Inject]
        private void Construct(ActorList actorList)
        {
            _actorList = actorList;
        }
        
        void IStartable.Start()
        {
            foreach (ActorView actorView in _actorList)
            {
                actorView.ActorDied += OnActorDied;
                actorView.TargetRequired += FindTargetForActor;
            }
        }
        
        void IDisposable.Dispose()
        {
            foreach (ActorView actorView in _actorList)
            {
                actorView.ActorDied -= OnActorDied;
                actorView.TargetRequired -= FindTargetForActor;
            }
        }
        
        private ActorView FindTargetForActor(ActorView actor)
        {
            if (!CheckRemainingTeams())
            {
                return null;
            }
            
            if (actor.TeamSettings.SharedAI)
            {
                if (!_sharedAITargets.TryGetValue(actor.TeamSettings, out ActorView targetActor))
                {
                    return _sharedAITargets[actor.TeamSettings] = FindGeneralEnemyForTeam(actor.TeamSettings);
                }

                if (targetActor != null)
                {
                    return targetActor;
                }

                _sharedAITargets.Remove(actor.TeamSettings);
                return _sharedAITargets[actor.TeamSettings] = FindGeneralEnemyForTeam(actor.TeamSettings);
            }

            return _actorList.OrderBy(i => Vector3.Distance(i.transform.position, actor.transform.position))
                .FirstOrDefault(i => i.TeamSettings != actor.TeamSettings);
        }

        private bool CheckRemainingTeams()
        {
            HashSet<TeamSettings> remainingTeams = new HashSet<TeamSettings>();
            foreach (ActorView actor in _actorList)
            {
                remainingTeams.Add(actor.TeamSettings);
            }

            if (remainingTeams.Count <= 1)
            {
                BattleEnded.Invoke();
                return false;
            }

            return true;
        }

        private ActorView FindGeneralEnemyForTeam(TeamSettings teamSettings)
        {
            if (!teamSettings.SharedAI)
            {
                throw new NotSupportedException();
            }

            return _actorList.FirstOrDefault(i => i.TeamSettings != teamSettings);
        }
        
        private void OnActorDied(ActorView actorView)
        {
            actorView.ActorDied -= OnActorDied;
            actorView.TargetRequired -= FindTargetForActor;
            _deadActors.Add(actorView);
        }

        public void UpdateBattleState()
        {
            foreach (ActorView actor in _actorList)
            {
                actor.Tick();
            }
            
            foreach (ActorView deadActor in _deadActors)
            {
                _actorList.RemoveActor(deadActor);
                Object.Destroy(deadActor.gameObject);
            }
            
            _deadActors.Clear();
        }
    }
}