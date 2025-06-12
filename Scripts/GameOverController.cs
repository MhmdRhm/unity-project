using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void OnGameOverAnimationComplete() {
        SceneManager.LoadScene(0);
    }
}
