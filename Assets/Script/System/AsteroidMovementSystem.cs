using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class AsteroidMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref Translation trans, ref PhysicsVelocity velocity,ref LocalToWorld localToWorld, in AsteroidMovementData moveData) =>
        {
            //float directionX = moveData.speed * deltaTime * Input.GetAxis("Horizontal");
            //float directionY = 
           // Vector3 forward = new Vector3(0, 0, 1);

           // rotation.Value = math.mul(rotation.Value, quaternion.RotateZ(math.radians(moveData.rotationDirection * deltaTime * moveData.rotationSpeed)));
            //trans.Value.x += direction.x;
            //trans.Value.y += direction.y;
            float3 direction = moveData.speed * deltaTime * moveData.direction;
            float2 newVel = velocity.Linear.xy;
            //velocity.Linear.xy += rotation.Value.value.z;
            //velocity.Linear.x += moveData.speed * deltaTime * moveData.direction2;
            // trans.Value += math.forward(rotation.Value) * moveData.speed * deltaTime ;
            Debug.DrawLine(localToWorld.Position, localToWorld.Position + direction * 10f, Color.red);
 
            velocity.Linear += (moveData.speed * deltaTime * direction);
          

        }).Run();

        return default;
    }
}
