using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesture : MonoBehaviour {
  public Renderer rend;
  public Vector3 downPosition;
  public Vector3 upPosition;
  public Vector3 v;

  public float width;
  public float height;
  public float angle;
  public float length;

  public const int ATTACKTYPE_NONE = 0x00;
  public const int ATTACKTYPE_SLASH = 0x01;
  public const int ATTACKTYPE_STAB = 0x02;
  public const int ATTACKTYPE_HACK = 0x03;

  // Use this for initialization
  void Start () {
    rend = GetComponent<Renderer>();
  }
  
  // Update is called once per frame
  void Update () {
    
  }

  void OnMouseDown() {
    downPosition = Input.mousePosition;
    // Debug.Log("DOWN: (" + downPosition.x + ", " + downPosition.y + ")");
  }

  void OnMouseDrag() {
  }

  void OnMouseUp() {
    upPosition = Input.mousePosition;
    // Debug.Log("UP: (" + upPosition.x + ", " + upPosition.y + ")");

    width = upPosition.x - downPosition.x;
    height = upPosition.y - downPosition.y;
    angle = Mathf.Atan(height / width);
    length = Mathf.Sqrt( Mathf.Pow(width, 2) + Mathf.Pow(height, 2));

    // Debug.Log("HEIGHT: " + height + ", WIDTH: " + width + ", ANGLE (radians): " + angle + ", ANGLE (degrees): " + (angle * (180/Mathf.PI) ) );

    Debug.Log("ATTACK TYPE: " + new string[] {
      "None",
      "Slash",
      "Stab",
      "Hack"
      }[determineAttackType()]);
  }

  int determineAttackType() {
    // it's not an attack if you're going from left to right
    if (width < 0) {
      return ATTACKTYPE_NONE;
    }

    // if the length of the motion is too short, then don't do anything
    if (length < 50) {
      return ATTACKTYPE_NONE;
    }

    // between 10 degrees and 90 degrees, inclusively
    if (angle >= 0.174533 && angle <= 1.5708) {
      return ATTACKTYPE_SLASH;

    // between 10 degrees and -10 degrees, inclusively
    } else if (angle <= 0.174533 && angle >= -0.174533) {
      return ATTACKTYPE_STAB;
    
    // between -10 degrees and -90 degrees, inclusively
    } else if (angle <= -0.174533 && angle >= -90) {
      return ATTACKTYPE_HACK;
    }

    return ATTACKTYPE_NONE;
  }
}
