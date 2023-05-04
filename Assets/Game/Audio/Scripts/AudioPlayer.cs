using UnityEngine;

using Random = UnityEngine.Random;

namespace Game.Audio.Scripts
{
    public class AudioPlayer : MonoBehaviour
    {
        public static AudioPlayer Audio { get; private set; }

        [Header("AudioSources")]
        [SerializeField] private AudioSource _audioSourceMain;

        [SerializeField] private AudioSource _audioSourceSecondary;
        [SerializeField] private AudioSource _audioSourceBackground;

        [Header("Clips")]
        [SerializeField] private AudioClip _someClip;

        private void Awake()
        {
            if (Audio != null && Audio != this)
                Destroy(this);
            else
                Audio = this;
        }

        public void StatBackgroundMusic(bool start)
        {
            if (start)
                _audioSourceBackground.Play();
            else
                _audioSourceBackground.Pause();
        }

        public void SetAudioSourceActive(bool isActive)
        {
            _audioSourceMain.enabled = isActive;
        }

        public void SetAudioSourceVolume(float newVolumeValue)
        {
            _audioSourceMain.volume = newVolumeValue;
            _audioSourceSecondary.volume = newVolumeValue;

            if (!_audioSourceMain.isPlaying)
                _audioSourceMain.Play();
        }

        /// <summary>
        /// Use for multiple clips play to end only last of them
        /// </summary>
        public void PlaySound(AudioSounds soundToPlay)
        {
            DoPlay(_audioSourceMain, soundToPlay);
        }

        /// <summary>
        /// Use for multiple clips play to end only last of them,
        /// Secondary audio source that should play parallel
        /// </summary>
        public void PlaySoundAsSecondary(AudioSounds soundToPlay)
        {
            DoPlay(_audioSourceSecondary, soundToPlay);
        }

        /// <summary>
        /// Use for multiple clips play to end each of them
        /// </summary>
        public void PlayOneShotSound(AudioSounds soundToPlay, float soundVolume = 1f, bool randomPitch = false)
        {
            DoOneShotSound(_audioSourceMain, soundToPlay, soundVolume, randomPitch);
        }

        public void StopMainSounds()
        {
            _audioSourceMain.Stop();
        }

        public void StopSecondarySounds()
        {
            _audioSourceSecondary.Stop();
        }

        private void DoPlay(AudioSource source, AudioSounds soundToPlay)
        {
            if (!_audioSourceMain.enabled)
                return;

            source.pitch = 1f;
            source.clip = AudioToPlay(soundToPlay);
            if (source.clip != null)
                source.Play();
        }

        private void DoOneShotSound(AudioSource source, AudioSounds soundToPlay, float soundVolume,
            bool randomPitch)
        {
            if (!_audioSourceMain.enabled)
                return;

            source.pitch = randomPitch ? Random.Range(0.8f, 1.2f) : 1f;

            AudioClip sfxClip = AudioToPlay(soundToPlay);

            if (sfxClip != null)
                source.PlayOneShot(sfxClip, soundVolume * _audioSourceMain.volume);
        }

        private AudioClip AudioToPlay(AudioSounds soundToPlay)
        {
            return soundToPlay switch
            {
                _ => null
            };
        }
    }
}