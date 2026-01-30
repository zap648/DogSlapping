using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int highScore;
    [SerializeField] private Dog dog;
    [SerializeField] private float timer = 3.0f;
    [SerializeField] private GameObject slapThingy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slapThingy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Slap();
            if (score > highScore)
            {
                highScore = score;
            }
            Debug.Log($"Score: {score}, High Score: {highScore}");
        }
    }

    private void Slap()
    {
        dog.Slapped();
        StartCoroutine(SlapThing(0.1f));
        score++;
    }

    IEnumerator SlapThing(float time)
    {
        slapThingy.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));
        slapThingy.SetActive(true);
        yield return new WaitForSeconds(time);
        slapThingy.SetActive(false);
    }
}
