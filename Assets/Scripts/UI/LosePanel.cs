using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace War.io
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;


        [SerializeField]
        private AudioSource _loseAudio;

        private void Start()
        {
            _gameManager.Loss += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
            _loseAudio.Play();
        }



    }



}
