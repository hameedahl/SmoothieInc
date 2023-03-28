using Unity.Netcode;
using UnityEngine;

public class DriverNetwork : NetworkBehaviour
{
    public NetworkVariable<bool> arrived = new NetworkVariable<bool>(
        value:false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    // Start is called before the first frame update
    public void arrive()
    {
        arrived.Value = true;
    }
}
