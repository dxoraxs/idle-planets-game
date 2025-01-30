using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.GamePanel.Planet
{
    public class PlanetView : MonoBehaviour
    {
        [field: SerializeField] public Image PlanetIcon;
        [field: SerializeField] public Button PlanetButton;
        [field: SerializeField] public LockState LockVisual;
        [field: SerializeField] public OpenState OpenVisual;
    }
}