using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progresser : MonoBehaviour
{
    [SerializeField]
    private GameObject ProgressHubOBJ;

    public byte playerOrder;

    public byte progressStatus;
    private WaitForSeconds[] level;
    private GameController gameController;

    public bool isHaveStatus;
    public bool isStun;
    public float duration;
    public bool isStartProgress;

    private ProgressHub progressHub = null;

    IEnumerator progressing(byte level)
    {
        while(true)
        {
            progressHub.totalProgress += 1;
            yield return this.level[level];
        }
    }
    private void Awake()
    {
        level = new WaitForSeconds[3];
        level[0] = new WaitForSeconds(1f);
        level[1] = new WaitForSeconds(0.5f);
        level[2] = new WaitForSeconds(0.2f);

        gameController = FindObjectOfType<GameController>();
        progressHub = ProgressHubOBJ.GetComponent<ProgressHub>();
        progressStatus = 0;
        isStartProgress = true;
    }

    private void Update()
    {
        if(gameController.isGameStart)
        {
            if (isHaveStatus)
            {
                if (duration > 0)
                {
                    duration -= Time.deltaTime;
                }
                else
                {
                    restoreStatus();
                }
            }
            if (progressStatus <= 3)
            {
                if (isStartProgress)
                {
                    StopAllCoroutines();
                    StartCoroutine(progressing(progressStatus));
                    isStartProgress = false;
                }
            }
            else
            {
                StopAllCoroutines();
                isStartProgress = true;
            }
        }
    }
    private void restoreStatus()
    {
        isStun = false;
        progressStatus = 0;
        duration = 0;
        isHaveStatus = false;
    }
    public void setStatus(bool isStun, byte progressStatus, float duration)
    {
        this.isStun = isStun;
        this.progressStatus += progressStatus;
        this.duration = duration;
        isHaveStatus = true;
        isStartProgress = true;
    }
}
