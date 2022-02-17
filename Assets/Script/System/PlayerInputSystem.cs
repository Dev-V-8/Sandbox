using Unity.Entities;
using Unity.Jobs;
using UnityEngine;


[AlwaysSynchronizeSystem]
public class PlayerInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach((ref PlayerMovementData movementData, in PlayerInputData inputData) =>
        {
            Vector2 direction = new Vector2();
            float rotationDirection = 0f;
            movementData.direction2 = 0f;
            movementData.direction = Vector2.zero;
           
            if (Input.GetKey(inputData.left))
            {
                rotationDirection = 1.0f;
            } else if (Input.GetKey(inputData.right))
            {
                rotationDirection = -1.0f;
            }

            if (Input.GetKey(inputData.up))
            {
                movementData.direction2 = 1;
            }
            else if (Input.GetKey(inputData.down))
            {
                movementData.direction2 = -1;
            }

            movementData.direction = direction;
            movementData.rotationDirection = rotationDirection;
        }).Run();
        
        return default;
    }
}
