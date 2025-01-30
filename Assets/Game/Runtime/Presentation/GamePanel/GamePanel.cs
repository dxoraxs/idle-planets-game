using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Runtime.Infrastructure.Panels;
using TMPro;
using UnityEngine;
using EditorAttributes;
using UnityEngine.UI;
using Game.Runtime.Presentation.GamePanel.Planet;
using UnityEditor.Presets;

namespace Game.Runtime.Presentation.GamePanel
{
    public class GamePanel : PanelBase
    {
        [SerializeField] private PlanetView[] _planetViews;
        private readonly Dictionary<string, PlanetView> _planetViewDictionary = new();
        private IGamePanelPresenter _presenter;

        public void SetPresenter(IGamePanelPresenter presenter)
        {
            _presenter = presenter;

            _presenter.PlanetViewUpdated += UpdatedPlanetView;
            InitializePlanetViews();
        }

        private void InitializePlanetViews()
        {
            var planets = _presenter.GetAllPlanets();
            for (var i = 0; i < planets.Count && i < _planetViews.Length; i++)
            {
                var view = _planetViews[i];
                var planetData = planets[i];
                _planetViewDictionary.Add(planetData.Id, view);
                
                view.PlanetButton.onClick.AddListener(() => OnClickPlanet(planetData.Id));
                view.OpenVisual.CoinButton.onClick.AddListener(() => OnClickCoinPlanet(planetData.Id));

                SetUpdatePlanetView(planetData);
            }
        }

        private void OnClickPlanet(string id)
        {
            _presenter.OnClickPlanet(id);
        }

        private void OnClickCoinPlanet(string id)
        {
            _presenter.OnClickPlanetCoin(id);
        }

        private void UpdatedPlanetView(string id)
        {
            var planetViewData = _presenter.GetPlanet(id);
            SetUpdatePlanetView(planetViewData);
        }

        private void SetUpdatePlanetView(PlanetViewData planet)
        {
            var view = _planetViewDictionary[planet.Id];
            view.PlanetIcon.sprite = planet.Icon;
            view.LockVisual.MainBody.SetActive(!planet.IsOpen);
            view.OpenVisual.MainBody.SetActive(planet.IsOpen);

            if (planet.IsOpen)
            {
                if (planet.ProgressBarValue >= 1)
                {
                    view.OpenVisual.CoinImage.SetActive(true);
                    view.OpenVisual.ProgressBar.SetActive(false);
                }
                else
                {
                    view.OpenVisual.CoinImage.SetActive(false);
                    view.OpenVisual.ProgressBar.SetActive(true);
                    view.OpenVisual.ProgressBarFillImage.fillAmount = planet.ProgressBarValue;
                    view.OpenVisual.ProgressTimeText.text = planet.ProgressText;
                }
            }
            else
            {
                view.LockVisual.CostText.text = planet.CostOpen.ToString();
            }
        }

#if UNITY_EDITOR
        [Button]
        private void FindPlanetViews()
        {
            _planetViews = GetComponentsInChildren<PlanetView>();
        }
#endif
    }
}