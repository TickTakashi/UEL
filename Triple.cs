public class Triple<X, Y, Z> {
  private X _x;
  private Y _y;
  private Z _z;

  public Triple(X first, Y second, Z third) {
    _x = first;
    _y = second;
    _z = third;
  }

  public X first { get { return _x; } }
  public X x { get { return _x; } }

  public Y second { get { return _y; } }
  public Y y { get { return _y; } }

  public Z third { get { return _z; } }
  public Z z { get { return _z; } }

  public override bool Equals(object obj) {
    if (obj == null)
      return false;
    if (obj == this)
      return true;
    Triple<X, Y, Z> other = obj as Triple<X, Y, Z>;
    if (other == null)
      return false;

    return
        (((first == null) && (other.first == null))
            || ((first != null) && first.Equals(other.first)))
          &&
        (((second == null) && (other.second == null))
            || ((second != null) && second.Equals(other.second)))
          &&
        (((third == null) && (other.third == null))
            || ((third != null) && third.Equals(other.third)));

  }

  public override int GetHashCode() {
    int hashcode = 0;
    if (first != null)
      hashcode += first.GetHashCode();
    if (second != null)
      hashcode += second.GetHashCode();
    if (third != null)
      hashcode += third.GetHashCode();
    return hashcode;
  }

  public Triple<X, Y, Z> Clone() {
    return new Triple<X, Y, Z>(first, second, third);
  }
}