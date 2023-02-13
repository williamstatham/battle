using System.Linq;
using UnityEngine;

namespace Game.Scripts.Core.Utils
{
    public class FollowCameraRotation : MonoBehaviour
    {
        private Camera _renderingCamera;
        
        private void Start()
        {
            _renderingCamera =
                Camera.allCameras.FirstOrDefault(i => (i.cullingMask & gameObject.layer) == gameObject.layer);
        }
        
        private void Update()
        {
            if ((object) _renderingCamera != null)
            {
                transform.rotation = _renderingCamera.transform.rotation;
            }
        }
    }
}