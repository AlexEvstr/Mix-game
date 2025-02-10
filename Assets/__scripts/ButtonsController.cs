using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _goodShotSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _shotSoundd;
    [SerializeField] private AudioClip _winSoundd;
    private SceneTransitionManager _sceneTransitionManager;
    [SerializeField] private AudioSource _musicManager;

    private void Start()
    {
        Time.timeScale = 1;
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        Screen.orientation = ScreenOrientation.Portrait;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = PlayerPrefs.GetFloat("MusicVolumeValue", 1);
        _musicManager.volume = PlayerPrefs.GetFloat("MusicVolumeValue", 1f);
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
    public void PlayExplosionSound()
    {
        _audioSource.PlayOneShot(_explosionSound);
    }
    public void PlayGoodShotSound()
    {
        _audioSource.PlayOneShot(_goodShotSound);
    }
    public void PlayLooseSound()
    {
        _audioSource.PlayOneShot(_loseSound);
    }
    public void PlayShootSound()
    {
        _audioSource.PlayOneShot(_shotSoundd);
    }

    public void PlayWinSound()
    {
        _audioSource.PlayOneShot(_winSoundd);
    }

    public void HomeBtn()
    {
        _sceneTransitionManager.LoadScene("MenuScene");
    }
}