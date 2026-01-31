using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject loseCanvas;
    public GameObject gameCanvas;
    public TMP_Text scoreText;
    public GameObject[] gameBackgrounds;
    int score;

    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    public AudioClip loseMusic;

    public AudioSource audioSource;

    private int level = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TriggerButton();
        ShowBackground();
        if (gameCanvas.activeSelf) UpdateScoreText();
        score = GameObject.Find("GameManager").GetComponent<GameManager>().score;

        //Change the level after a certain score has been obtained.
        if (score == 10) level = 1;
        if (score == 30) level = 2;
        if (score == 60) level = 3;
        if (score == 100) level = 4;
    }

    void TriggerButton()
    {
        if (Input.anyKeyDown && startCanvas.activeSelf)
        {
            StartGame();
            CameraShake();
        }
        //else if (Input.anyKeyDown && !startCanvas.activeSelf) LoseGame();
    }

    public void StartGame()
    {
        Debug.Log("Clicked button");
        startCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        level = 0;
        audioSource.clip = gameMusic;
        audioSource.Play();
    }

    public void LoseGame()
    {
        Debug.Log("You are murderer");
        loseCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        audioSource.clip = loseMusic;
        audioSource.Play();
    }

    public void MainMenu()
    {
        startCanvas.SetActive(true);
        loseCanvas.SetActive(false);
        Debug.Log("main menu");
        audioSource.clip = mainMenuMusic;
        audioSource.Play();

    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
        Debug.Log(score);
    }

    void ShowBackground()
    {
        foreach (GameObject background in gameBackgrounds)
        {
            if (System.Array.IndexOf(gameBackgrounds, background) == level)
            {
                background.SetActive(true);
            }
            else background.SetActive(false);
        }
    }

    void CameraShake()
    {
        Camera.main.fieldOfView += 10;
        Debug.Log("shakek");
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z)
    }
}
