using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound {
	public string name;
	public AudioClip clip;
	[Range(0f, 1f)] public float volume = .75f;
	[Range(0f, 1f)] public float volumeVariance = .1f;
	[Range(.1f, 3f)] public float pitch = 1f;
	[Range(0f, 1f)] public float pitchVariance = .1f;
	public bool loop = false;
	public AudioMixerGroup mixerGroup;
	[HideInInspector] public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
	public AudioMixerGroup mixerGroup;
	public Sound[] sounds;

	[SerializeField] protected Slider volumeSlider;

	void Awake()
	{
		// only one audio manager allowed
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	void Update() {
		AudioListener.volume = volumeSlider.value;
	}

	public void Play(string sound)
	{
		// find sound 
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		// apply volume and pitch with variance
		s.source.volume = s.volume  * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch  * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

}