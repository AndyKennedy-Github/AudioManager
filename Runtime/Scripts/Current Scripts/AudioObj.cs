using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aek.Audio
{
    /// <summary>
    /// A simple script to hold your reference to your game manager and can be easily edited
    /// to fit your needs.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioObj : MonoBehaviour
    {
        public AudioManager am;

        [SerializeField]
        private AudioSource source;

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                am.PlayAudio(0, source, false, 0, true);
            }
        }
    }
}

