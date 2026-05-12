using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class AudioComponent : SerializedMonoBehaviour
    {
        [SerializeField]
        Dictionary<string, AudioClip> _sources;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(string key)
        {
            _audioSource.PlayOneShot(_sources[key]);
        }
    }
}
