using System;
using UnityEngine;

namespace Game
{
    public class TossAttackView : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem _effect;
        
        [SerializeField] 
        private AudioComponent _audio;
        
        [SerializeField] 
        private Animator _animator;
        
        private RequestComponent _pushRequest;
        private readonly int _blowForwardKey = Animator.StringToHash("BlowForward");

        private void Awake()
        {
            _pushRequest = GetComponent<RequestComponent>();
            _pushRequest.OnInvoke += ShowAttack;
        }

        private void ShowAttack()
        {
            _animator.SetTrigger(_blowForwardKey);
            _audio.Play("Toss");
            _effect.Play();
        }
    }
}
    