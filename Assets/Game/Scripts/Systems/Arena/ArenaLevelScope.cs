using Game.Scripts.Systems.Application;
using Game.Scripts.Systems.Arena.Actors;
using Game.Scripts.Systems.Arena.ViewModel;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Systems.Arena
{
    public sealed class ArenaLevelScope : LifetimeScope
    {
        [SerializeField] private ApplicationSettings applicationSettings;
        [SerializeField] private ActorList actorList;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ActorManager, ActorManager>(Lifetime.Scoped)
                .AsImplementedInterfaces();

            builder.Register<ArenaUIViewModel>(Lifetime.Scoped);
            
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(actorList)
                    .AsImplementedInterfaces();

                componentsBuilder.AddInstance(applicationSettings);
            });
            
            builder.UseEntryPoints(entryPointsBuilder =>
            {
                entryPointsBuilder.Add<ArenaPresenter>();
                entryPointsBuilder.Add<ApplicationPresenter>();
            });
        }
    }
}