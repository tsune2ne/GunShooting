using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GunShooting
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] Player Player;
        [SerializeField] Enemy[] Enemies;
        [SerializeField] Text ScoreText;

        int _score = 0;
        int Score
        {
            set { 
                _score = value;
                OnUpdateScore(_score);
            }
            get { return _score;  }
        }

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            foreach (var enemy in Enemies)
            {
                enemy.onDead = OnEnemyDead;
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Player.Shoot();
            }

            var rotateX = Input.GetAxis("Mouse X");
            var rotateY = Input.GetAxis("Mouse Y");
            if (Mathf.Abs(rotateX + rotateY) > 0)
            {
                Player.Rotate(rotateX, rotateY);
            }
        }

        void OnEnemyDead(int score)
        {
            Score += score;
        }

        void OnUpdateScore(int score)
        {
            ScoreText.text = score.ToString();
        }
    }
}
