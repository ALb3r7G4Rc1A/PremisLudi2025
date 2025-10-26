using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string menuSongName = "MenuSong";
    void Start()
    {
        AudioManager.Instance.Play(menuSongName);   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGameScene()
    {
        AudioManager.Instance.Stop(menuSongName); 
        SceneManager.LoadScene(1);
    }
}
