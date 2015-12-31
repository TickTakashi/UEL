using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DontDestroyOnLoad : MonoBehaviour {
  static List<GameObject> ddol;
  void Start() {
    if (ddol == null) {
      ddol = new List<GameObject>();
    }

    if (!ddol.Exists((GameObject go) => { return go.name.Equals(gameObject.name); })) {
      DontDestroyOnLoad(gameObject);
      ddol.Add(gameObject);
    } else {
      Destroy(gameObject);
    }
  }
}
