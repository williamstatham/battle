using System;
using Game.Scripts.Systems.Arena.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Systems.Arena.UI
{
    public class BattleTimeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private string textFormat;
        [SerializeField] private string timeFormat;

        private ArenaUIViewModel _arenaUIViewModel;

        [Inject]
        private void Construct(ArenaUIViewModel arenaUIViewModel)
        {
            _arenaUIViewModel = arenaUIViewModel;
        }

        private void Start()
        {
            _arenaUIViewModel.CurrentBattleTime
                .Subscribe(OnBattleTimeChange)
                .AddTo(this);
            
            OnBattleTimeChange(_arenaUIViewModel.CurrentBattleTime.Value);
        }

        private void OnBattleTimeChange(TimeSpan time)
        {
            timeText.text = string.Format(textFormat, time.ToString(timeFormat));
        }
    }
}