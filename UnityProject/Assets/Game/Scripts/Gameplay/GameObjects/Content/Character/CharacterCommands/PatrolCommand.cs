using System.Collections.Generic;
using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public class PatrolCommand : ICharacterCommand
    {
        private readonly struct Waypoint
        {
            private readonly Vector3 _point;
            private readonly GameObject _target;
            private readonly bool _isTarget;

            public Waypoint(Vector3 point)
            {
                _point = point;
                _target = null;
                _isTarget = false;
            }

            public Waypoint(GameObject target)
            {
                _point = default;
                _target = target;
                _isTarget = true;
            }

            public bool IsValid =>
                !_isTarget ||
                (_target && (!_target.TryGetComponent(out HealthComponent health) || health.IsAlive));

            public Vector3 Position => _isTarget ? _target.transform.position : _point;
        }

        private readonly List<Waypoint> _waypoints = new();
        private int _index;
        private bool _isStarted;

        public PatrolCommand(Vector3 point) => _waypoints.Add(new Waypoint(point));
        public PatrolCommand(GameObject target) => _waypoints.Add(new Waypoint(target));

        public void AddWaypoint(Vector3 point) => _waypoints.Add(new Waypoint(point));
        public void AddWaypoint(GameObject target) => _waypoints.Add(new Waypoint(target));

        public bool TryExecute(Blackboard blackboard)
        {
            if (!_isStarted)
            {
                GameObject character = blackboard.GetValue(BlackBoardAPI.Character);
                _waypoints.Insert(0, new Waypoint(character.transform.position));
                _index = 1;
                _isStarted = true;
            }

            if (!TryUpdateCurrentPoint(blackboard))
                return false;

            blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)BlackBoardAPI.CommandType.Patrol);
            return true;
        }
        
        public bool TryUpdateCurrentPoint(Blackboard blackboard)
        {
            while (_waypoints.Count > 0)
            {
                if (_index >= _waypoints.Count)
                    _index = 0;

                Waypoint waypoint = _waypoints[_index];
                if (waypoint.IsValid)
                {
                    blackboard.SetPrimitiveValue(BlackBoardAPI.HomePosition, waypoint.Position);
                    return true;
                }

                _waypoints.RemoveAt(_index);
            }

            return false;
        }

        public bool TryMoveNext(Blackboard blackboard)
        {
            if (_waypoints.Count == 0)
                return false;

            _index = (_index + 1) % _waypoints.Count;
            return TryUpdateCurrentPoint(blackboard);
        }
    }
}