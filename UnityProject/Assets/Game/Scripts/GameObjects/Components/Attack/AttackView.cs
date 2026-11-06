using System;
using UnityEngine;

namespace Game
{
    public class AttackView : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem _effect;
        
        [SerializeField] 
        private AudioComponent _audio;
        
        [SerializeField] 
        private Animator _animator;
        
        [SerializeField]
        private string _animationKey;
        
        [SerializeField]
        private string _audioKey;
        
        private DirectPushComponent _directPush;
        private int _animationHash;

        private void Awake()
        {
            _animationHash = Animator.StringToHash(_animationKey);
            _directPush = GetComponent<DirectPushComponent>();
            _directPush.OnAttack += Show;
        }
        
        private void OnDestroy()
        {
            _directPush.OnAttack -= Show;
        }

        private void Show()
        {
            _animator.SetTrigger(_animationHash);
            _audio.Play(_audioKey);
            _effect.Play();
        }
    }
}
    