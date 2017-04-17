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
		if (Input.GetKey ("a") && Input.GetKey ("d")) {
			walkAnim.SetBool("walking", false);
			return;
		}

		if (Input.GetKey ("w") && Input.GetKey ("s")) {
			walkAnim.SetBool("walking", false);
			return;
		}

		if (Input.GetKey ("d") && transform.localScale.x != 1) 
		{
			transform.localScale = new Vector3 (1, 1, 1);
		} else if (Input.GetKey ("a") && transform.localScale.x != -1) 
		{
				transform.localScale = new Vector3 (1.5f, 1.5f, 1);
			walkAnim.SetBool ("walking", true);
		} else if (Input.GetKeyDown ("a")) 
		{
			if (transform.localScale.x != -1) 
			{
				transform.localScale = new Vector3 (-1.5f, 1.5f, 1);
			}
			walkAnim.SetBool ("walking", true);
		}

		walkAnim.SetBool("walking", Input.GetKey ("a") || Input.GetKey ("s") || Input.GetKey ("d") || Input.GetKey ("w"));

	}
}
