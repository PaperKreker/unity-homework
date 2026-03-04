using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class PositionClamp : MonoBehaviour
    {
        [SerializeField]
        private TransformBounds _area;

        private void LateUpdate()
        {
            ClampPosition();
        }

        private void ClampPosition()
        {
            transform.position = _area.ClampInBounds(transform.position);
        }
    }
}