using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class Sound
    {
        public string soundName;
        public AudioClip soundClip;
        public bool isLoop;

        [HideInInspector] public AudioSource source;
    }

    [SerializeField] private Sound[] allSounds;
    [SerializeField] private bool isBgmPlayed;

    private void Awake()
    {
        foreach (var s in allSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.soundClip;
            s.source.loop = s.isLoop;
        }
    }

    public void PlaySound(string key)
    {
        var _find = Array.Find(allSounds, s => s.soundName == key);
        if (_find != null)
        {
            _find.source.Play();
            if (_find.isLoop) { isBgmPlayed = true; }
        }
    }

    public void MuteBGM(bool isMute)
    {
        var _find = Array.Find(allSounds, s => s.isLoop == true);
        if (_find != null)
        {
            _find.source.mute = isMute;
        }
    }

    public bool IsBgmPlay()
    {
        return isBgmPlayed;
    }
}
