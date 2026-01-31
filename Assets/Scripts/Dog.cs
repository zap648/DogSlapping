using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private bool isSad;
    [SerializeField] private bool isEntering = true;
    public bool isLeaving = false;
    [SerializeField] private bool slappable = true;
    private float elapsedTime;
    public float timeMultiplier = 1.0f;
    private Vector2 startPosition;
    private Vector2 endPosition = Vector2.zero;

    public Sprite SadMask;
    public Sprite HappyMask;

    public SpriteRenderer maskRenderer;

    public GameObject mask;
    public GameObject slappedAnimal;

    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startPosition = transform.position;
        isSad = Random.value < 0.7f;
        if (isSad)
        {
            // Set sad dog sprite or animation here
            //GetComponent<SpriteRenderer>().color = Color.blue; // Example: change color to blue for sad dog
            maskRenderer.sprite = SadMask;

        }
        else
        {
            // Set happy dog sprite or animation here
            //GetComponent<SpriteRenderer>().color = Color.yellow; // Example: change color to yellow for happy dog
            maskRenderer.sprite = HappyMask;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.highPriAnimation)
            return;

        if (isEntering)
            MoveIn();
        else if (isLeaving)
            MoveOut();

        if (transform.position.x > 9.0f || transform.position.x < -9.0f ||
            transform.position.y > 5.0f || transform.position.y < -5.0f)
        {
            slappable = false;
        }
        else if (maskRenderer.sprite != null)
        {
            slappable = true;
        }

        if (!isEntering)
        {
            isLeaving = true;
        }
    }

    private void MoveIn()
    {
        // Movement logic can be added here if needed
        if (elapsedTime >= 1.0f)
        {
            elapsedTime = 0.0f;
            transform.position = endPosition;
            isEntering = false;
            startPosition = -startPosition;
            return;
        }
        transform.position = Vector2.Lerp(startPosition, endPosition, elapsedTime);
        elapsedTime += timeMultiplier * Time.deltaTime;
    }

    private void MoveOut()
    {
        // Movement logic can be added here if needed
        if (elapsedTime >= 1.0f)
        {
            elapsedTime = 0.0f;
            transform.position = startPosition;
            isLeaving = false;
            if (isSad && maskRenderer.sprite != null)
            {
                gameManager.combo = 0;
            }
            Destroy(gameObject);
            return;
        }
        transform.position = Vector2.Lerp(endPosition, startPosition, elapsedTime);
        elapsedTime += timeMultiplier * Time.deltaTime;        
    }

    public bool Slapped()
    {
        if (!slappable)
        {
            return false;
        }

        if (isSad)
        {
            GetComponent<SpriteRenderer>().sprite = null;
            maskRenderer.sprite = null;

            Instantiate(mask);
            Instantiate(slappedAnimal);
        }

        if (isSad)
        {
            isLeaving = true;
            slappable = false;
            return true;
        }
        else
        {
            isLeaving = false;
            slappable = false;
            return true;
        }
    }
}
