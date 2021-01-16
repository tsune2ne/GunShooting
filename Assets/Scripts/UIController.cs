using UnityEngine;
using GunShooting.UI;
using System;

namespace GunShooting
{ 
    public class UIController : MonoBehaviour
    {
        [SerializeField] InPlayPanel InPlayPanel;
        [SerializeField] InFinishPanel InFinishPanel;

        public Action onBackToTitle;

        public void GameStart()
        {
            InPlayPanel.Show();
            InFinishPanel.Hide();
        }

        public void GameFinish()
        {
            InPlayPanel.Hide();
            InFinishPanel.Show();
        }

        public void UpdateInPlayPanel(int score)
        {
            InPlayPanel.UpdateScore(score);
            InFinishPanel.UpdateScore(score);
        }

        public void OnClickBackToTitleButton()
        {
            if (onBackToTitle != null)
            {
                onBackToTitle.Invoke();
            }
        }
    }
}
