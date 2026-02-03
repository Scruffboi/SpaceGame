using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SpaceGame
{

    public class SoundManager : MonoBehaviour
    {
        [Header("Audio Clips")]
        [SerializeField] private AudioClip shoot;
        [SerializeField] private AudioClip startGame;
        [SerializeField] private AudioClip bigExplosion;
        [SerializeField] private AudioClip smallExplosion;
        [SerializeField] private AudioClip powerup;
        [SerializeField] private AudioClip lifeup;

        [Header("Sound Manager")]
        [SerializeField] private AudioSource audioSource;

        [SerializeField] Dictionary<string, AudioClip> soundClips;

        private AudioSource thrusterSource;
        [SerializeField] private float thrusterVolumeMult = 1;
        [SerializeField] private float thrusterMinVolume = 0.1f;

        private void Awake()
        {
            soundClips = new Dictionary<string, AudioClip>
            {
                { "start_game", startGame },
                { "player_shoot", shoot },
                { "explosion", bigExplosion },
                { "small_explosion", smallExplosion },
                { "powerup", powerup },
                { "lifeup", lifeup },
            };
            GameManager.GetInstance().OnGameStart += OnGameStart;
            GameManager.GetInstance().OnGameOver += OnGameOver;
        }

        private void OnGameStart()
        {
            thrusterSource = GameManager.GetInstance().GetPlayer().thrusterAudioSource;
            thrusterSource.Play();
        }

        private void OnGameOver()
        {
            thrusterSource.Stop();
        }

        public void Shoot(AudioSource source)
        {
            source.pitch = Random.Range(0.8f, 1.2f);
            source.PlayOneShot(soundClips["player_shoot"]);
        }

        public void PlaySound(string clipName, Vector3 new_position)
        {
            AudioSource new_source = Instantiate(audioSource, new_position, Quaternion.identity);
            new_source.PlayOneShot(soundClips[clipName]);
            Destroy(new_source, soundClips[clipName].length);
        }

        public void Thrust(float volume)
        {
            thrusterSource.volume = thrusterMinVolume + Mathf.MoveTowards(thrusterSource.volume, volume, thrusterVolumeMult * Time.deltaTime);
        }
    }

}
