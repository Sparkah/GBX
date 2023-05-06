using Game.Player;
using UnityEngine;
using System.Collections;

namespace Assets.Game.Scripts
{
    public class PlayerHelper : MonoBehaviour
    {
        private PlayerController player;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
        }

        public void ShowPlayer()
        {
            player.gameObject.SetActive(true);
        }

        public void HidePlayer()
        {
            player.gameObject.SetActive(false);
        }
    }
}