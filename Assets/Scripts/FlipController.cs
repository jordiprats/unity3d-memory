using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipController : MonoBehaviour {

	public bool blocked=false;
	private bool faceup=false;

	public float degreespersecond = 360f;

	private float limit_flip=180f;

	public void FlipCard()
	{
		if(((MemoryController)GameObject.Find("screen").GetComponent(typeof(MemoryController))).flipping)
		{
			Debug.Log("delayed flip: "+this.name);
		}
		else
		{
			Debug.Log("flipping "+this.name);
		}
		StartCoroutine(wait_for_flip());
	}

	IEnumerator wait_for_flip()
	{
		while( ((MemoryController)GameObject.Find("screen").GetComponent(typeof(MemoryController))).flipping )
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
			((MemoryController)GameObject.Find("screen").GetComponent(typeof(MemoryController))).flipping = true;

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
			((MemoryController)GameObject.Find("screen").GetComponent(typeof(MemoryController))).flipping=false;
		}
	}
}
