using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper {

	public static void FadeOut(AudioSource source, float fadeDuration, float timer){
		source.volume = 1 - (timer * (1 / fadeDuration));
	}

	public static IEnumerator FadeOutEnum(AudioSource source, float fadeDuration) {
		float startVolume = source.volume;

		while (source.volume > 0) {
			source.volume -= startVolume * Time.deltaTime / fadeDuration;

			yield return null;
		}
		//source.Stop ();
		//source.volume = startVolume;

		yield return null;
	}
}
