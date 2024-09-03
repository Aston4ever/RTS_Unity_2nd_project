using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [Header("Params")]
    [Range(1,10)][SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 2f;
    [SerializeField] private float gravity = 0.03f;

    [Range(1,3)][SerializeField] private float rotationSpeed = 1f;
    
    [Header("Camera")]
    [SerializeField] private Transform cameraTarget;
    [Tooltip("restricted upper angle")][SerializeField] private float topClamp = 90f;
    [Tooltip("restricted lower angle")][SerializeField] private float bottomClamp = -90f;

    [SerializeField] private float groundedOffset = 0.76f;
    [SerializeField] private float groundedRadius = 0.28f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded;
    
    private float mouseX;
    private float mouseY;

    private float cinemachineTargetPitch;
    private float rotationVelocity;
    
    private CharacterController controller;
    private PlayerAnimations anims;

    private Vector3 inputDir;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        anims = GetComponentInChildren<PlayerAnimations>();
    }

    private void FixedUpdate() {
        Vector3 spherePosition = new Vector3( transform.position.x, transform.position.y - groundedOffset, transform.position.z );
        isGrounded = Physics.CheckSphere( spherePosition, groundedRadius, groundLayer, QueryTriggerInteraction.Ignore );
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Vector3 spherePosition = new Vector3( transform.position.x, transform.position.y - groundedOffset, transform.position.z );
        Gizmos.DrawSphere(spherePosition, groundedRadius);
    }

    void Update() {
        GetInput();
        DoRotation();
        CheckForJump();
        DoMovement();
        DoAnimations();
    }

    private void CheckForJump() {
        if ( isGrounded ) {
            if ( Input.GetKey(KeyCode.Space) ) {
                inputDir.y = jump;
            } else {
                inputDir.y = 0f;
            }
        } else {
            inputDir.y -= gravity;
        }
    }

    private void DoRotation() {
        if ( mouseX == 0f && mouseY == 0f ) 
            return;
        cinemachineTargetPitch += mouseY * rotationSpeed * -1;
        rotationVelocity = mouseX * rotationSpeed;

        cinemachineTargetPitch = ClampAngle( cinemachineTargetPitch, bottomClamp, topClamp );
        cameraTarget.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0f, 0f);
            
        transform.Rotate(Vector3.up * rotationVelocity);
        
    }
    private float ClampAngle( float lfAngle, float lfMin, float lfMax ) {
        if ( lfAngle < -360f ) lfAngle += 360f;
        if ( lfAngle > 360f ) lfAngle -= 360f;
        return Mathf.Clamp( lfAngle, lfMin, lfMax );
    }
    private void GetInput() {
        float hor = Input.GetAxis( "Horizontal" );
        float ver = Input.GetAxis( "Vertical" );

        inputDir = new Vector3( hor, inputDir.y, ver );

        mouseX = Input.GetAxis( "Mouse X" );
        mouseY = Input.GetAxis( "Mouse Y" );
    }
    private void DoMovement() {
        Vector3 moveDirection = transform.TransformDirection( inputDir ) * speed;
        moveDirection.y = inputDir.y;
        
        controller.Move( moveDirection * Time.deltaTime );
    }

    private void DoAnimations() {
        if ( inputDir.x != 0 || inputDir.z != 0) {
            anims.SetWalkAnimation(true);
        } else {
            anims.SetWalkAnimation(false);
        }
    }
}
