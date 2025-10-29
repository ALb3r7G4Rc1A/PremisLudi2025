using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string menuSongName = "MenuSong";
    [SerializeField] private string levelSongName = "LevelSong";
    [SerializeField] private AudioMixer audioMixer; // assigna l'AudioMixer des de l'Inspector

    private const string MUSIC_PARAM = "MusicVolume";
    private const string SFX_PARAM = "SFXVolume";

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.Instance.Play(menuSongName);
        }  
    }

    public void LoadGameScene()
    {
        AudioManager.Instance.Stop(menuSongName);
        SceneManager.LoadScene(2);
    }
    public void LoadTutorialScene()
    {
        AudioManager.Instance.Stop(menuSongName);
        SceneManager.LoadScene(1);
    }

    // --- CONTROL DE MÚSICA ---
    public void MuteMusic()
    {
        audioMixer.SetFloat(MUSIC_PARAM, -80f); // pràcticament silenci
    }

    public void UnmuteMusic()
    {
        audioMixer.SetFloat(MUSIC_PARAM, 0f); // volum normal (ajusta segons la teva configuració)
    }

    // --- CONTROL DE SFX ---
    public void MuteSFX()
    {
        audioMixer.SetFloat(SFX_PARAM, -80f);
    }

    public void UnmuteSFX()
    {
        audioMixer.SetFloat(SFX_PARAM, 0f);
    }

    public void LoadMenuScene()
    {
        AudioManager.Instance.Stop(levelSongName);
        SceneManager.LoadScene(0);
    }
}
