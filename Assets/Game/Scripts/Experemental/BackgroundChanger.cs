using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Experemental
{
    public class BackgroundChanger : MonoBehaviour
    {
        [SerializeField]
        private Image imageRenderer;

        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();

        private int index = 0;

        private void Awake()
        {
            imageRenderer.sprite = sprites[index];
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextBackground();
            }
        }

        private void NextBackground()
        {
            index += 1;
            if (index > sprites.Count - 1)
                index = 0;

            imageRenderer.sprite = sprites[index];
        }
    }
}