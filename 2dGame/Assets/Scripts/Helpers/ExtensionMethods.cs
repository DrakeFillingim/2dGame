using UnityEngine;
using System.Linq;

public static class ExtensionMethods
{
    public static AnimationClip GetClip(this Animator anim, string animationName)
    {
        return anim.runtimeAnimatorController.animationClips.First(x => x.name == animationName);
    }

    /*public static AnimationClip GetClip(this Animator anim, int animationHash)
    {
        return anim.runtimeAnimatorController.animationClips.First(x => Animator.StringToHash(x.name) == animationHash);
    }*/
}
