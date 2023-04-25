using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject time;
    private float level = 0;
    //private float timeDiff;
    


    public void startTimer()
    {
        level = 0;
        this.gameObject.SetActive(true);
        StartCoroutine(timer());
    }

    public int stopTimer()
    {
        this.gameObject.SetActive(false);
        StopCoroutine(timer());

        Debug.Log(level);

        /* check for percision */
        //if (level >= .8f && level <= 1f) {
        //    level = 1;
        //} else if (level >= 1.8f && level <= 2f) {
        //    level = 2;
        //} else if (level >= 2.8f && level <= 3f) {
        //    level = 3;
        //} else { /* not in any range */
        //    level = 0;
        //}
        return (int) Mathf.RoundToInt(level);
    }

    public IEnumerator timer()
    {
        while (time.transform.localScale.x <= 3.1f)
        {
            time.transform.localScale = new Vector3(level += .1f, time.transform.localScale.y);
            yield return new WaitForSeconds(.5f);
        }
    }
}
