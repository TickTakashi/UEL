using UnityEngine;
using System.Collections;

public class OpenLink : MonoBehaviour {
  public string url = "http://www.ticktakashi.com";

  // Use this for initialization
  public void VisitLink() {
    Application.OpenURL(url);
  }
}