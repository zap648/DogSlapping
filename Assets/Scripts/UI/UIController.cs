using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject startCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TriggerButton();
    }

    void TriggerButton()
    {
        if (Input.anyKeyDown && startCanvas.activeSelf)
        {
            StartGame();
        }
        else if (Input.anyKeyDown && !startCanvas.activeSelf) LoseGame();
    }

    public void StartGame()
    {
        Debug.Log("Clicked button");
        startCanvas.SetActive(false);
    }

    public void LoseGame()
    {
        Debug.Log("You are murderer");
        startCanvas.SetActive(true);
    }
}
