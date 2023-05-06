using UnityEngine;
using System.Collections;
using UnityEditor.Sequences;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ЫУЧ : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SceneManager.LoadScene(0);
    }
}