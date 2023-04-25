using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnRange : MonoBehaviour
{
    public Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
        StartCoroutine(WaitThenStart());
    }

    public IEnumerator WaitThenStart()
    {
        yield return new WaitForSeconds(0.1f);
        collider.enabled = true;
    }

}
