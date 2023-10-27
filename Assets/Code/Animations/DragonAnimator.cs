using UnityEngine;

public class DragonAnimator : MonoBehaviour {

    
    string currentAnimation;
    
    private Animator animator;
    
    private readonly string IsWalk = "IsWalk";
    private readonly string IsRun = "IsRun";
    private readonly string Atack = "Attack_1";
    private readonly string Dead = "Dead";

    void OnEnable() {
        animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation() {
        StopWalkingAndRunningAnimations();
        TryChangingAnimationTo(Atack);
    }

    public void PlayDeadAnimation() {
        StopWalkingAndRunningAnimations();
        TryChangingAnimationTo(Dead);
    }

    void TryChangingAnimationTo(string clipName) {
        if(currentAnimation == clipName)
            return;
        animator.SetTrigger(clipName);
        currentAnimation = clipName;
    }
    
    public void PlayWalkAnimation() {
        animator.SetBool(IsWalk, true);
        currentAnimation = IsWalk;
    }

    public void StopWalkingAndRunningAnimations() {
        animator.SetBool(IsWalk, false);
        animator.SetBool(IsRun, false);
    }

    public void PlayRunAnimation() {
        animator.SetBool(IsWalk, true);
        animator.SetBool(IsRun, true);
    }
}
