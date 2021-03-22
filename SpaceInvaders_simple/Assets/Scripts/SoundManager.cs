using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton
{
    public AudioSource audioSource;

    public List<AudioClip> playerShootSounds;
    public List<AudioClip> enemyplayerShootSounds;

    public AudioClip explosionSound;

    public AudioClip musicClip;

    // Start is called before the first frame update
    private static SoundManager instance = null;

    // Game Instance Singleton
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayRandomSoundFromList(List<AudioClip> listOfAudioClips, int indexOfClip = -1)
    {
        if (listOfAudioClips != null && listOfAudioClips.Count > 0)
        {
            int randomClipIndex = Random.Range(0, listOfAudioClips.Count);

            if (indexOfClip >= 0)
                audioSource.PlayOneShot(listOfAudioClips[indexOfClip]);
            else
                audioSource.PlayOneShot(listOfAudioClips[randomClipIndex]);
        }
    }

    public void PlayPlayerLaserShootSoundRandom(int indexOfClip = -1)
    {
        PlayRandomSoundFromList(playerShootSounds, indexOfClip);
    }
    public void PlayEnemyLaserShootSoundRandom(int indexOfClip = -1)
    {
        PlayRandomSoundFromList(enemyplayerShootSounds, indexOfClip);
    }

    public void PlayRandomShootSoundByEnum(SpaceShipsTypes spaceShipsType, int indexOfClip = -1)
    {
        if (spaceShipsType == SpaceShipsTypes.Player)
            PlayPlayerLaserShootSoundRandom(indexOfClip);
        else if (spaceShipsType == SpaceShipsTypes.Enemy)
            PlayEnemyLaserShootSoundRandom(indexOfClip);
    }
    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosionSound);
    }
}
