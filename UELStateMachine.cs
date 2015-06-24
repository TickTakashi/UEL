using UnityEngine;
using System.Collections;

namespace UEL {
  // TODO: Add a 2D Version of this
  public abstract class UELStateMachine : UELBehaviour {
    public abstract class UELState {
      public virtual void EnterState() { }
      public virtual void ExitState() { }
      public virtual IEnumerator Run() { yield return null; }
      public virtual void Update() { }
    }

    public class DefaultState : UELState { }

    public UELState current_state;
    private IEnumerator current_state_run;

    public virtual void Awake() {
      if (current_state == null) {
        current_state = InitialState();
        EnterState();
      }
    }

    public abstract UELState InitialState();

    public void Transition(UELState next_state) {
      ExitState();
      current_state = next_state;
      EnterState();
    }

    private void ExitState() {
      StopCoroutine(current_state_run);
      current_state.ExitState();
    }

    private void EnterState() {
      current_state.EnterState();
      current_state_run = current_state.Run();
      StartCoroutine(current_state_run);
    }

    public virtual void Update() {
      current_state.Update(); 
    }
  }
}