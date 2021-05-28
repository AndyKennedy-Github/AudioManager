using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aek.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public bool debug;

        //Set this to create the default audio source
        public AudioSource m_source;

        //Holds a reference to all Audio Sources you want to pull from
        public List<AudioSource> sources = new List<AudioSource>();

        //Holds a reference to all the clips you want to use in the scene
        public List<AudioObject> clips = new List<AudioObject>();

        [System.Serializable]
        public class AudioObject
        {
            public string name;
            public AudioClip clip;
        }

        private class AudioJob
        {
            public AudioAction action;
            public AudioSource source;
            public string name;
            public int index;
            public bool fade;
            public float delay;


            public AudioJob(AudioAction _action, AudioSource _source, string _name, bool _fade, float _delay)
            {
                action = _action;
                source = _source;
                name = _name;
                fade = _fade;
                delay = _delay;
            }

            public AudioJob(AudioAction _action, AudioSource _source, int _index, bool _fade, float _delay)
            {
                action = _action;
                source = _source;
                index = _index;
                fade = _fade;
                delay = _delay;

            }
        }

        private enum AudioAction
        {
            START,
            STOP,
            RESTART
        }


        #region Public Functions

        public void PlayAudio(string _name, AudioSource _source = null, bool _fade = false, float _delay = 0.0f)
        {
            AddJob(new AudioJob(AudioAction.START, _source, _name, _fade, _delay));
        }
        public void PlayAudio(int _index, AudioSource _source = null, bool _fade = false, float _delay = 0.0f)
        {
            AddJob(new AudioJob(AudioAction.START, _source, _index, _fade, _delay));
        }
        public void StopAudio(string _name, AudioSource _source = null, bool _fade = false, float _delay = 0.0f)
        {
            AddJob(new AudioJob(AudioAction.STOP, _source, _name, _fade, _delay));
        }
        public void StopAudio(int _index, AudioSource _source = null, bool _fade = false, float _delay = 0.0f)
        {
            AddJob(new AudioJob(AudioAction.STOP, _source, _index, _fade, _delay));
        }
        public void RestartAudio(string _name, AudioSource _source = null, bool _fade = false, float _delay = 0.0f)
        {
            AddJob(new AudioJob(AudioAction.RESTART, _source, _name, _fade, _delay));
        }
        public void RestartAudio(int _index, AudioSource _source = null, bool _fade = false, float _delay = 0.0f)
        {
            AddJob(new AudioJob(AudioAction.RESTART, _source, _index, _fade, _delay));
        }

        public AudioClip GetAudioClip(string _name)
        {
            foreach (AudioObject _obj in clips)
            {
                if (_obj.name == _name)
                {
                    return _obj.clip;
                }
            }
            return null;
        }

        #endregion

        #region Private Functions
        private IEnumerator RunAudioJob(AudioJob _job)
        {
            yield return new WaitForSeconds(_job.delay);

           
            if(_job.source == null)
            {
                _job.source = m_source;
            }
            Log("Current Audio Source is: " + _job.source);
            if(_job.name != null)
            {
                Log(_job.name);
                Log(GetAudioClip(_job.name).name);
                _job.source.clip = GetAudioClip(_job.name);
                //_job.source.clip = clips[clips.IndexOf(_job.name)];
            }
            else
            {
                Log(clips[_job.index].name);
                _job.source.clip = clips[_job.index].clip;
            }

            switch (_job.action)
            {
                case AudioAction.START:
                    _job.source.Play();
                    break;
                case AudioAction.STOP:
                    if (!_job.fade)
                    {
                        _job.source.Stop();
                    }
                    break;
                case AudioAction.RESTART:
                    _job.source.Stop();
                    _job.source.Play();
                    break;
            }

            if (_job.fade)
            {
                float _initial = _job.action == AudioAction.START || _job.action == AudioAction.RESTART ? 0.0f : 1.0f;
                float _target = _initial == 0 ? 1 : 0;
                float _duration = 1.0f;
                float _timer = 0.0f;

                while (_timer <= _duration)
                {
                    _job.source.volume = Mathf.Lerp(_initial, _target, _timer / _duration);
                    _timer += Time.deltaTime;
                    yield return null;
                }

                if (_job.action == AudioAction.STOP)
                {
                    _job.source.Stop();
                }
            }

            //Log("Current Job Count: " + m_JobTable.Count);

            yield return null;
        }

        private void AddJob(AudioJob _job)
        {
            
            //start job
            IEnumerator _jobRunner = RunAudioJob(_job);
            StartCoroutine(_jobRunner);
            Log("Starting job on [" + _job.name + "] with the operation: " + _job.action);
        }
       
        private void Log(string _msg)
        {
            if (!debug) return;
            Debug.Log("[AudioManager]: " + _msg);
        }

        private void LogWarning(string _msg)
        {
            if (!debug) return;
            Debug.LogWarning("[AudioManager]: " + _msg);
        }

        #endregion
    }
}
