using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aek.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioObj : MonoBehaviour
    {
        public AudioManager am;

        [SerializeField]
        private AudioSource source;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                am.PlayAudio(0, source);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                am.PlayAudio(0);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                am.RestartAudio("Correct");
            }
        }
    }
}

