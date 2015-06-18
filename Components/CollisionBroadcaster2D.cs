using UnityEngine;
using System.Collections;
using System;
using UEL;

public class CollisionEventArgs : EventArgs {
  public Collision2D collision;
  public CollisionEventArgs(Collision2D collision) {
    this.collision = collision;
  }
}

public class TriggerEventArgs : EventArgs {
  public Collider2D collider;
  public TriggerEventArgs(Collider2D collider) {
    this.collider = collider;
  }
}

[RequireComponent (typeof(Rigidbody2D))]
public class CollisionBroadcaster2D : UELBehaviour {
  public event EventHandler<CollisionEventArgs> CollisionEnterEvent;
  public event EventHandler<CollisionEventArgs> CollisionStayEvent;
  public event EventHandler<CollisionEventArgs> CollisionExitEvent;

  public event EventHandler<TriggerEventArgs> TriggerEnterEvent;
  public event EventHandler<TriggerEventArgs> TriggerStayEvent;
  public event EventHandler<TriggerEventArgs> TriggerExitEvent;

  void OnCollisionEnter2D(Collision2D col) {
    if (CollisionEnterEvent != null)
      CollisionEnterEvent(this, new CollisionEventArgs(col));
  }

  public IEnumerator WaitForCollisionEnter(EventArgsCondition<CollisionEventArgs> condition, Condition c = null) {
    bool matched = false;
    EventHandler<CollisionEventArgs> handler = null;

    handler = (a, b) => {
      if (condition(b)) {
        matched = true;
        CollisionEnterEvent -= handler;
      }
    };

    CollisionEnterEvent += handler;

    while (!matched || (c != null && c()))
      yield return null;
  }
  

  void OnCollisionStay2D(Collision2D col) {
    if (CollisionStayEvent != null)
      CollisionStayEvent(this, new CollisionEventArgs(col));
  }

  public IEnumerator WaitForCollisionStay(EventArgsCondition<CollisionEventArgs> condition, Condition c = null) {
    bool matched = false;
    EventHandler<CollisionEventArgs> handler = null;

    handler = (a, b) => {
      if (condition(b)) {
        matched = true;
        CollisionStayEvent -= handler;
      }
    };

    CollisionStayEvent += handler;

    while (!matched || (c != null && c()))
      yield return null;
  }

  void OnCollisionExit2D(Collision2D col) {
    if (CollisionExitEvent != null)
      CollisionExitEvent(this, new CollisionEventArgs(col));
  }

  public IEnumerator WaitForCollisionExit(EventArgsCondition<CollisionEventArgs> condition, Condition c = null) {
    bool matched = false;
    EventHandler<CollisionEventArgs> handler = null;

    handler = (a, b) => {
      if (condition(b)) {
        matched = true;
        CollisionExitEvent -= handler;
      }
    };

    CollisionExitEvent += handler;

    while (!matched || (c != null && c()))
      yield return null;
  }

  void OnTriggerEnter2D(Collider2D col) {
    if (TriggerEnterEvent != null)
      TriggerEnterEvent(this, new TriggerEventArgs(col));
  }

  public IEnumerator WaitForTriggerEnter(EventArgsCondition<TriggerEventArgs> condition, Condition c = null) {
    bool matched = false;
    EventHandler<TriggerEventArgs> handler = null;

    handler = (a, b) => {
      if (condition(b)) {
        matched = true;
        TriggerEnterEvent -= handler;
      }
    };

    TriggerEnterEvent += handler;

    while (!matched || (c != null && c()))
      yield return null;
  }

  void OnTriggerStay2D(Collider2D col) {
    if (TriggerStayEvent != null)
      TriggerStayEvent(this, new TriggerEventArgs(col));
  }

  public IEnumerator WaitForTriggerStay(EventArgsCondition<TriggerEventArgs> condition, Condition c = null) {
    bool matched = false;
    EventHandler<TriggerEventArgs> handler = null;

    handler = (a, b) => {
      if (condition(b)) {
        matched = true;
        TriggerStayEvent -= handler;
      }
    };

    TriggerStayEvent += handler;

    while (!matched || (c != null && c()))
      yield return null;
  }

  void OnTriggerExit2D(Collider2D col) {
    if (TriggerExitEvent != null)
      TriggerExitEvent(this, new TriggerEventArgs(col));
  }

  public IEnumerator WaitForTriggerExit(EventArgsCondition<TriggerEventArgs> condition, Condition c = null) {
    bool matched = false;
    EventHandler<TriggerEventArgs> handler = null;

    handler = (a, b) => {
      if (condition(b)) {
        matched = true;
        TriggerExitEvent -= handler;
      }
    };

    TriggerExitEvent += handler;

    while (!matched || (c != null && c()))
      yield return null;
  }
}
