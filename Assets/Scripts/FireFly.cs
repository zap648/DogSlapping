using UnityEngine;

public class FireFly : MonoBehaviour
{

    private float sideMovement;
    private float upDownMovement;
    private float random;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        random = Random.Range(0, 1);
        Move();

    }

    void Move()
    {
        sideMovement = Mathf.Cos(Time.time + .1f) * .001f;
        upDownMovement = Mathf.Sin(Time.time + .1f) * .001f;

        Debug.Log(sideMovement);
        if (random == 0) transform.position = new Vector3(transform.position.x + sideMovement, transform.position.y + upDownMovement, transform.position.z);
        if (random == 1) transform.position = new Vector3(transform.position.x - sideMovement, transform.position.y - upDownMovement, transform.position.z);
    }
}
