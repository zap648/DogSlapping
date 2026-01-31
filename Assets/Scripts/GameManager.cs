using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int score;
    [SerializeField] private int highScore;
    private GameObject dog;
    [SerializeField] private List<GameObject> dogs;
    [SerializeField] private GameObject slapThingy;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private AudioSource slapSound;
    [SerializeField] private bool gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slapThingy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (startMenu.activeSelf || deathMenu.activeSelf) //
        {
            return;
        }

        if (dog == null)
        {
            int index = Random.Range(0, dogs.Count);
            dog = Instantiate(dogs[index], new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Quaternion.identity);
            dog.transform.position = dog.transform.position.normalized * 15;
        }
        else if (Input.anyKeyDown)
        {
            if (gameOver)
            {
                gameOver = false;
                Destroy(dog);
                return;
            }
            
            Slap();
        }
    }

    private void Slap()
    {
        StartCoroutine(SlapThing(0.1f));

        if (!dog.GetComponent<Dog>().Slapped())
        {
            return;
        }
        if (!dog.GetComponent<Dog>().isLeaving)
        {
            GameOver();
            return;
        }
        
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

    IEnumerator SlapThing(float time)
    {
        slapThingy.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));
        slapThingy.SetActive(true);
        yield return new WaitForSeconds(time);
        slapThingy.SetActive(false);
    }
}
