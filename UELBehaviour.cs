using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UEL {
  public class UELBehaviour : MonoBehaviour, ISubject {

    public delegate void Task();
    
    // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    // Invoke Wrapper Functions
    // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
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
    
    // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    // Observer Pattern Implementation
    // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    public HashSet<IObserver> observers;
    
    public void NotifyAll() {
      foreach (IObserver o in observers)
        o.Notify();
    }

    public void Attach(IObserver observer) {
      if (observers == null)
        observers = new HashSet<IObserver>();
      
      observers.Add(observer);
    }

    public void Detach(IObserver observer) {
      if (observers.Contains(observer))
        observers.Remove(observer);
      else
        throw new UnityException("UELBehaviour - Detach - Observer not present!");
    }
  }
}