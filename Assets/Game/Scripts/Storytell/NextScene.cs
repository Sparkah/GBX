using UnityEngine;
using System.Collections;
using Game.Audio.Scripts;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private void Start()
    {
        AudioSystem.StopSceneMusicAction?.Invoke(true);
        AudioPlayer.StatBackgroundMusicAction?.Invoke(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}