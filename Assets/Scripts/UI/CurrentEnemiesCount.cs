using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace War.io
{
    public class CurrentEnemiesCount : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _outputText;
        public void SetEnemies(int enemies)
        {
            _outputText.text = enemies.ToString();
        }
    }




}

