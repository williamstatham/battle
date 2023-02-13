using UnityEngine;

namespace Game.Scripts.Systems.Arena.Team
{
    public class TeamColorSetter : MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private string shaderPropertyName;

        private MaterialPropertyBlock _mpb;

        private void Awake()
        {
            _mpb = new MaterialPropertyBlock();
        }
        
        public void SetColor(Color color)
        {
            _mpb.SetColor(Shader.PropertyToID(shaderPropertyName), color);
            renderer.SetPropertyBlock(_mpb);
        }
    }
}