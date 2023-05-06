using Game.Audio.Scripts;
using UnityEngine;

namespace Game.Scripts.TaskAnimatedCharacters
{
    public class DuckAnimat : ParentAnima
    {
        [SerializeField] private BoxCollider2D _boxCollider;
        [SerializeField] private LayerMask _layerAferDetection;
        public override void PlayerDeathAnim()
        {
            AudioPlayer.Audio.PlayOneShotSound(AudioSounds.CrackSound);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CompareTag("TaskKilla"))
            {
                _boxCollider.enabled = true;
                gameObject.layer = _layerAferDetection;
            }

            if (col.CompareTag("Player"))
            {
                AudioPlayer.Audio.PlayOneShotSound(AudioSounds.CrackSound);
            }
        }
    }
}