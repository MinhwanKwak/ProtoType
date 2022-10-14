using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using UnityEngine.AddressableAssets;


public class SoundPlayer : SingletonScriptableObject<SoundPlayer>, ILoader
{
    [System.Serializable]
    public class SoundData
    {
        public string soundKey;
        public AudioClip audioData;
    }
    [SerializeField]
    private List<SoundData> soundDatas = new List<SoundData>();
    private Dictionary<string, AudioClip> soundDataDic = new Dictionary<string, AudioClip>();
    public List<SoundData> SoundDataList { get { return soundDatas; } }

    private List<AudioSource> cachedSources = new List<AudioSource>();
    private List<AudioSource> cachedSpaceSources = new List<AudioSource>();
    private AudioSource BGMSource = null;
    private AudioClip recoveryBGMClip = null;
    private Transform Root = null;


    private bool soundon = true;
    public AudioClip GetSoundData(string key)
    {
        if(soundDataDic.ContainsKey(key))
            return soundDataDic[key];
        
        return null;
    }

    public void SetRoot(Transform _root)
    {
        Root = _root;
    }

    public void PlaySound(string key, float volume = 1f )
    {
        if (!soundon) return;

        var audio = GetAudioSource();
        audio.loop = false;
        audio.PlayOneShot(GetSoundData(key));
        audio.volume = volume;
    }

    public void PlayLoopSound(string key , float volume = 1f)
    {
        if (!soundon) return;

        var audio = GetAudioSource();
        audio.loop = true;
        audio.clip = GetSoundData(key);
        audio.Play();
        audio.volume = volume;

    }

    public void StopLoopSound(string key)
    {
        if (cachedSources != null)
        {
            if (cachedSources.Count > 0)
            {
                var findsource = cachedSources.Find(x => x.clip.name == key);

                if (findsource != null)
                {
                    findsource.Stop();
                }
            }
        }
    }



    IEnumerator waitTimeAndCallback(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    public void SpacePlaySound(string key , Transform root)
    {
        if (!soundon) return;

        

        var audio = GetSpaceAudioSource(root , PlaySoundAction, key);

        if (audio != null)
        {
            PlaySoundAction(audio, key );
        }
    }


    private void PlaySoundAction(AudioSource source , string key)
    {
        source.loop = false;

        source.PlayOneShot(GetSoundData(key));
    }

    public void PlayBGM(string key, bool recovery = false)
    {
        if(BGMSource == null)
        {
            BGMSource = new GameObject("sound entity").AddComponent<AudioSource>();
            BGMSource.transform.SetParent(Root);
        }

        BGMSource.loop = true;
        var clip = GetSoundData(key);
        if(recovery)
            recoveryBGMClip = clip;
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    public void RecoveryBGM()
    {
        BGMSource.clip = recoveryBGMClip;
        BGMSource.Play();
    }

    public void BgmVolum(float volume)
    {
        BGMSource.volume = volume;
        BGMSource.Play();
    }

    public void BgmSwitch(bool value)
    {
        if(BGMSource != null)
            BGMSource.mute = !value;
    }
    public void EffectSwitch(bool value)
    {
        soundon = value;
    }

    private AudioSource GetAudioSource()
    {
        AudioSource audio = null;

        foreach(var source in cachedSources)
        {
            if(!source.isPlaying)
            {
                audio = source;
                return audio;
            }
        }
    
        if(audio == null)
        {
            audio = new GameObject("sound entity").AddComponent<AudioSource>();
            cachedSources.Add(audio);
            audio.transform.SetParent(Root);
        }

        return audio;
    }

    private AudioSource GetSpaceAudioSource(Transform root , System.Action<AudioSource , string> AddAction = null , string key = "")
    {
        AudioSource audio = null;

        foreach (var source in cachedSpaceSources)
        {
            if (source == null)
                break;

            if (!source.isPlaying)
            {
                audio = source;
                audio.transform.SetParent(root);
                audio.transform.localPosition = Vector3.zero;
            
                audio.spatialBlend = 1f;
                audio.dopplerLevel = 0;
                return audio;
            }
        }

        


        if (audio == null)
        {
            Addressables.InstantiateAsync("SpaceSound").Completed += (handle) =>
            {
                audio = handle.Result.GetComponent<AudioSource>();
                cachedSpaceSources.Add(audio);
                audio.transform.SetParent(root);
                audio.transform.localPosition = Vector3.zero;
                audio.spatialBlend = 1f;
                audio.dopplerLevel = 0;
                AddAction?.Invoke(audio , key);
            };

        }

        return audio;
    }

    public void Init()
    {
        cachedSpaceSources.Clear();
    }


    public void Load()
    {
        cachedSources.Clear();          
        cachedSpaceSources.Clear();
        soundDataDic.Clear();
        foreach(var sd in soundDatas)
        {
            soundDataDic.Add(sd.soundKey, sd.audioData);
        }
    }
}
