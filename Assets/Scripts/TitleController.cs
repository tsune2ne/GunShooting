using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void OnClickGameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
