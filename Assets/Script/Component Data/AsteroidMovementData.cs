using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct AsteroidMovementData : IComponentData
{
    public float3 direction;
    public float speed;
}
