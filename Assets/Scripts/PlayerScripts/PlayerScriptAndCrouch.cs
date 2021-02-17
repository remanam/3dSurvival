using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;

    private bool is_Crouching;

    private PlayerFootSteps playerFootSteps;

    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

    private float walk_Step_Distance = 0.5f;
    private float sprint_Step_Distance = 0.3f;
    private float crouch_Step_Distance = 0.65f;

    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        look_Root = transform.GetChild(0);

        playerFootSteps = GetComponentInChildren<PlayerFootSteps>();
    }

    private void Start()
    {
        playerFootSteps.volume_Min = walk_Volume_Min;
        playerFootSteps.volume_Max = walk_Volume_Max;
        playerFootSteps.step_Distance = walk_Step_Distance;
    }

    private void Update()
    {
        Sprint();
        Crouch();
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C)) {

            // if we are crouching, then player stands up
            if (is_Crouching == true) {

                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.speed = move_Speed;

                // Walk Footsteps
                playerFootSteps.volume_Min = walk_Volume_Min;
                playerFootSteps.volume_Max = walk_Volume_Max;
                playerFootSteps.step_Distance = walk_Step_Distance;

                is_Crouching = false;
            }
            //if we are not crouching, the player starts to crouch
            else {

                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;

                //Crouch FootSteps
                playerFootSteps.volume_Min = crouch_Volume;
                playerFootSteps.volume_Max = crouch_Volume;
                playerFootSteps.step_Distance = crouch_Step_Distance;

                is_Crouching = true;

            }

        }// if we press "c"
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && is_Crouching == false) {
            playerMovement.speed = sprint_Speed;

            //Sprint Footsteps
            playerFootSteps.step_Distance = sprint_Step_Distance;
            playerFootSteps.volume_Min = sprint_Volume;
            playerFootSteps.volume_Max = sprint_Volume;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && is_Crouching == false) {
            playerMovement.speed = move_Speed;

            // Back to walk Footsteps
            playerFootSteps.volume_Min = walk_Volume_Min;
            playerFootSteps.volume_Max = walk_Volume_Max;
            playerFootSteps.step_Distance = walk_Step_Distance;


        }
    }
}
