using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool isAirborn;
    [SerializeField] private GameObject model;
    [SerializeField] private Animator anim;

    private void Start()
    {
        movementVector = transform.position;
    }

    private void Update()
    {
        CheckInputs();
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.CurrentStatus != GameStatus.Idle)
            return;
        
        Move();
    }
    
    private void Move()
    {
        movementVector = Vector3.zero;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movementVector.x += horizontal;
        movementVector.z += vertical;

        var position = transform.position;
        position = Vector3.Lerp(position, position + movementVector, speed * Time.deltaTime);
        
        transform.position = position;

        if (movementVector != Vector3.zero)
        {
            model.transform.LookAt(transform.position + movementVector);
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    private void CheckInputs()
    {
        if (isAirborn && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, 200,0));
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isAirborn = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        isAirborn = true;
    }

    private void OnCollisionStay(Collision other)
    {
        isAirborn = true;
    }
}
