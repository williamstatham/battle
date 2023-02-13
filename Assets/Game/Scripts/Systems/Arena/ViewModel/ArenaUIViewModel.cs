using System;
using UniRx;

namespace Game.Scripts.Systems.Arena.ViewModel
{
    public sealed class ArenaUIViewModel
    {
        public readonly ReactiveProperty<TimeSpan> CurrentBattleTime = new();
    }
}