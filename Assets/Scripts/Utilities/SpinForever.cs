using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinForever : MonoBehaviour
{
    [SerializeField] private float speed = 180;
    [SerializeField] private Vector3 rotation = Vector3.up;

    public void SetSpeed(float newSpeed) => speed = newSpeed;

    public void SetAxis(Vector3 axis) => rotation = axis;
    
    void Update() => transform.Rotate(rotation * (speed * Time.deltaTime), Space.Self);
}
