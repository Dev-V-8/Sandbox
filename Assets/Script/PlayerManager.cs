using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.IsGameOver())
        {
            return;
        }
    }
}