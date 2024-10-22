using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip test;

    private void Start()
    {
        SetMusicVolume();
        SetSFXVolume();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music", Mathf.Log10(musicSlider.value)*20);
        if(musicSlider.value == 0)
        {
            audioMixer.SetFloat("Music", -80f);
        }
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("Sounds", Mathf.Log10(soundSlider.value)*20);
        if(soundSlider.value == 0)
        {
            audioMixer.SetFloat("Sounds", -80f);
        }
        StartCoroutine(TestAudioClipPlay());
    }

    IEnumerator TestAudioClipPlay()
    {
        audioSource.clip = test;
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
    }
}
