using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 movementVector;

    private void Start()
    {
        movementVector = transform.position;
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.CurrentStatus != GameStatus.Idle)
            return;
        
        movementVector = Vector3.zero;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movementVector.x += horizontal;
        movementVector.z += vertical;

        var position = transform.position;
        position = Vector3.Lerp(position, position + movementVector, speed * Time.deltaTime);
        
        transform.position = position;
    }
}
