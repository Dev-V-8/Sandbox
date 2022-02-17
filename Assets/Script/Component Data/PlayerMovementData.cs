using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct PlayerMovementData : IComponentData
{
    public Vector2 direction;
    public float direction2;
    public float rotationDirection;
    public float speed;
    public float rotationSpeed;
}
