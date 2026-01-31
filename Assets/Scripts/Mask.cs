using UnityEngine;

public class Masks : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(-transform.right * 100f);
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
