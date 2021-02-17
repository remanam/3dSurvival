using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 move_Direction;

    public float speed = 5f;

#pragma warning disable 0414
    private float gravity = 20f;
#pragma warning restore 0414

    public float jump_Force = 10f;
    private float vertical_Velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MoveThePlayer();
    }

    private void MoveThePlayer()
    {
        //Получаем велиину, на которую надо перенести игрока
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        // Playey Controller используется без Rigidbody, поэтому гравитацию прикладываем мы сами
        ApplyGravity();

        characterController.Move(move_Direction);
    }

    void ApplyGravity()
    {
        if (characterController.isGrounded) {

            vertical_Velocity -= gravity * Time.deltaTime;

            //jump
            PlayerJump();
        }
        else {
            vertical_Velocity -= gravity * Time.deltaTime;
        }

        move_Direction.y = vertical_Velocity * Time.deltaTime;
        
    }

    private void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_Velocity = jump_Force;
        }
    }
}
