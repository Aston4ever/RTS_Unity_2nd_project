using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    private const string walkStr = "Walk";
    private Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SetWalkAnimation( bool isWalking ) {
        animator.SetBool(walkStr, isWalking);
    }
}
