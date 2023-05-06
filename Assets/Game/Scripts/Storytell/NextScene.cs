using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
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