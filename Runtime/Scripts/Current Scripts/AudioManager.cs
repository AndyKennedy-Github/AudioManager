using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aek.Audio
{
    /// <summary>
    /// The script that handles Audio in the scene.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        //! Set this to true to show log messages in the console to track progression of AudioManager
        public bool debug;

        //! Set this to create the default audio source
        public AudioSource m_source;

        //! Holds a reference to all Audio Sources you want to pull from
        public List<AudioSource> sources = new List<AudioSource>();

        //! Holds a reference to all the clips you want to use in the scene
        public List<AudioObject> clips = new List<AudioObject>();

        //! AudioObject constructor
        //! Allows you to search clips via a name and index as opposed to just using an index with a list of regular clips
        [System.Serializable]
        public class AudioObject
        {
            public string name;
            public AudioClip clip;
        }

        //! AudioJob constructor
        //! The constructor that gets read in by the Coroutine 'RunAudioJob'
        private class AudioJob
        {
            public AudioAction action;
            public AudioSource source;
            public string name;
            public int index;
            public bool fade;
            public float delay;
            public bool loop;


            public AudioJob(AudioAction _action, AudioSource _source, string _name, bool _fade, float _delay, bool _loop)
            {
                action = _action;
                source = _source;
                name = _name;
                fade = _fade;
                delay = _delay;
                loop = _loop;
            }

            public AudioJob(AudioAction _action, AudioSource _source, int _index, bool _fade, float _delay, bool _loop)
            {
                action = _action;
                source = _source;
                index = _index;
                fade = _fade;
                delay = _delay;
                loop = _loop;
            }
        }

        //! A list of actions the AudioManager can perform, can be added to and modified further
        private enum AudioAction
        {
            START,
            STOP,
            RESTART
        }


        #region Public Functions
        /// <summary>
        /// A list of the functions that are referenced by objects in code.
        /// The functions are doubled because you should be able to search a 
        /// clip by its name or its index.
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_source"></param>
        /// <param name="_fade"></param>
        /// <param name="_delay"></param>
        public void PlayAudio(string _name, AudioSource _source = null, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.START, _source, _name, _fade, _delay, _loop));
        }
        public void PlayAudio(int _index, AudioSource _source = null, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.START, _source, _index, _fade, _delay, _loop));
        }
        public void StopAudio(string _name, AudioSource _source = null, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.STOP, _source, _name, _fade, _delay, _loop));
        }
        public void StopAudio(int _index, AudioSource _source = null, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.STOP, _source, _index, _fade, _delay, _loop));
        }
        public void RestartAudio(string _name, AudioSource _source = null, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.RESTART, _source, _name, _fade, _delay, _loop));
        }
        public void RestartAudio(int _index, AudioSource _source = null, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.RESTART, _source, _index, _fade, _delay, _loop));
        }
        public void PlayAudio(string _name, int _source = 0, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.START, sources[_source], _name, _fade, _delay, _loop));
        }
        public void PlayAudio(int _index, int _source = 0, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.START, sources[_source], _index, _fade, _delay, _loop));
        }
        public void StopAudio(string _name, int _source = 0, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.STOP, sources[_source], _name, _fade, _delay, _loop));
        }
        public void StopAudio(int _index, int _source = 0, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.STOP, sources[_source], _index, _fade, _delay, _loop));
        }
        public void RestartAudio(string _name, int _source = 0, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.RESTART, sources[_source], _name, _fade, _delay, _loop));
        }
        public void RestartAudio(int _index, int _source = 0, bool _fade = false, float _delay = 0.0f, bool _loop = false)
        {
            AddJob(new AudioJob(AudioAction.RESTART, sources[_source], _index, _fade, _delay, _loop));
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
        /// <summary>
        /// Where the magic happens. The coroutine takes a job,
        /// reads all of its parameters, then adjusts what action it
        /// takes on the clip and source depending on the parameters set.
        /// </summary>
        /// <param name="_job"></param>
        /// <returns></returns>
        private IEnumerator RunAudioJob(AudioJob _job)
        {
            yield return new WaitForSeconds(_job.delay);


            if (_job.source == null)
            {
                _job.source = m_source;
            }
            Log("Current Audio Source is: " + _job.source);
            if (_job.name != null)
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

            _job.source.loop = _job.loop;

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
                    _job.source.loop = false;
                    _job.source.Stop();
                }
            }

            //Log("Current Job Count: " + m_JobTable.Count);

            yield return null;
        }

        private void AddJob(AudioJob _job)
        {
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
