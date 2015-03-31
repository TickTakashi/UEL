using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UEL {
  interface ISubject {
    void NotifyAll();
    void Attach(IObserver observer);
    void Detach(IObserver observer);
  }
}
