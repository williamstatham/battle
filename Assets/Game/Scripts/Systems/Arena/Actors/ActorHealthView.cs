using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Systems.Arena.Actors
{
    public class ActorHealthView : MonoBehaviour
    {
        [SerializeField] private Image progressor;

        private Camera _renderingCamera;
        
        private void Awake()
        {
            _renderingCamera =
                Camera.allCameras.FirstOrDefault(i => (i.cullingMask & gameObject.layer) == gameObject.layer);
        }

        public void SetHealthProgressor(float delta)
        {
            progressor.fillAmount = delta;
        }

        private void Update()
        {
            if (_renderingCamera != null)
            {
                transform.rotation = _renderingCamera.transform.rotation;
            }
        }
    }
}