
using System;
using Game.Scripts.Systems.Arena.Actors;
using Game.Scripts.Systems.Arena.ViewModel;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems.Arena
{
    public sealed class ArenaPresenter : IStartable, ITickable, IDisposable
    {
        private ActorManager _actorManager;
        private ArenaUIViewModel _arenaUIViewModel;

        private bool _gameRunning;
        private float _battleTimeSeconds;
        
        [Inject]
        private void Construct(ActorManager actorManager, ArenaUIViewModel arenaUIViewModel)
        {
            _actorManager = actorManager;
            _arenaUIViewModel = arenaUIViewModel;
        }
        
        void IStartable.Start()
        {
            _actorManager.BattleEnded += OnBattleEnded;
            _gameRunning = true;
        }

        void IDisposable.Dispose()
        {
            _actorManager.BattleEnded -= OnBattleEnded;
        }

        void ITickable.Tick()
        {
            if (!_gameRunning)
            {
                return;
            }
            _actorManager.UpdateBattleState();
            _battleTimeSeconds += Time.deltaTime;
            _arenaUIViewModel.CurrentBattleTime.Value = TimeSpan.FromSeconds(_battleTimeSeconds);
        }
        
        private void OnBattleEnded()
        {
            _gameRunning = false;
        }
    }
}