using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
  public class FocusCameraTarget : MonoBehaviour {
    [SerializeField] private float _DistanceFromTarget;
    
    public float DistanceFromTarget => _DistanceFromTarget;
  }
}
