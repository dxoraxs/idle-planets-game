using Game.Runtime.Infrastructure.Panels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.PlanetPanel
{
    public class PlanetPanel : PanelBase
    {
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _populationText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _incomeText;
        [SerializeField] private TextMeshProUGUI _upgradePriceText;
        [SerializeField] private Button _upgradeButton;
    }
}