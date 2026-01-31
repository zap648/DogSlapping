using UnityEngine;

public class Slap : MonoBehaviour
{
    Animator animator;
    public float speed = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = speed; // Set animation speed to 2x
    }

    // Update is called once per frame
    void Update()
    {

    }
}
