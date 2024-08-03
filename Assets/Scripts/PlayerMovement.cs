using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float ForwardForce = 100f;
    public float SidewaysForce = 100f;
    
    private Rigidbody _rigidbody;

    private float _horizontalInput;
    private float startY;

    private AudioSource _audioSource;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        startY = transform.position.y;
    }

    void Update()
    {
        if(!GameManager.Instance.IsRunning)
            return;

        if (Input.GetAxis("Horizontal") < 0)
        {
            _horizontalInput = -1;
        }
        else if (Input.GetAxis("Horizontal") > 0f)
        {
            _horizontalInput = 1;
        }
        else
        {
            _horizontalInput = 0;
        }

        _horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < startY - 0.3f)
            GameManager.Instance.EndGame();

        if (!GameManager.Instance.IsGameEnded)
        {
            var euler = transform.rotation.eulerAngles;
            euler.y = 0;
            transform.rotation = Quaternion.Euler(euler);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameManager.Instance.IsGameEnded)
            _rigidbody.AddForce(_horizontalInput * SidewaysForce * Time.fixedDeltaTime, 0, 0, ForceMode.VelocityChange);     
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle" && !GameManager.Instance.IsGameEnded)
        {
            _audioSource.Play();
            GameManager.Instance.EndGame(false);
            _rigidbody.AddForce(0, 0, ForwardForce, ForceMode.VelocityChange);
        }
    }
}
