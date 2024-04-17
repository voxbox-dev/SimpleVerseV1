using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    [RequireComponent(typeof(Animator))]
    public class RandomizeAnimation : MonoBehaviour
    {
        private Animator animator;
        private AnimationClip[] clips;

        void Awake()
        {
            // Get the Animator component
            animator = GetComponent<Animator>();
            // Get all animation clips from the Animator Controller
            clips = animator.runtimeAnimatorController.animationClips;
        }

        void Start()
        {
            // Start the random animation sequence
            StartCoroutine(PlayRandomAnimation());
        }

        private IEnumerator PlayRandomAnimation()
        {
            while (true)
            {
                // Pick a random animation clip
                int randomIndex = Random.Range(0, clips.Length);
                AnimationClip randomClip = clips[randomIndex];

                // Play the randomly selected animation clip
                animator.Play(randomClip.name);

                // Wait for the animation clip to finish before selecting the next one
                yield return new WaitForSeconds(randomClip.length);
            }
        }
    }
}
