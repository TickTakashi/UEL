using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UEL {
  public static class CSharpExtensionMethods {
    public delegate B Func<A, B>(A a);

    public static int BinarySearchForMatch<T>(this IList<T> list,
        Func<T, int> comparer) {
      int min = 0;
      int max = list.Count - 1;

      while (min <= max) {
        int mid = (min + max) / 2;
        int comparison = comparer(list[mid]);
        if (comparison == 0) {
          return mid;
        }
        if (comparison < 0) {
          min = mid + 1;
        } else {
          max = mid - 1;
        }
      }
      return ~min;
    }
  }
}