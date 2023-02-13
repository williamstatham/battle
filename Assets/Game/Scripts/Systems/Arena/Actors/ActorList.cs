using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.Systems.Arena.Actors
{
    public sealed class ActorList : MonoBehaviour, IEnumerable<ActorView>, IInitializable
    {
        private List<ActorView> _actors;
        

        void IInitializable.Initialize()
        {
            _actors = GetComponentsInChildren<ActorView>().ToList();
        }

        IEnumerator<ActorView> IEnumerable<ActorView>.GetEnumerator()
        {
            return _actors.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _actors.GetEnumerator();
        }

        public void RemoveActor(ActorView actor)
        {
            _actors.Remove(actor);
        }
    }
}