using UnityEngine;

namespace Game.Scripts.Systems.Arena.Team
{
    [CreateAssetMenu(fileName = nameof(TeamSettings), menuName = "Descriptors/Teams/" + nameof(TeamSettings))]
    public class TeamSettings : ScriptableObject
    {
        [SerializeField] private Color teamColor;
        [SerializeField] private bool sharedAI;

        public Color TeamColor => teamColor;
        public bool SharedAI => sharedAI;
    }
}