using UnityEngine;
using System.Collections;

public class Recursion : MonoBehaviour
{
	public GameObject smallPlayer;
	public GameObject bigPlayer;
	public GameObject hatPlatform;
	public GameObject background;
	public ShowHide oldSky, newSky, newCloud;
	public bool zoomOut = false;

	public void TriggerZoomOut (Collider2D other)
	{
		if (!zoomOut)
		{
			zoomOut = true;

			// kill the hat platform and the small player
			Destroy(smallPlayer);
			Destroy(hatPlatform);

			// zoom the camera way out
			ZoomCamera z = Camera.main.gameObject.AddComponent<ZoomCamera>();
			z.startZoom = Camera.main.orthographicSize;
			z.endZoom = 80.0f;
			z.delayTime = 0.5f;
			z.lerpTime = 5.0f;
			z.callback = gameObject;

			// scale up the background
			LerpScale ls = background.AddComponent<LerpScale>();
			ls.endScale = 40.0f;
			ls.delayTime = 0.5f;
			ls.lerpTime = 4.0f;
			ls.smooth = false;

			Invoke("SwapSky", 1f);
			Invoke("ReturnControl", 3f);
		}
	}

	public void SwapSky ()
	{
		oldSky.Hide();
		newSky.Show();
		newCloud.Show();
	}

	public void ReturnControl ()
	{
		// enable control of the big player
		bigPlayer.GetComponent<Platformer>().enabled = true;
		bigPlayer.GetComponent<Collider2D>().enabled = true;
		bigPlayer.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	public void ZoomCallback (GameObject obj)
	{
		newCloud.GetComponent<FloatAround>().enabled = true;
	}
}
