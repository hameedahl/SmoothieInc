using Unity.Netcode;
using UnityEngine;

public class DriverNetwork : NetworkBehaviour
{

    public TruckManager arrived;

    public NetworkVariable<bool> networkArrived = new NetworkVariable<bool>(
        value:false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    // Start is called before the first frame update
    public void Arrive()
    {
        networkArrived.Value = arrived;
    }
}
