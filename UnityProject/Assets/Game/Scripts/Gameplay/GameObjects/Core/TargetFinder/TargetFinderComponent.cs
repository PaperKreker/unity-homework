using System;
using UnityEngine;

namespace SampleGame
{
    public sealed class TargetFinderComponent : MonoBehaviour
    {
        public GameObject Target { get; private set; }

        private UnitRadiusComponent _unitRadius;
        private Collider[] _overlapResult = new Collider[32];
        
        private void Awake()
        {
            _unitRadius = GetComponent<UnitRadiusComponent>();
        }

        public GameObject FindTarget(TeamType team = TeamType.Neutral)
        {
            GameObject newTarget = null;
            int size = Physics.OverlapSphereNonAlloc(transform.position, _unitRadius.Value, _overlapResult);
            
            for (int i = 0; i < size; ++i)
            {
                if (CanBeTarget(_overlapResult[i], team))
                {
                    newTarget = _overlapResult[i].gameObject;
                }
            }

            if (!newTarget)
            {
                Target = null;
            }
            else if (Target == null)
            {
                Target = newTarget;
            }
            return Target;
        }

        private bool CanBeTarget(Collider targetCollider, TeamType team)
        {
            return targetCollider &&
                targetCollider.TryGetComponent(out TeamComponent teamComponent) &&
                teamComponent.Team == team &&
                targetCollider.TryGetComponent(out HealthComponent healthComponent) &&
                healthComponent.IsAlive;
        }
    }
}