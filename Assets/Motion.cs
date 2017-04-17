using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour {
  public const float STANDARD_SPEED = 1.0f;

  void Update () {
    if (Input.GetKey(KeyCode.A)) {
      transform.position += (Vector3.left * STANDARD_SPEED * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.D)) {
      transform.position += (Vector3.right * STANDARD_SPEED * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.W)) {
      transform.position += (Vector3.up * STANDARD_SPEED * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.S)) {
      transform.position += (Vector3.down * STANDARD_SPEED * Time.deltaTime);
    }
  }
}
