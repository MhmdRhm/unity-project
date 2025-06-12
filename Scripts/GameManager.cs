using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject objectiveText;
    public GameObject destroyedBarrelsText;
    public GameObject countText;
    public GameObject closeObj;
    public Image[] heartImages;
    public float secondPhaseYPos = 3.4f;
    public float thirdPhaseYPos = 9.5f;
    private int lives;
    private int destroyedBarrels = 3;
    private GameObject[] ladders;
    private Color defaultLadderColor;

    private void Awake()
    {
        ladders = GameObject.FindGameObjectsWithTag("Ladder");
        defaultLadderColor = ladders[0].GetComponent<SpriteRenderer>().color;
    }

    private void Start()
    {
        Cursor.visible = false;
        NewGame();
        Invoke(nameof(ActivateUIElements), 9);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (player.transform.position.y >= secondPhaseYPos)
        {
            Barrel_Spawner.secondPhase = true;
        }

        if (player.transform.position.y >= thirdPhaseYPos)
        {
            Fireball_Spawner.thirdPhase = true;
        }
    }

    private void NewGame()
    {
        lives = 3;
    }

    public void Complete()
    {
        float currentBestTime = Time.timeSinceLevelLoad;
        float previousBestTime = PlayerPrefs.GetFloat("BestTime");
        if (previousBestTime == 0 || currentBestTime <= previousBestTime)
            PlayerPrefs.SetFloat("BestTime", currentBestTime);
        AudioManager.Instance.PlayCreditsClip();
        SceneManager.LoadScene(3);
    }

    public void Hit()
    {
        lives--;
        if (lives <= 0)
        {
            AudioManager.Instance.PlayGameOverClip();
            SceneManager.LoadScene(2);
        }
        else
        {
            heartImages[lives].gameObject.SetActive(false);
        }
    }

    public void ActivateUIElements()
    {
        player.GetComponent<PlayerController>().enabled = true;
        for (int i = 0; i < heartImages.Length; i++)
            heartImages[i].gameObject.SetActive(true);
        objectiveText.SetActive(true);
    }

    public int DecrementDestoyredBarrels()
    {
        destroyedBarrels--;
        countText.GetComponent<Text>().text = destroyedBarrels.ToString();
        return destroyedBarrels;
    }

    public void DeactivateObjectiveBarrelText()
    {
        destroyedBarrelsText.SetActive(false);
    }

    public void ActivateObjectiveBarrelText()
    {
        destroyedBarrelsText.SetActive(true);
    }

    public void Zoom()
    {
        closeObj.GetComponent<Animator>().SetTrigger("Finish");
    }

    public void SetLaddersToGray()
    {
        foreach (GameObject ladder in ladders)
        {
            ladder.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
        }
    }
    
    public void ResetLadderColors()
    {
        foreach (GameObject ladder in ladders)
        {
            ladder.GetComponent<SpriteRenderer>().color = defaultLadderColor;
        }
    }
}
