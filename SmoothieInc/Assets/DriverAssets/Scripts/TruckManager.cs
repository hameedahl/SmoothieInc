using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public bool arrived = false;
    bool playEffect = true;
    public DriverNetwork drivernet;
    public OrderFinder of;
    public BooleanNetworkHandler networkHandler;
    public GameObject dest;
    public Transform target;
    public CarController controller;
    public GameObject arriveEffect;

    public bool host = false;

    void Start()
    {
        //StartCoroutine(WaitOrder());
    }

    public Vector3 NewOrder(int diff)
    {
        networkHandler.SetArrivedStatus(false);
        if(host)
        {
            //int diff = Random.Range(1,10);
            playEffect = true;
            dest = of.Find(diff);
            of.DisableCol();
            dest.transform.GetChild(0).gameObject.GetComponent<DropZone>().SetCurrent(true);
            target.position = dest.transform.GetChild(0).position;
            networkHandler.SetDestination(dest.transform.GetChild(0).position);
            return dest.transform.GetChild(0).position;
        }
        return new Vector3(0,0,0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Destination" && col.gameObject.transform.parent.gameObject == dest)
        {
            if(playEffect)
            {
                Instantiate(arriveEffect, col.gameObject.transform.position, new Quaternion(-1f,0,0,1));
                playEffect = false;
            }
            Debug.Log("Arrived");
            dest.transform.GetChild(0).gameObject.GetComponent<DropZone>().SetCurrent(false);
            networkHandler.SetArrivedStatus(true);
            drivernet.Arrive();
        }
    }

    public void EnableCol()
    {
        of.EnableCol();
    }

    // public IEnumerator WaitOrder()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     NewOrder();
    // }

    public bool GetArrivedStatus()
    {
      return arrived;
    }
}
