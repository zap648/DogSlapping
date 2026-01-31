using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject loseCanvas;
    public TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TriggerButton();
        UpdateScoreText();
    }

    void TriggerButton()
    {
        if (Input.anyKeyDown && startCanvas.activeSelf)
        {
            StartGame();
        }
        //else if (Input.anyKeyDown && !startCanvas.activeSelf) LoseGame();
    }

    public void StartGame()
    {
        Debug.Log("Clicked button");
        startCanvas.SetActive(false);
    }

    public void LoseGame()
    {
        Debug.Log("You are murderer");
        loseCanvas.SetActive(true);
    }

    public void MainMenu()
    {
        startCanvas.SetActive(true);
        loseCanvas.SetActive(false);
        Debug.Log("main menu");

    }

    void UpdateScoreText()
    {
        scoreText = GameObject.Find("GameManager").GetComponent<GameManager>().score;
        Debug.Log(scoreText);
    }
}
