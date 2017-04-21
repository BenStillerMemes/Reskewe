using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour {
  public Transform target;
  private Vector3 velocity = Vector3.zero;
  public Camera mainCamera;
  public Vector3 point;
  public float speed;

  public Vector3 newCamPosition;

  // Use this for initialization
  void Start () {
    newCamPosition = mainCamera.transform.position;
  }

  // Update is called once per frame
  void Update () {
    if (target)
    {
      point = mainCamera.WorldToViewportPoint(target.position);
      if (point.x < 0.2f) {
        newCamPosition += (Vector3.left * 5);
      } else if (point.x > 0.8f) {
        newCamPosition += (Vector3.right * 5);
      }

      if (newCamPosition.x < -9.55f) { newCamPosition.x = -9.55f; }
      if (newCamPosition.x > 9.45f) { newCamPosition.x = 9.45f; }

      // mainCamera.transform.position = newCamPosition;
    }

    mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, newCamPosition, ref velocity, 0.5f, Mathf.Infinity);
  }
}
