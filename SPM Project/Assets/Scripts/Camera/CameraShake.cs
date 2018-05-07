using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve IntensityToMagnitudeCurve;
    public float MaxDuration;
    public Vector2 PerlinSpeed;
    public Vector2 MaxShake;
    public float RollSpeed;
    public float MaxRoll;

	//Audio
	private static AudioSource source;
	[Header("AudioClips")]
	public AudioClip Shake;

    private static float _intensity;

    private void LateUpdate()
    {
		if (Input.GetKeyDown ("p")) {
			CameraShake.AddIntensity(1);
		} 
        _intensity -= Time.deltaTime / MaxDuration;
        _intensity = Mathf.Clamp01(_intensity);
        float magnitude = IntensityToMagnitudeCurve.Evaluate(_intensity);
        float xPerlin = Mathf.Lerp(-MaxShake.x, MaxShake.x, Mathf.PerlinNoise(Time.time * PerlinSpeed.x, 0.0f));
        float yPerlin = Mathf.Lerp(-MaxShake.y, MaxShake.y, Mathf.PerlinNoise(0.0f, Time.time * PerlinSpeed.y));
        float roll = Mathf.Lerp(-MaxRoll, MaxRoll, Mathf.PerlinNoise(Time.time * RollSpeed, Time.time * RollSpeed));
        transform.localPosition = new Vector3(xPerlin, yPerlin, 0.0f) *  magnitude;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, roll * magnitude);
    }


    public static void AddIntensity(float intensity)
    {
        _intensity += intensity;
		if (!source.isPlaying) {
			source.Play ();			
		}

    }

	void Start(){
		source = GetComponent<AudioSource> ();
		source.clip = Shake;
	}
}
