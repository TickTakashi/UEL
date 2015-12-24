using UnityEngine;
using System.Collections;
using System;

namespace UEL {
  public delegate bool EventArgsCondition<E>(E eventArgs) where E : EventArgs;
  public delegate bool Condition();
  public delegate void Func();

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

    public static bool IsInLayerMask(this GameObject obj, LayerMask layerMask) {
      return (layerMask.value & (1 << obj.layer)) > 0;
    }

    public static float ToAngle(this Vector2 vec) {
        return Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;
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

    public static Color Sinerp(Color a, Color b, float t) {
      return Color.Lerp(a, b, Mathf.Sin(t * Mathf.PI * 0.5f));
    }

    public static Color Coserp(Color a, Color b, float t) {
      return Color.Lerp(a, b, Mathf.Cos(t * Mathf.PI * 0.5f));
    }

    public static Color Quaderp(Color a, Color b, float t) {
      return Color.Lerp(a, b, t * t);
    }

    public static Color Smoothstep(Color a, Color b, float t) {
      return Color.Lerp(a, b, t * t * (3f - 2f * t));
    }

    public static Color Smootherstep(Color a, Color b, float t) {
      return Color.Lerp(a, b, t * t * t * (t * (6f * t - 15f) + 10f));
    }

    // Vector3 Methods
    public static Vector3 WithX(this Vector3 vec, float new_x) {
      return new Vector3(new_x, vec.y, vec.z);
    }

    public static Vector3 WithY(this Vector3 vec, float new_y) {
      return new Vector3(vec.x, new_y, vec.z);
    }

    public static Vector3 WithZ(this Vector3 vec, float new_z) {
      return new Vector3(vec.x, vec.y, new_z);
    }

    public static Vector3 RotatedAroundPoint(this Vector3 point, Vector3 pivot,
      Quaternion rotation) {
      Vector3 relative_position = point - pivot;
      Vector3 new_relative_position = rotation * relative_position;
      Vector3 final_position = new_relative_position + pivot;
      return final_position;
    }

    public static Vector3 Sinerp(Vector3 a, Vector3 b, float t) {
      return Vector3.Lerp(a, b, Mathf.Sin(t * Mathf.PI * 0.5f));
    }

    public static Vector3 Coserp(Vector3 a, Vector3 b, float t) {
      return Vector3.Lerp(a, b, Mathf.Cos(t * Mathf.PI * 0.5f));
    }

    public static Vector3 Quaderp(Vector3 a, Vector3 b, float t) {
      return Vector3.Lerp(a, b, t * t);
    }

    public static Vector3 Smoothstep(Vector3 a, Vector3 b, float t) {
      return Vector3.Lerp(a, b, t * t * (3f - 2f * t));
    }

    public static Vector3 Smootherstep(Vector3 a, Vector3 b, float t) {
      return Vector3.Lerp(a, b, t * t * t * (t * (6f * t - 15f) + 10f));
    }

    // Vector2 Methods

    public static Vector2 Sinerp(Vector2 a, Vector2 b, float t) {
      return Vector2.Lerp(a, b, Mathf.Sin(t * Mathf.PI * 0.5f));
    }

    public static Vector2 Coserp(Vector2 a, Vector2 b, float t) {
      return Vector2.Lerp(a, b, 1f - Mathf.Cos(t * Mathf.PI * 0.5f));
    }

    public static Vector2 Quaderp(Vector2 a, Vector2 b, float t) {
      return Vector2.Lerp(a, b, t * t);
    }

    public static Vector2 Smoothstep(Vector2 a, Vector2 b, float t) {
      return Vector2.Lerp(a, b, t * t * (3f - 2f * t));
    }

    public static Vector2 Smootherstep(Vector2 a, Vector2 b, float t) {
      return Vector2.Lerp(a, b, t * t * t * (t * (6f * t - 15f) + 10f));
    }

    // Quaternion Methods

    public static Quaternion LookAtRotation(this Transform trans, Vector3 point) {
      return Quaternion.LookRotation((-trans.position + point).normalized);
    }

    public static Quaternion Sinerp(Quaternion a, Quaternion b, float t) {
      return Quaternion.Lerp(a, b, Mathf.Sin(t * Mathf.PI * 0.5f));
    }

    public static Quaternion Coserp(Quaternion a, Quaternion b, float t) {
      return Quaternion.Lerp(a, b, Mathf.Cos(t * Mathf.PI * 0.5f));
    }

    public static Quaternion Quaderp(Quaternion a, Quaternion b, float t) {
      return Quaternion.Lerp(a, b, t * t);
    }

    public static Quaternion Smoothstep(Quaternion a, Quaternion b, float t) {
      return Quaternion.Lerp(a, b, t * t * (3f - 2f * t));
    }

    public static Quaternion Smootherstep(Quaternion a, Quaternion b, float t) {
      return Quaternion.Lerp(a, b, t * t * t * (t * (6f * t - 15f) + 10f));
    }

    // Float Methods
    public static float Sinerp(float a, float b, float t) {
      return Mathf.Lerp(a, b, Mathf.Sin(t * Mathf.PI * 0.5f));
    }

    public static float Coserp(float a, float b, float t) {
      return Mathf.Lerp(a, b, 1f - Mathf.Cos(t * Mathf.PI * 0.5f));
    }

    public static float Quaderp(float a, float b, float t) {
      return Mathf.Lerp(a, b, t * t);
    }

    public static float Smoothstep(float a, float b, float t) {
      return Mathf.Lerp(a, b, t * t * (3f - 2f * t));
    }

    public static float Smootherstep(float a, float b, float t) {
      return Mathf.Lerp(a, b, t * t * t * (t * (6f * t - 15f) + 10f));
    }

    // RectTransform Methods
    public static void SetLocation(this RectTransform trans, Vector2 point) {
      trans.anchorMin = point;
      trans.anchorMax = point;
    }
  }
}
