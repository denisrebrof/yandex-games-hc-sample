using Sound.data;
using Sound.domain;
using UnityEngine;
using Zenject;

namespace Sound._di
{
    [CreateAssetMenu(menuName = "Installers/SoundInstaller")]
    public class SoundInstaller: ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISoundPrefsRepository>().To<PlayerPrefsSoundPrefsRepository>().FromNew().AsSingle();
        }
    }
}