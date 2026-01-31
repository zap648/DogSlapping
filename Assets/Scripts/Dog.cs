using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private bool isSad;
    [SerializeField] private bool isEntering = true;
    public bool isLeaving = false;
    [SerializeField] private bool slappable = false;
    public float slapTimer = 3.0f;
    private float elapsedTime;
    private float waitingTime;
    public float timeMultiplier = 1.0f;
    private Vector2 startPosition;
    private Vector2 endPosition = Vector2.zero;

    public Sprite SadMask;
    public Sprite HappyMask;

    public SpriteRenderer maskRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        isSad = Random.value < 0.7f;
        if (isSad)
        {
            // Set sad dog sprite or animation here
            GetComponent<SpriteRenderer>().color = Color.blue; // Example: change color to blue for sad dog
            maskRenderer.sprite = SadMask;

        }
        else
        {
            // Set happy dog sprite or animation here
            GetComponent<SpriteRenderer>().color = Color.yellow; // Example: change color to yellow for happy dog
            maskRenderer.sprite = HappyMask;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEntering)
            MoveIn();
        else if (isLeaving)
            MoveOut();

        if (slappable)
        {
            // Optional: Add some visual indication that the dog can be slapped
            waitingTime += Time.deltaTime;
            if (waitingTime >= slapTimer)
            {
                isLeaving = true;
                slappable = false;
                waitingTime = 0.0f;
            }
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
        slappable = true;
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
            Destroy(gameObject);
            return;
        }
        transform.position = Vector2.Lerp(endPosition, startPosition, elapsedTime);
        elapsedTime += timeMultiplier * Time.deltaTime;

        if (isSad) { GetComponent<SpriteRenderer>().sprite = null;
            maskRenderer.sprite = null;
        }

        
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
