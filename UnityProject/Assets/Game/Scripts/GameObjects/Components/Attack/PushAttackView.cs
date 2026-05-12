using System;
using UnityEngine;

namespace Game
{
    public class PushAttackView : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem _effect;
        
        [SerializeField] 
        private AudioComponent _audio;
        
        [SerializeField] 
        private Animator _animator;
        
        private RequestComponent _pushRequest;
        private readonly int _blowUpKey = Animator.StringToHash("BlowUp");

        private void Awake()
        {
            _pushRequest = GetComponent<RequestComponent>();
            _pushRequest.OnInvoke += ShowAttack;
        }

        private void ShowAttack()
        {
            _animator.SetTrigger(_blowUpKey);
            _audio.Play("Push");
            _effect.Play();
        }
    }
}
    