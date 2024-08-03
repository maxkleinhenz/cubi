using UnityEngine;

public class MenuObstacleMovement : MonoBehaviour
{
    public float force = -300f;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, force * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
