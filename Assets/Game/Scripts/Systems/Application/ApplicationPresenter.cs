using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems.Application
{
    public sealed class ApplicationPresenter : IInitializable
    {
        private ApplicationSettings _applicationSettings;
        
        [Inject]
        private void Construct(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }
        
        void IInitializable.Initialize()
        {
            UnityEngine.Application.targetFrameRate = _applicationSettings.TargetFramerate;
        }
    }
}