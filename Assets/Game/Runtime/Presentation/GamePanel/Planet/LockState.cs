using System;
using TMPro;
using UnityEngine;

namespace Game.Runtime.Presentation.GamePanel.Planet
{
    [Serializable]
    public class LockState
    {
        [field: SerializeField] public GameObject MainBody { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CostText { get; private set; }
    }
}