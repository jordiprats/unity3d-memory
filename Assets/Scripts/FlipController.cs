using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipController : MonoBehaviour {

	public bool flipping=false;

	public bool blocked=false;
	private bool faceup=false;

	public float degreespersecond = 360f;

	private float limit_flip=180f;

	public void FlipCard()
	{
		if(flipping)
		{
			Debug.Log("delayed flip: "+this.name);
			StartCoroutine(wait_for_flip());
		}
		else
		{
			StartCoroutine(flip());
			Debug.Log("flipping "+this.name);
		}
	}

	IEnumerator wait_for_flip()
	{
		while(flipping)
		{
			yield return new WaitForSeconds(0.01f);
		}
		StartCoroutine(flip());
	}

	IEnumerator flip()
	{
		bool done = false;
		if(!this.blocked)
		{
			flipping = true;

			while(!done)
			{
				float degree = degreespersecond * Time.deltaTime;
				if(faceup)
					degree=-degree;

				transform.Rotate(new Vector3(0, degree, 0));

				if(limit_flip <	transform.eulerAngles.y)
				{
					transform.Rotate(new Vector3(0, -degree, 0));
					done = true;
				}

				yield return new WaitForSeconds(0.01f);
			}

			faceup=!faceup;
			flipping=false;
		}
	}
}
