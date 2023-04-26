using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public bool arrived = false;
    public DriverNetwork drivernet;
    public OrderFinder of;
    public BooleanNetworkHandler networkHandler;
    public GameObject dest;
    public Transform target;

    public bool host = false;

    void Start()
    {
        //StartCoroutine(WaitOrder());
    }

    public Vector3 NewOrder()
    {
        networkHandler.SetArrivedStatus(false);
        if(host)
        {
            int diff = Random.Range(1,10);
            dest = of.Find(diff);
            dest.transform.GetChild(0).gameObject.GetComponent<DropZone>().SetCurrent(true);
            target.position = dest.transform.GetChild(0).position;
            return dest.transform.GetChild(0).position;
        }
        return new Vector3(0,0,0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Destination" && col.gameObject.transform.parent.gameObject == dest)
        {
            Debug.Log("Arrived");
            dest.transform.GetChild(0).gameObject.GetComponent<DropZone>().SetCurrent(false);
            networkHandler.SetArrivedStatus(true);
            drivernet.Arrive();
        }
    }

    public IEnumerator WaitOrder()
    {
        yield return new WaitForSeconds(0.1f);
        NewOrder();
    }

    public bool GetArrivedStatus()
    {
      return arrived;
    }
}
