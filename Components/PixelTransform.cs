using UnityEngine;
using System.Collections;

public class PixelTransform : MonoBehaviour {

  public Transform driver;
  public Vector2 pixel_offset;
  public int pixels_per_unit = 8;

  private Transform trans;

  void Start () {
    trans = GetComponent<Transform>();
  }

  void LateUpdate () {
    SnapTransformToPixel();
  }

  public void SnapTransformToPixel() {
    Vector2 p_pos = RoundToPixel(driver.position);
    trans.position = new Vector3(p_pos.x, p_pos.y, trans.position.z);
  }

  public Vector2 RoundToPixel(Vector2 position) {
    Vector2 rounded = new Vector2(RoundOrdinate(position.x), RoundOrdinate(position.y));
    return rounded;
  }

  float RoundOrdinate(float ord) {
    ord = Mathf.Round(ord / PixelGridSize()) * PixelGridSize();
    return ord;
  }

  float PixelGridSize() {
    return 1f / pixels_per_unit;
  }
}