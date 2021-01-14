using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GunShooting
{
    public class Controller : MonoBehaviour
    {
        const string PlayerPrefab = "Prefabs/Player";

        [SerializeField] Enemy[] Enemies;
        [SerializeField] Text ScoreText;
        [SerializeField] GameObject PlayerSpawnPoint;
        Player player;

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

            var obj = (GameObject)Resources.Load(PlayerPrefab);
            var instance = Instantiate(obj, PlayerSpawnPoint.transform.position, Quaternion.identity);
            player = instance.GetComponent<Player>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.Shoot();
            }

            var rotateX = Input.GetAxis("Mouse X");
            var rotateY = Input.GetAxis("Mouse Y");
            if (Mathf.Abs(rotateX + rotateY) > 0)
            {
                player.Rotate(rotateX, rotateY);
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
