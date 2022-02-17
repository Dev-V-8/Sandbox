using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
[AlwaysSynchronizeSystem]
public class WrappingSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float edgeBuffer = 50f;
        float screenHeight = Screen.height + edgeBuffer;
        float screenWidth = Screen.width + edgeBuffer;
        Camera cam = Camera.main;
        bool isWrappingX = false;
        bool isWrappingY = false;

        Entities.ForEach((ref Translation trans, ref PhysicsVelocity velocity, ref Rotation rotation, ref LocalToWorld localToWorld) =>
        {
            Vector2 viewPortPosition = cam.WorldToViewportPoint(trans.Value);
            float3 newPosition = trans.Value;

            bool isVisible = (viewPortPosition.x > 0 && viewPortPosition.x < 1) &&
            (viewPortPosition.y > 0 && viewPortPosition.y < 1);

            if (isVisible)
            {
                isWrappingX = false;
                isWrappingY = false;
                return;
            }

            if (isWrappingX && isWrappingY)
            {
                return;
            }

            if (!isWrappingX && (viewPortPosition.x > 1 || viewPortPosition.x < 0))
            {
                float offset = viewPortPosition.x > 1 ? 1 : -1;
                newPosition.x = -newPosition.x + offset;

                isWrappingX = true;
            }

            if (!isWrappingY && (viewPortPosition.y > 1 || viewPortPosition.y < 0))
            {
                float offset = viewPortPosition.y > 1 ? 1 : -1;
                newPosition.y = -newPosition.y + offset;

                isWrappingY = true;
            }

            trans.Value = newPosition;
        }).WithoutBurst().Run();

        return default;
    }

  
}
