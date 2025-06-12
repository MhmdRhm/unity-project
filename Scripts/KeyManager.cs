using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{
    private void Update() {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }
}
