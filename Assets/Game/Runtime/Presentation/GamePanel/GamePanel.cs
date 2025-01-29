using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Infrastructure.Panels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Game.Runtime.Presentation.GamePanel.Planet;

namespace Game.Runtime.Presentation.GamePanel
{
    public class GamePanel : PanelBase
    {
        [SerializeField] private List<PlanetView> _planetViews = new();

#if UNITY_EDITOR
        [ContextMenu("Find Planet Views")]
        private void FindPlanetViews()
        {
            _planetViews.Clear();
            _planetViews.AddRange(GetComponentsInChildren<PlanetView>());
        }
#endif
    }
}