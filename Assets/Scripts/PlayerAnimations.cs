using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    [SerializeField] private float blendPower = 0.2f;
    [SerializeField] private float blendSpeed = 25f;

    private const string blendStr = "MotionSpeed";
    private const string jumpStr = "Jump";
    private const string fallStr = "Fall";
    private Animator animator;
    private float animationBlend;
    void Start() {
        animator = GetComponent<Animator>();
    }

    public void SetJumpAnim() {
        animator.SetTrigger(jumpStr);
    }
    public void SetFallAnim(bool isFalling) {
        animator.SetBool(fallStr, isFalling);
    }
    public void SetMotionSpeed( float value ) {
        animationBlend = Mathf.Lerp( animationBlend, value * blendPower, Time.deltaTime * blendSpeed );
        animator.SetFloat(blendStr, animationBlend);
    }
}
