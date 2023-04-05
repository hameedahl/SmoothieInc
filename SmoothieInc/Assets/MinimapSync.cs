using UnityEngine;
using Unity.Netcode;

public class MinimapSync : NetworkBehaviour
{
    [SerializeField] private RenderTexture minimapRenderTexture;
    private Texture2D minimapTexture2D;
    private byte[] minimapBytes;

    public Texture2D MinimapTexture2D => minimapTexture2D;

    private void Start()
    {
        minimapTexture2D = new Texture2D(minimapRenderTexture.width, minimapRenderTexture.height);
    }

    private void Update()
    {
        if (IsServer)
        {
            RenderTextureToTexture2D(minimapRenderTexture, minimapTexture2D);
            minimapBytes = minimapTexture2D.EncodeToJPG();
            MinimapUpdateServerRpc(minimapBytes);
        }
    }

    private void RenderTextureToTexture2D(RenderTexture renderTexture, Texture2D texture2D)
    {
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;
    }

    [ServerRpc]
    private void MinimapUpdateServerRpc(byte[] minimapData)
    {
        MinimapUpdateClientRpc(minimapData);
    }

    [ClientRpc]
    private void MinimapUpdateClientRpc(byte[] minimapData)
    {
        if (!IsServer)
        {
            minimapTexture2D.LoadImage(minimapData);
        }
    }
}
