using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private bool isSad;
    [SerializeField] private bool isEntering = true;
    [SerializeField] private bool isLeaving = false;
    private float elapsedTime;
    private Vector2 startPosition;
    private Vector2 endPosition = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEntering)
            MoveIn();
        else if (isLeaving)
            MoveOut();
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
        elapsedTime += Time.deltaTime;
    }

    private void MoveOut()
    {
        // Movement logic can be added here if needed
        if (elapsedTime >= 1.0f)
        {
            elapsedTime = 0.0f;
            transform.position = startPosition;
            isLeaving = false;
            return;
        }
        transform.position = Vector2.Lerp(endPosition, startPosition, elapsedTime);
        elapsedTime += Time.deltaTime;
    }

    public void Slapped()
    {

    }
}
