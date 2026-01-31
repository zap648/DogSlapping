using UnityEngine;

public class FireFly : MonoBehaviour
{

    float sideMovement;
    float upDownMovement;
    float random;
    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.time * Time.deltaTime;
        if (timer > 1)
        {
            random = -1;

            if (timer > 2) timer = 0;
        }
        else
        {
            random = 1;
            timer = 0;
        }

        
        Move();

    }

    void Move()
    {
        sideMovement = Mathf.Cos(Time.time + .1f) * .001f;
        upDownMovement = Mathf.Sin(Time.time + .1f) * .001f;

       // Debug.Log(random + ", " + timer);
        transform.position = new Vector3(transform.position.x + (sideMovement * random), transform.position.y + upDownMovement, transform.position.z);
    }
}
