using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.GamePanel.Planet
{
    [Serializable]
    public class OpenState
    {
        [field: SerializeField] public GameObject MainBody { get; private set; }
        [field: SerializeField] public GameObject CoinImage { get; private set; }
        [field: SerializeField] public GameObject ProgressBar { get; private set; }
        [field: SerializeField] public Image ProgressBarFillImage { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ProgressTimeText { get; private set; }
    }
}