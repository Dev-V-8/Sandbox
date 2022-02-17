using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PlayerMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        Entities.ForEach((ref Translation trans, ref PhysicsVelocity velocity,ref Rotation rotation,ref LocalToWorld localToWorld, in PlayerMovementData moveData) =>
        {
            Vector3 forward = new Vector3(0, 0, 1);

            rotation.Value = math.mul(rotation.Value, quaternion.RotateZ(math.radians(moveData.rotationDirection * deltaTime * moveData.rotationSpeed)));

            float3 direction = moveData.speed * deltaTime * forward;
            float2 newVel = velocity.Linear.xy;
  
            Debug.DrawLine(localToWorld.Position, localToWorld.Position + localToWorld.Up * 10f, Color.red);
            //Wrap
            velocity.Linear += (localToWorld.Up * moveData.speed * deltaTime * moveData.direction2);

        }).Run();

        return default;
    }
}
