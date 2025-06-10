using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace War.io
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private AudioSource _winAudio;

        private void Start()
        {
            _gameManager.Win += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
            _winAudio.Play();
        }




    }








}


