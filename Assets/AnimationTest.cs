using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {

	public Animator attackAnim;
	public Animator walkAnim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q")) 
		{
			//print ("Q");
			attackAnim.SetTrigger ("overhead");
		} else if (Input.GetKeyDown ("w")) 
		{
			//print ("W");
			attackAnim.SetTrigger ("stab");
		} else if (Input.GetKeyDown ("e")) 
		{
			//print ("E");
			attackAnim.SetTrigger ("underhand");
		}

		if (Input.GetKeyDown ("d")) 
		{
			if (transform.localScale.x != 1) 
			{
				transform.localScale = new Vector3 (1, 1, 1);
			}
			walkAnim.SetBool ("walking", true);
		} else if (Input.GetKeyDown ("a")) 
		{
			if (transform.localScale.x != -1) 
			{
				transform.localScale = new Vector3 (-1, 1, 1);
			}
			walkAnim.SetBool ("walking", true);
		}
		if (!Input.GetKey ("a") && !Input.GetKey ("d")) 
		{
			walkAnim.SetBool ("walking", false);
		}
	
	}
}
