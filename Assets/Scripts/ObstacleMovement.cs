using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float Force = 70;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.IsGameEnded)
            rb.velocity = new Vector3(0, 0, 0);
        else
            rb.AddForce(0, 0, -Force * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
            Destroy(gameObject);
    }
}
