using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
    [Header("Game Stats")]
    [SerializeField] public int score;
    [SerializeField] private int highScore;
    [SerializeField] public int combo;
    [SerializeField] public float slapTimer = 2.0f;
    [SerializeField] private float slapCooldown = 0.5f;
    public float slapTimerMax;
    private float slapCooldownMax;

    [Header("Dog Management")]
    [SerializeField] private List<GameObject> dogs;
    public GameObject dog;
    [SerializeField] private GameObject slapThingy;
    [SerializeField] private GameObject dagNiceThingy;
    [SerializeField] private GameObject comboThingy;

    [Header("UI Elements")]
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private AudioSource slapSound;
    [SerializeField] public AudioClip smallSlapSound;
    [SerializeField] public AudioClip comboSlapSound;

    [Header("Game State")]
    [SerializeField] public bool gameOver;
    [SerializeField] public bool highPriAnimation;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slapSound.clip = smallSlapSound;
        slapThingy.SetActive(false);
        slapTimerMax = slapTimer;
        slapCooldownMax = slapCooldown;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (highPriAnimation) // If there's a high priority animation
        {
            return;
        }

        if (startMenu.activeSelf || deathMenu.activeSelf) // If the menus are active, this will prevent the game from running
        {
            return;
        }

        if (dog == null)
        {
            int index = Random.Range(0, dogs.Count);
            dog = Instantiate(dogs[index], new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Quaternion.identity);
            dog.transform.position = dog.transform.position.normalized * 15;
            dog.GetComponent<Dog>().timeMultiplier = slapTimerMax / slapTimer;
        }
        else if (Input.anyKeyDown && slapCooldown < 0.0f)
        {
            if (combo % 20 == 19 && combo != 0)
            {
                slapSound.clip = comboSlapSound;
                slapSound.Play();
                ComboSlap();

            }
            else
            {
                slapSound.Play();
                Slap();
            }

            if (combo % 5 == 0 && combo != 0 && combo % 20 != 0)
            {
                DagApproves();
            }
        }
        SpeedUp();

        slapCooldown -= Time.deltaTime;
    }

    public void Slap()
    {

        if (gameOver)
        {
            return;
        }

        StartCoroutine(SlapThing(slapCooldownMax));

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

        score++;
        combo++;
        if (score > highScore)
            highScore = score;

        slapCooldown = slapCooldownMax;
    }

    void DagApproves()
    {
        Debug.Log("Dag Approves!");
        StartCoroutine(DagThing(1.0f));
    }

    void ComboSlap()
    {
        Debug.Log("Combo Slap!");
        StartCoroutine(ComboThing(2.0f));
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
        slapThingy.SetActive(true);
        yield return new WaitForSeconds(time);
        slapThingy.SetActive(false);
    }

    IEnumerator DagThing(float time)
    {
        Debug.Log("Dag Approves!");
        dagNiceThingy.SetActive(true);
        yield return new WaitForSeconds(time);
        dagNiceThingy.SetActive(false);
    }

    IEnumerator ComboThing(float time)
    {
        Debug.Log("Super Slap!");
        highPriAnimation = true;
        comboThingy.SetActive(true);
        yield return new WaitForSeconds(time);
        slapSound.clip = smallSlapSound;
        comboThingy.SetActive(false);
        highPriAnimation = false;
    }
}
