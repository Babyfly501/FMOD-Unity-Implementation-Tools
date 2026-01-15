using UnityEngine;
using FMODUnity;

public class PlayOneShotSound : MonoBehaviour
{
	[SerializeField]
	private EventReference soundEvent;

	public void PlayOneShotEvent()
	{
		if (!string.IsNullOrEmpty(soundEvent.Path))
		{
			RuntimeManager.PlayOneShot(soundEvent);
		}
	}
}