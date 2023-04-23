using UnityEngine;
using UnityEngine.UI;

public class OrderUIManager : MonoBehaviour
{
    public Text orderInfoText;
    public GameObject orderPanel;
    public GameHandler gameHandler;

    void Start()
    {
        orderPanel.SetActive(false);
    }

    public void UpdateOrderInfo()
    {
        string orderInfo = "New Order:\n";
        int[] valuesArray = gameHandler.GetValuesArray();

        for (int i = 0; i < valuesArray.Length; i++)
        {
            orderInfo += valuesArray[i].ToString() + ", ";
        }

        orderInfoText.text = orderInfo;
        orderPanel.SetActive(true);
    }

    public void HideOrderInfo()
    {
        orderPanel.SetActive(false);
    }
}
