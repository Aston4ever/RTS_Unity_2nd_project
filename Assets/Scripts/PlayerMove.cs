using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 2f;
    [SerializeField] private float gravity = 0.03f;

    private CharacterController controller;

    private Vector3 inputDir;
    void Start() {
        controller = GetComponent<CharacterController>();
    }
    
    void Update() {
        float hor = Input.GetAxis( "Horizontal" );
        float ver = Input.GetAxis( "Vertical" );

        inputDir = new Vector3( hor, inputDir.y, ver );

        Vector3 moveDirection = transform.TransformDirection( inputDir ) * speed;
        moveDirection.y = inputDir.y;
        
        controller.Move( moveDirection * Time.deltaTime );
    }
}
