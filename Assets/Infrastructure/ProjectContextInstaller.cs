using Game.Audio.Scripts;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private AudioSystem _audioSystem;
        [SerializeField] private GameProgressHandler _gameProgressHandler;
       

        public override void InstallBindings()
        {
            BindGameProgress();
            BindAudioSystem();
        }

        private void BindGameProgress()
        {
            World world = _gameProgressHandler.InitWorld();
            Container.Bind<World>().FromInstance(world).AsSingle();
        }
        

        private void BindAudioSystem()
        {
            Container.Bind<AudioSystem>().FromInstance(_audioSystem).AsSingle();
        }
    }
}