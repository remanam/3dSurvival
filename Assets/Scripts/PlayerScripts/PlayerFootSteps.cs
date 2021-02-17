using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource footstep_Sound;

    [SerializeField] 
    private AudioClip[] footstep_Clip;

    private CharacterController characterController;

    [HideInInspector] //We only wanna edit them via script 
    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance; // Determine how far we can go if we are moveing, sprinting, crouching
    void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();

        characterController = GetComponentInParent<CharacterController>();
    }

    
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    private void CheckToPlayFootstepSound()
    {
        // if we are NOT on the ground
        if (characterController.isGrounded == false)
            return;

        // ≈сли длинна вектор3  положительна€, то включаем footstep (она считатс€ по модулю, может быть только положительна€)
        if (characterController.velocity.sqrMagnitude > 0) {
            accumulated_Distance += Time.deltaTime;

            // „тоб звук игралс€ с определЄнной частотой, завис€щей от изменени€ позиции
            if (accumulated_Distance > step_Distance) {
                footstep_Sound.volume = UnityEngine.Random.Range(volume_Min, volume_Max); // random sound volume
                footstep_Sound.clip = footstep_Clip[UnityEngine.Random.Range(0, footstep_Clip.Length)]; // random clip
                footstep_Sound.Play();

                accumulated_Distance = 0f;
            }
        }
        else {
            accumulated_Distance = 0f;
        }
    }
}
