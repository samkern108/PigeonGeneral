using UnityEngine;

namespace N_extensions
{

public static class AnimationEx
{
	public static bool isAnimationComplete(this Animation animation, AnimationClip clip)
	{
		Debug.Assert(clip != null);

		UnityEngine.AnimationState state = animation[clip.name];

		return (state.enabled == false || (Mathf.Abs(1f - state.normalizedTime) < 0.001f));		
	}
}

}	// N_application