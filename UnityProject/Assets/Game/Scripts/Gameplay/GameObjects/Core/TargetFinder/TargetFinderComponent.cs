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

        public GameObject FindTarget(TeamComponent teamComponent)
        {
            GameObject newTarget = null;
            int size = Physics.OverlapSphereNonAlloc(transform.position, _unitRadius.Value, _overlapResult);
            
            for (int i = 0; i < size; ++i)
            {
                GameObject overlapObject = _overlapResult[i].gameObject;
                if (CanBeTarget(overlapObject, teamComponent))
                {
                    newTarget = overlapObject;
                }
            }

            if (!CanBeTarget(Target, teamComponent))
            {
                Target = null;
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

        private bool CanBeTarget(GameObject targetCandidate, TeamComponent teamComponent)
        {
            return targetCandidate &&
                   teamComponent.IsEnemy(targetCandidate.gameObject) &&
                   targetCandidate.TryGetComponent(out HealthComponent healthComponent) &&
                   healthComponent.IsAlive;
        }
    }
}