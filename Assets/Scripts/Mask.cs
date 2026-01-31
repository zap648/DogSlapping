using UnityEngine;

public class Mask : MonoBehaviour
{
    public Rigidbody2D rb;


    private float slapDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slapDirection = Random.Range(-10, 10);
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(transform.right * 40f * slapDirection);
        rb.AddForce(transform.up * 150f);

        rb.angularVelocity += 500f;

        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.rotation += 2f;
    }
}
