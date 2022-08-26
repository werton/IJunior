using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private const string AnimationIdle = "idle";
    private const string AnimationWalking = "walking";
    private const string AnimationJump = "jump";
    private const string AnimationFall = "fall";
    private const string AnimationHurt = "hurt";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        _animator.SetBool(AnimationIdle, true);
        _animator.SetBool(AnimationWalking, false);
    }

    public void SetWalk()
    {
        _animator.SetBool(AnimationWalking, true);
        _animator.SetBool(AnimationIdle, false);
    }

    public void SetHurt()
    {
        _animator.SetBool(AnimationHurt, true);
    }

    public void SetFall()
    {
        _animator.SetTrigger(AnimationFall);
        _animator.SetBool(AnimationIdle, false);
    }

    public void SetJump()
    {
        _animator.SetTrigger(AnimationJump);
        _animator.SetBool(AnimationIdle, false);
        _animator.SetBool(AnimationWalking, false);
    }
}