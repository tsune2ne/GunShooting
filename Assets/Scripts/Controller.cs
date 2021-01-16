using UnityEngine;
using UnityEngine.UI;

namespace GunShooting
{
    enum GameState
    {
        Play,
        Finish
    }

    public class Controller : MonoBehaviour
    {
        const string PlayerPrefab = "Prefabs/Player";

        [SerializeField] Enemy[] Enemies;
        [SerializeField] Text ScoreText;
        [SerializeField] GameObject PlayerSpawnPoint;
        [SerializeField] UIController UIController;
        GameState currentState;
        Player player;
        int deadEnemyCount = 0;

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
            currentState = GameState.Play;

            // マウスカーソルロック
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            foreach (var enemy in Enemies)
            {
                enemy.onDead = OnEnemyDead;
            }

            var obj = (GameObject)Resources.Load(PlayerPrefab);
            var instance = Instantiate(obj, PlayerSpawnPoint.transform.position, Quaternion.identity);
            player = instance.GetComponent<Player>();

            // TODO back to title scene
            UIController.onBackToTitle = () =>
            {
                Debug.Log("back to title");
            };
            UIController.GameStart();
        }

        void Finish()
        {
            currentState = GameState.Finish;

            // マウスカーソルロック解除
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            UIController.GameFinish();
        }

        void Update()
        {
            if (currentState == GameState.Play)
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
        }

        void OnEnemyDead(int score)
        {
            Score = Score + score;

            deadEnemyCount++;
            if (deadEnemyCount == Enemies.Length)
            {
                OnAllEnemyDead();
            }
        }

        void OnAllEnemyDead()
        {
            Finish();
        }

        void OnUpdateScore(int score)
        {
            UIController.UpdateInPlayPanel(score);
        }
    }
}
