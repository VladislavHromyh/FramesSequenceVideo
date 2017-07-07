using UnityEngine;

public class FramesSequenceVideo : MonoBehaviour {

	public Material Material;
	public bool IsLoop;
	[HideInInspector] public bool IsPlaying;
	private Texture[] frames;
	private int index = 0;
	private YieldInstruction timeToWait;

	public void Initialize(string pathToFrames, int framesPerSecond) {
		frames = Resources.LoadAll<Texture>(pathToFrames);
		timeToWait = new WaitForSeconds(1.0f / ((float)framesPerSecond));
	}

	public void Play() {
		IsPlaying = true;
		StartCoroutine("PlayingCoroutine");
	}

	public void Pause() {
		IsPlaying = false;
		StopCoroutine("PlayingCoroutine");
	}

	public void Stop() {
		IsPlaying = false;
		StopCoroutine("PlayingCoroutine");
		index = 0;
	}

	private IEnumerator PlayingCoroutine() {
		while (true) {
			Material.mainTexture = frames[index];
			++index;
			if (index == frames.Length - 1) {

				if (IsLoop) {
					index = 0;
				} else {
					index = 0;
					IsPlaying = false;
					yield break;
				}
			}

			yield return timeToWait;
		}
	}
}
