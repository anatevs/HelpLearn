using Mirror;
using UnityEngine;

public class TestPlayer : NetworkBehaviour
{
    private void Update()
    {
        if (isLocalPlayer)
        {
            Debug.Log(name);
        }
    }
}
