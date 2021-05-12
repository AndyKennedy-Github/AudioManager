using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aek.Audio
{
            public class TestAudio : MonoBehaviour
            {
                public AudioController ac;

#region Unity Functions
#if UNITY_EDITOR
                private void Update()
                {
                    if(Input.GetKeyUp(KeyCode.T))
                    {
                        ac.PlayAudio(AudioType.ST_01, true);
                    }
                    if (Input.GetKeyUp(KeyCode.G))
                    {
                        ac.StopAudio(AudioType.ST_01, true);
                    }
                    if (Input.GetKeyUp(KeyCode.B))
                    {
                        ac.RestartAudio(AudioType.ST_01, true);
                    }

                    if (Input.GetKeyUp(KeyCode.Y))
                    {
                        ac.PlayAudio(AudioType.SFX_01);
                    }
                    if (Input.GetKeyUp(KeyCode.H))
                    {
                        ac.StopAudio(AudioType.SFX_01);
                    }
                    if (Input.GetKeyUp(KeyCode.N))
                    {
                        ac.RestartAudio(AudioType.SFX_01);
                    }

                    if (Input.GetKeyUp(KeyCode.A))
                    {
                        ac.PlayAudio(AudioType.ST_02);
                    }
                }
#endif
#endregion
            }
        }
