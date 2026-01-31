using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class GameManager : MonoBehaviour
{
    [Header("Game Stats")]
    [SerializeField] public int score;
    [SerializeField] private int highScore;
    [SerializeField] private int combo;
    [SerializeField] private float slapTimer = 2.0f;
    [SerializeField] private float slapCoolDown = 0.5f;
    private float slapTimerMax;
    [Header("Dog Management")]
    [SerializeField] private List<GameObject> dogs;
    private GameObject dog;
    [SerializeField] private GameObject slapThingy;
    [Header("UI Elements")]
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private AudioSource slapSound;
    [Header("Game State")]
    [SerializeField] private bool gameOver;

    public GameObject mask;
    public GameObject slappedAnimal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slapThingy.SetActive(false);
        slapTimerMax = slapTimer;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMenu.activeSelf || deathMenu.activeSelf) // If the menus are active, this will prevent the game from running
        {
            return;
        }

        if (dog == null)
        {
            int index = Random.Range(0, dogs.Count);
            dog = Instantiate(dogs[index], new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Quaternion.identity);
            dog.transform.position = dog.transform.position.normalized * 15;
            dog.GetComponent<Dog>().slapTimer = slapTimer;
            dog.GetComponent<Dog>().timeMultiplier = slapTimerMax / slapTimer;
        }
        else if (Input.anyKeyDown)
        {
            if (gameOver)
            {
                slapTimer = slapTimerMax;
                gameOver = false;
                Destroy(dog);
                return;
            }
            
            Slap();
        }
        SpeedUp();
    }

    private void Slap()
    {
        StartCoroutine(SlapThing(0.5f));

        if (!dog.GetComponent<Dog>().Slapped())
        {
            combo = 0;
            return;
        }
        if (!dog.GetComponent<Dog>().isLeaving)
        {
            combo = 0;
            GameOver();
            return;
        }

        combo++;
        Instantiate(mask);
        Instantiate(slappedAnimal);

        score++;
        if (score > highScore)
            highScore = score;
    }

    private void GameOver()
    {
        score = 0;
        gameOver = true;
        Destroy(dog); //
        Camera.main.GetComponent<UIController>().LoseGame();
        Debug.Log("Game Over!");
    }

    private void SpeedUp()
    {
        slapTimer -= 0.01f * Time.deltaTime;
        if (slapTimer < 0.5f)
            slapTimer = 0.5f;
    }

    IEnumerator SlapThing(float time)
    {
        slapThingy.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));
        slapThingy.SetActive(true);
        yield return new WaitForSeconds(time);
        slapThingy.SetActive(false);
    }
}
