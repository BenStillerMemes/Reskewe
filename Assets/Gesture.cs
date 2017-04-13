using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesture : MonoBehaviour {
  public Animator attackAnim;
  public Animator walkAnim;

  public Renderer rend;
  public Vector3 downPosition;
  public Vector3 upPosition;

  public bool dragging = false;

  public float width;
  public float height;
  public float angle;
  public float length;

  public const int ATTACKTYPE_NONE = 0x00;
  public const int ATTACKTYPE_SLASH = 0x01;
  public const int ATTACKTYPE_STAB = 0x02;
  public const int ATTACKTYPE_HACK = 0x03;

  public LineRenderer line;

  // Use this for initialization
  void Start () {
    line = GetComponent<LineRenderer>();
    line.material = new Material(Shader.Find("Particles/Additive"));
    line.widthMultiplier = 0.2f;
    line.positionCount = 2;
    
    line.sortingOrder = 4;
    line.sortingLayerName = "UI";

    // A simple 2 color gradient with a fixed alpha of 1.0f.
    float alpha = 1.0f;
    Gradient gradient = new Gradient();
    gradient.SetKeys(
      new GradientColorKey[] { new GradientColorKey(Color.white, 1.0f), new GradientColorKey(Color.red, 1.0f) },
      new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1.0f), new GradientAlphaKey(alpha, 1.0f) }
    );
    line.colorGradient = gradient;
  }
  
  // Update is called once per frame
  void Update () {
    if (dragging) {
      upPosition = Input.mousePosition;

      line.SetPositions( new Vector3[] { Camera.main.ScreenToWorldPoint(new Vector3(downPosition.x,downPosition.y, Camera.main.nearClipPlane)), Camera.main.ScreenToWorldPoint(new Vector3(upPosition.x, upPosition.y, Camera.main.nearClipPlane)) } );
    }
  }

  void OnMouseDown() {
    downPosition = Input.mousePosition;

    dragging = true;
    line.enabled = true;
    // Debug.Log("DOWN: (" + downPosition.x + ", " + downPosition.y + ")");
  }

  void OnMouseDrag() {
  }

  void OnMouseUp() {
    upPosition = Input.mousePosition;
    dragging = false;
    line.enabled = false;
    // Debug.Log("UP: (" + upPosition.x + ", " + upPosition.y + ")");

    width = upPosition.x - downPosition.x;
    height = upPosition.y - downPosition.y;
    angle = Mathf.Atan(height / width);
    length = Mathf.Sqrt( Mathf.Pow(width, 2) + Mathf.Pow(height, 2));

    // Debug.Log("HEIGHT: " + height + ", WIDTH: " + width + ", ANGLE (radians): " + angle + ", ANGLE (degrees): " + (angle * (180/Mathf.PI) ) );
    int attackType = determineAttackType();
    if (attackType > 0) {
      attackAnim.SetTrigger (new string[] {
        "",
        "underhand",
        "stab",
        "overhead"
        }[determineAttackType()]);
    }
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
