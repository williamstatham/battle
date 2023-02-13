using System;
using Game.Scripts.Systems.Arena.Team;
using UniBT;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Systems.Arena.Actors
{
    public sealed class ActorView : MonoBehaviour
    {
        [SerializeField] private BehaviorTree behaviourTree;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private ActorHealthView healthView;
        
        [Space]
        [SerializeField] private TeamSettings teamSettings;
        [SerializeField] private TeamColorSetter teamColorSetter;
        
        [SerializeField] private float health;
        [SerializeField] private float attackRange;
        [SerializeField] private float damagePerSecond;

        private ActorView _currentTarget;

        private float _currentHealth;

        public TeamSettings TeamSettings => teamSettings;

        public event Func<ActorView, ActorView> TargetRequired;
        public event Action<ActorView> ActorDied = (self) => { };

        private void Awake()
        {
            _currentHealth = health;
        }

        private void Start()
        {
            teamColorSetter.SetColor(teamSettings.TeamColor);
        }

        public bool TargetExists()
        {
            return _currentTarget != null;
        }

        public void SearchForTarget()
        {
            _currentTarget = TargetRequired?.Invoke(this);
        }

        public bool CanAttackTarget()
        {
            if (!TargetExists())
            {
                return false;
            }

            return Vector3.Distance(_currentTarget.transform.position, transform.position) <= attackRange;
        }

        public void Tick()
        {
            behaviourTree.Tick();
            ValidateLineRenderer();
        }

        public bool MoveToTarget()
        {
            if (CanAttackTarget())
            {
                agent.isStopped = true;
                return false;
            }

            agent.SetDestination(_currentTarget.transform.position);
            agent.isStopped = false;
            return true;
        }

        public void AttackTarget()
        {
            _currentTarget.Damage(damagePerSecond * Time.deltaTime);
        }

        private void Damage(float damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            healthView.SetHealthProgressor(_currentHealth / health);
            if (_currentHealth <= 0)
            {
                ActorDied.Invoke(this);
            }
        }

        private void ValidateLineRenderer()
        {
            if (!TargetExists() || !CanAttackTarget())
            {
                lineRenderer.gameObject.SetActive(false);
                return;
            }
            lineRenderer.gameObject.SetActive(true);
            
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _currentTarget.transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Color prevColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = prevColor;
        }
    }
}