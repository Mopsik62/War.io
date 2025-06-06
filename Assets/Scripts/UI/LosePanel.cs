using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace War.io
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;
        private void Start()
        {
            _gameManager.Loss += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }



    }



}
