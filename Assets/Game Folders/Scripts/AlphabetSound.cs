using System;
using UnityEngine;

public class AlphabetSound : MonoBehaviour
{
    [System.Serializable]
    public class HurufSound
    {
        public string id;
        public AudioClip klip;
    }

    [SerializeField] private HurufSound[] allDatas;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void GetSound(int key)
    {
        source.clip = allDatas[key].klip;
        source.Play();
        
    }
}