using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject time;
    private float level = 0;
    public GameHandler gameHandler;

    public void startTimer()
    {
        level = 0;
        time.transform.localScale = new Vector3(level, time.transform.localScale.y);
        this.gameObject.SetActive(true);
        StartCoroutine(timer());
    }

    public void stopTimer()
    {
        this.gameObject.SetActive(false);
        StopCoroutine(timer());
        gameHandler.blenderLevel = level;
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
