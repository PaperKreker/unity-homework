using System;
using System.Collections.Generic;
using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public class CharacterAI : MonoBehaviour
    {
        [SerializeField] private Blackboard _blackboard;
        [SerializeField] private Character _character;
        
        [SerializeField] private UnitRadiusComponent _stoppingRadius;
        [SerializeField] private UnitRadiusComponent _shootingRadius;
        [SerializeField] private UnitRadiusComponent _searchingRadius;
        
        public ICharacterCommand CurrentCommand { get; private set; }
        private readonly Queue<ICharacterCommand> _queue = new();

        private void Awake()
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.StoppingDistance, _stoppingRadius.Value);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.ShootingDistance, _shootingRadius.Value);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.SearchingDistance, _searchingRadius.Value);
            ResetPositions();
        }

        public void Stop()
        {
            _queue.Clear();
            ResetState();
        }
        
        public void Execute(ICharacterCommand command, bool enqueue)
        {
            if (enqueue && CurrentCommand != null)
            {
                _queue.Enqueue(command);
                return;
            }

            _queue.Clear();
            StartCommand(command);
        }
        
        public void CompleteCommand()
        {
            while (_queue.Count > 0)
            {
                if (StartCommand(_queue.Dequeue()))
                    return;
            }

            ResetState();
        }

        private bool StartCommand(ICharacterCommand command)
        {
            ResetState();
            
            if (!command.TryExecute(_blackboard))
                return false;

            CurrentCommand = command;
            return true;
        }
        
        private void SetCommand(BlackBoardAPI.CommandType command)
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)command);
        }

        private void ResetTarget()
        {
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, _character.gameObject);
        }
        private void ResetPositions()
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, _character.transform.position);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.HomePosition, _character.transform.position);
        }

        private void ResetState()
        {
            CurrentCommand = null;
            SetCommand(BlackBoardAPI.CommandType.None);
            ResetTarget();
            ResetPositions();
        }
    }
}
