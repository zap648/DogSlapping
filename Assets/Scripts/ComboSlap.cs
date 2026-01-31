using UnityEngine;

public class ComboSlap : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayComboSlap()
    {
        Debug.Log("Superslap2!");
        gameManager.Slap();
        gameObject.SetActive(true);
    }
}
