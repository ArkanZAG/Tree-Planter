using System;
using UnityEngine;

namespace Controller
{
    public class SoundController : MonoBehaviour
    {
        private const string SFX_VOLUME_SETTING = "sfx-volume";
        private const string BGM_VOLUME_SETTING = "bgm-volume";
        
        [SerializeField] private AudioSource bgmSource, sfxSource;

        private float sfxVolume = 1f;
        private float bgmVolume = 1f;

        private void Awake()
        {
            sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_SETTING, 1f);
            bgmVolume = PlayerPrefs.GetFloat(BGM_VOLUME_SETTING, 1f);
        }

        public void SetSfxVolume(float volume)
        {
            sfxVolume = volume;
            PlayerPrefs.SetFloat(SFX_VOLUME_SETTING, volume);
        }

        public void SetBgmVolume(float volume)
        {
            bgmVolume = volume;
            PlayerPrefs.SetFloat(BGM_VOLUME_SETTING, volume);
        }

        public void PlaySfx(AudioClip clip, float normalizedVolume = 1f)
        {
            sfxSource.volume = normalizedVolume * sfxVolume;
            sfxSource.PlayOneShot(clip);
        }

        public void PlayBgm(AudioClip clip, float normalizedVolume = 1f)
        {
            bgmSource.Stop();
            bgmSource.clip = clip;
            bgmSource.volume = normalizedVolume * bgmVolume;
            bgmSource.Play();
        }
    }
}
