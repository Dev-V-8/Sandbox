using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class DestructionSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (GameManager.IsGameOver()) return;

        float3 playerPos = float3.zero;
        //Player should not be an entity
        Entities.WithAll<PlayerTag>().ForEach((ref Translation trans) =>
        {
            playerPos = trans.Value;
        });

        Entities.WithAll<AsteroidMovementData>().ForEach((Entity enemy, ref Translation asteroidPos) =>
        {
            if (math.distance(asteroidPos.Value, playerPos) <= 2)
            {
                //GameManager.EndGame();
                //Test destroy
                PostUpdateCommands.DestroyEntity(enemy);
            }
        });
    }

}
