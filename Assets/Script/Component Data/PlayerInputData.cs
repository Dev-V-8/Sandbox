using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct PlayerInputData : IComponentData
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
}
