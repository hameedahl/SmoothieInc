using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public bool arrived = false;
    public DriverNetwork drivernet;
    public OrderFinder of;

    public GameObject dest;
    public Transform target;

    void Start()
    {
        StartCoroutine(WaitOrder());
    }

    public void NewOrder()
    {
        int diff = Random.Range(1,10);
        dest = of.Find(diff);
        dest.transform.GetChild(0).gameObject.GetComponent<DropZone>().SetCurrent(true);
        target.position = dest.transform.GetChild(0).position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Destination" && col.gameObject.transform.parent.gameObject == dest)
        {
            Debug.Log("Arrived");
            dest.transform.GetChild(0).gameObject.GetComponent<DropZone>().SetCurrent(false);
            arrived = true;
            drivernet.Arrive();
            NewOrder();
        }
    }

    public IEnumerator WaitOrder()
    {
        yield return new WaitForSeconds(0.1f);
        NewOrder();
    }
}
