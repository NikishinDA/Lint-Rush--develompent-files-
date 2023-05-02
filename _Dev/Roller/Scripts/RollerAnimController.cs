using UnityEngine;

namespace _Internal._Dev.Roller.Scripts
{
    public class RollerAnimController
    {
        private Animator animator;
        public RollerAnimController(Animator animator)
        {
            this.animator = animator;
        }

        public void AnimationStop()
        {
            animator.speed = 0;
        }

        public void AnimationPlay()
        {
            animator.speed = 1;
        }

        public void AnimationEnabled(bool enable)
        {
            animator.enabled = enable;
        }
    }
}
