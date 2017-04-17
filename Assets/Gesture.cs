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

  public LineRenderer line;

  // Use this for initialization
  void Start () {
    line = GetComponent<LineRenderer>();
    line.material = new Material(Shader.Find("Particles/Additive"));
    line.widthMultiplier = 0.075f;
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
  }

  void OnMouseUp() {
    upPosition = Input.mousePosition;
    dragging = false;
    line.enabled = false;

    width = upPosition.x - downPosition.x;
    height = upPosition.y - downPosition.y;
    angle = Mathf.Atan(height / width);
    length = Mathf.Sqrt( Mathf.Pow(width, 2) + Mathf.Pow(height, 2));

    attack();
  }

  void attack() {
    if (transform.localScale.x < 0) {
      width = -width;
      angle = -angle;
    }

    // it's not an attack if you're going from left to right
    if (width < 0) {
      return;
    }

    // if the length of the motion is too short, then don't do anything
    if (length < 20) {
      return;
    }

    // between 10 degrees and 90 degrees, inclusively
    if (angle >= 0.174533 && angle <= 1.5708) {
      attackUnderhand();
      return;
    // between 10 degrees and -10 degrees, inclusively
    } else if (angle <= 0.174533 && angle >= -0.174533) {
      attackStab();
      return;

    // between -10 degrees and -90 degrees, inclusively
    } else if (angle <= -0.174533 && angle >= -90) {
      attackOverhead();
      return;
    }
  }

  void attackUnderhand() {
    attackAnim.SetTrigger("underhand");
  }

  void attackStab() {
    attackAnim.SetTrigger("stab");
  }

  void attackOverhead() {
    attackAnim.SetTrigger("overhead");
  }
}
