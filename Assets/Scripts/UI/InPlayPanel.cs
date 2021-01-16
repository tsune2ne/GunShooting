﻿using UnityEngine;
using UnityEngine.UI;

namespace GunShooting.UI
{
    public class InPlayPanel : MonoBehaviour
    {
        [SerializeField] Text ScoreText;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateScore(int score)
        {
            ScoreText.text = score.ToString();
        }
    }
}