using UnityEngine;
using System.Collections;

namespace UEL {
  public static class UELMethods {

    // Gameobject Methods 
    public static T GetSafeComponent<T>(this GameObject obj) where T : MonoBehaviour {
      T component = obj.GetComponent<T>();

      if (component == null) {
        Debug.LogError("Expected to find component of type "
           + typeof(T) + " but found none", obj);
      }

      return component;
    }

    // Transform Methods
    public static void SetX(this Transform trans, float x) {
      Vector3 new_position =
        new Vector3(x, trans.position.y, trans.position.z);

      trans.position = new_position;
    }

    public static void SetY(this Transform trans, float y) {
      Vector3 new_position =
        new Vector3(trans.position.x, y, trans.position.z);

      trans.position = new_position;
    }

    public static void SetZ(this Transform trans, float z) {
      Vector3 new_position =
        new Vector3(trans.position.x, trans.position.y, z);

      trans.position = new_position;
    }

    public static void Rotate(this Transform trans, Quaternion rotation) {
      trans.rotation = trans.rotation * rotation;
    }

    public static void RotateAroundPoint(this Transform trans, Vector3 pivot,
      Quaternion rotation) {
      trans.position = trans.position.RotatedAroundPoint(pivot, rotation);
      trans.Rotate(rotation);
    }

    public static HSBColor ToHSB(this Color c) {
      return HSBColor.FromColor(c);
    }

    public static Color SetBrightness(this Color c, float brightness) {
      HSBColor h = c.ToHSB();
      h = new HSBColor(h.h, h.s, brightness, h.a);
      return h.ToColor();
    }

    public static Color SetHue(this Color c, float hue) {
      HSBColor h = c.ToHSB();
      h = new HSBColor(hue, h.s, h.b, h.a);
      return h.ToColor();
    }

    public static Color SetSaturation(this Color c, float saturation) {
      HSBColor h = c.ToHSB();
      h = new HSBColor(h.h, saturation, h.b, h.a);
      return h.ToColor();
    }

    public static Color SetAlpha(this Color c, float alpha) {
      Color col = new Color(c.r, c.g, c.b, alpha);
      return col;
    }

    // Vector3 Methods
    public static Vector3 RotatedAroundPoint(this Vector3 point, Vector3 pivot,
      Quaternion rotation) {
      Vector3 relative_position = point - pivot;
      Vector3 new_relative_position = rotation * relative_position;
      Vector3 final_position = new_relative_position + pivot; 
      return final_position;
    }
    

    // Vector2 Methods
  }
}
