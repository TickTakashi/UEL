using System;
using System.Collections.Generic;

// PoissonDisc classes generate random number pairs from a Poisson Disc distribution. They take x and y ranges (exclusive)
// and return pairs of (x, y) of "pleasantly" random value from the given 2D space avoiding under and over sampling.
public abstract class PoissonDisc {
  protected Random rng;
  protected List<Pair<int, int>> generatedValues;
  protected Pair<int, int> xyRange;

  public PoissonDisc(int seed, int xRange, int yRange) {
    rng = new Random(seed);
    xyRange = new Pair<int, int>(xRange, yRange);
    generatedValues = new List<Pair<int, int>>();
  }

  public abstract Pair<int, int> GetNext();

  public Pair<int, int> Get(int index) {
    return generatedValues[index];
  }

  protected double SquareDistance(Pair<int,int> a, Pair<int, int> b) {
    return Math.Sqrt(Math.Pow(a.first - b.first, 2) + Math.Pow(a.second - b.second, 2));
  }
}

public class MitchellsPoissonDisc : PoissonDisc {
  bool isCandidateListFresh;
  List<Pair<int, int>> candidateList;
  int numCandidates;

  public MitchellsPoissonDisc(int seed, int xRange, int yRange, int numCandidates) : base(seed, xRange, yRange) {
    isCandidateListFresh = false;
    this.numCandidates = numCandidates;
    candidateList = new List<Pair<int, int>>();
  }

  public List<Pair<int, int>> GetCandidates() {
    // If we have already generated candidates, just return those.
    // Otherwise we need to generate numCandidates new candidates.
    if (!isCandidateListFresh) {
      candidateList.Clear();
      for (int i = 0; i < numCandidates; i++) {
        candidateList.Add(new Pair<int, int>(rng.Next(xyRange.first), rng.Next(xyRange.second)));
      }

      isCandidateListFresh = true;
    }

    return candidateList;
  }

  public override Pair<int, int> GetNext() {
    // Check the distance from each candidate to its nearest neighbor (in generatedValue).
    // Mark candidates list as no longer fresh.
    // Return the candidate that is furthest from its nearest neighbor.

    List<Pair<int, int>> candidates = GetCandidates();

    Pair<int, int> bestCandidate = null;
    double bestDistance = double.MinValue;

    foreach(Pair<int, int> p in candidates) {
      double distance = GetNearestNeighborDistance(p);
      if (distance > bestDistance) {
        bestCandidate = p;
        bestDistance = distance;
      }
    }

    generatedValues.Add(bestCandidate);
    isCandidateListFresh = false;
    return bestCandidate;
  }


  private double GetNearestNeighborDistance(Pair<int, int> candidate) {
    double distance = double.MaxValue;

    foreach(Pair<int, int> p in generatedValues) {
      double pdist = SquareDistance(p, candidate);
      if (pdist < distance) {
        distance = pdist;
      }
    }

    return distance;
  }
}