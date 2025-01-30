using System;
using Game.Runtime.Infrastructure.Panels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.PlanetPanel
{
    public class PlanetPanel : PanelBase
    {
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _populationText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _incomeText;
        [SerializeField] private TextMeshProUGUI _upgradePriceText;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _closeButton;
        private IPlanetPanelPresenter _presenter;
        private string _defaultHeaderText;
        private string _defaultPopulationText;
        private string _defaultLevelText;
        private string _defaultIncomeText;
        private string _defaultUpgradePriceText;

        private void Start()
        {
            _defaultHeaderText = _header.text;
            _defaultPopulationText = _populationText.text;
            _defaultLevelText = _levelText.text;
            _defaultIncomeText = _incomeText.text;
            _defaultUpgradePriceText = _upgradePriceText.text;
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }

        public void SetPresenter(IPlanetPanelPresenter presenter)
        {
            _presenter = presenter;
            _presenter.OnPlanetSelected += SetInfoByPlanetId;
            _presenter.OnChangeResource += SetUpgradeButtonInteractable;
            
            _upgradeButton.onClick.AddListener(_presenter.OnClickUpgradeButton);
            _closeButton.onClick.AddListener(_presenter.OnClickCloseButton);
        }

        private void SetUpgradeButtonInteractable(bool interactable)
        {
            _upgradeButton.interactable = interactable;
        }
        
        private void SetInfoByPlanetId(string planetId)
        {
            var currentPlanet = _presenter.GetPlanetById(planetId);
            
            _avatar.sprite = currentPlanet.Avatar;
            _header.text = string.Format(_defaultHeaderText, currentPlanet.Name);
            _populationText.text = string.Format(_defaultPopulationText, currentPlanet.Population);
            _levelText.text = string.Format(_defaultLevelText, currentPlanet.Level, currentPlanet.MaxLevel);
            _incomeText.text = string.Format(_defaultIncomeText, currentPlanet.Income);
            _upgradePriceText.text = string.Format(_defaultUpgradePriceText, currentPlanet.UpgradePrice);
        }
    }
}