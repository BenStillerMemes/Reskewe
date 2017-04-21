using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour {
  public const float STANDARD_SPEED = 9f;
  public Animator walkAnim;
  private Rigidbody2D body;

  void Start() {
    body = GetComponent<Rigidbody2D>();
  }
  
  void FixedUpdate() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    if (horizontal != 0 || vertical != 0) {
      body.velocity = new Vector2(horizontal, vertical) * STANDARD_SPEED;
    }
  }
}
