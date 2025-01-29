using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.GamePanel.Planet
{
    public class PlanetView : MonoBehaviour
    {
        [SerializeField] private Image _planetIcon;
        [SerializeField] private Button _planetButton;
        [SerializeField] private LockState _lockVisual;
        [SerializeField] private OpenState _openVisual;
    }
}