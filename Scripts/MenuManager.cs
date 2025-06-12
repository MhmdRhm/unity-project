using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject bsTimeHolder;
    public GameObject bsTimeUI;

    private void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
        Cursor.visible = true;
        float bsTime = PlayerPrefs.GetFloat("BestTime");
        Debug.Log(bsTime);
        if (bsTime != 0)
        {
            bsTimeHolder.SetActive(true);
            bsTimeUI.SetActive(true);
            bsTimeHolder.GetComponent<Text>().text = bsTime.ToString("F2");
        }
    }

    public void StartGame() {
        AudioManager.Instance.PlayBackgroundMusic();
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }
}
