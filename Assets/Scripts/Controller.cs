using UnityEngine;
using UnityEngine.SceneManagement;
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
        const float TimerLimit = 10f;

        [SerializeField] Enemy[] Enemies;
        [SerializeField] Text ScoreText;
        [SerializeField] Text TimerText;
        [SerializeField] GameObject PlayerSpawnPoint;
        [SerializeField] UIController UIController;
        GameState currentState;
        Player player;
        int deadEnemyCount = 0;
        float timerCount = 0f;

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
            instance.transform.parent = PlayerSpawnPoint.transform;
            player = instance.GetComponent<Player>();

            // TODO back to title scene
            UIController.onBackToTitle = () =>
            {
                SceneManager.LoadScene("TitleScene");
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
            timerCount += Time.deltaTime;
            TimerText.text = Mathf.Ceil(TimerLimit - timerCount).ToString();
            if (timerCount >= TimerLimit)
            {
                Finish();
            }

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
