using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UEL {
  public class UELBehaviour : MonoBehaviour {
    public delegate void Task();
    public void Invoke(Task task, float delay) {
      Invoke(task.Method.Name, delay);
    }

    public void InvokeRepeating(Task task, float delay, float interval) {
      InvokeRepeating(task.Method.Name, delay, interval);
    }

    public void InvokeRepeating(Task task, float interval) {
      InvokeRepeating(task, 0f, interval);
    }

    public bool IsInvoking(Task task) {
      return IsInvoking(task.Method.Name);
    }

    public void CancelInvoke(Task task) {
      CancelInvoke(task.Method.Name);
    }

    public delegate void GradualFunction(float f);
    public IEnumerator Gradual(float max_time, GradualFunction func) {
      float f = 0f;
      while (f <= 1f) {
        f += Time.deltaTime / max_time;
        func(f);
        yield return null;
      }
    }
  }
}