using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicManager;
    [SerializeField] private AudioSource _soundManager;
    [SerializeField] private GameObject _musicOn;
    [SerializeField] private GameObject _musicOff;
    [SerializeField] private AudioClip _clickSound;

    private void Start()
    {
        _musicManager.volume = PlayerPrefs.GetFloat("MusicVolumeValue", 1f);
        if (_musicManager.volume == 1f) EnableMusic(); else DisableMusic();

        _soundManager.volume = PlayerPrefs.GetFloat("MusicVolumeValue", 1);

    }

    public void DisableMusic()
    {
        _musicOn.SetActive(false);
        _musicOff.SetActive(true);
        _musicManager.volume = 0;
        PlayerPrefs.SetFloat("MusicVolumeValue", 0);
    }

    public void EnableMusic()
    {
        _musicOff.SetActive(false);
        _musicOn.SetActive(true);
        _musicManager.volume = 1;
        PlayerPrefs.SetFloat("MusicVolumeValue", 1f);
    }

    public void PlayClickSound()
    {
        _soundManager.PlayOneShot(_clickSound);
    }
}