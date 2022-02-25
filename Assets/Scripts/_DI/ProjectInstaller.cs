using Gameplay.PlayerInput;
using Zenject;

namespace _DI
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputHandler>().FromInstance(new InputHandler()).AsSingle();
        }
    }
}