using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip boom;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip heal;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip gameOver;
    [SerializeField] AudioClip coin;
    [SerializeField] AudioClip button;

    public void PlayBoom()
    {
        audioSource.PlayOneShot(boom);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jump);
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(hit);
    }

    public void PlayHeal()
    {
        audioSource.PlayOneShot(heal);
    }

    public void PlayShoot()
    {
        audioSource.PlayOneShot(shoot);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOver);
    }

    public void PlayCoin()
    {
        audioSource.PlayOneShot(coin);
    }

    public void PlayButton()
    {
        audioSource.PlayOneShot(button);
    }
}
