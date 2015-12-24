using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DontDestroyOnLoad : MonoBehaviour {
  static List<GameObject> ddol;
  void Start() {
    if (ddol == null) {
      Debug.Log("ddol is null.");
      ddol = new List<GameObject>();
    }

    if (!ddol.Exists((GameObject go) => { return go.name.Equals(gameObject.name); })) {
      Debug.Log("Doesn't contain this, add this!");
      DontDestroyOnLoad(gameObject);
      ddol.Add(gameObject);
    } else {
      Destroy(gameObject);
    }
  }
}
