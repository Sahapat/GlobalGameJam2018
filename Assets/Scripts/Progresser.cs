using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RequestItem
{
    ArtAsset,
    data,
    RedBull,
    None
};

public class Progresser : MonoBehaviour
{
    [SerializeField]
    private GameObject ProgressHubOBJ;
    [SerializeField]
    private GameObject RequestObj;

    public byte playerOrder;

    public byte progressStatus;
    private WaitForSeconds[] level;
    private GameController gameController;

    public GameObject[] spawnRequest;
    public bool isHaveStatus;
    public bool isStun;
    public float duration;
    public bool isStartProgress;
    public bool canProgress;
    public byte stopRemain = 2;
    public float stopProgress;
    public bool isWorking;
    public bool isGetTranmission;

    public RequestItem itemRequest = RequestItem.None;

    private Animator anim;
    public ProgressHub progressHub = null;
    private RequestDisplay display = null;
    private GameController gamecontroller = null;
    private bool alreadySpawn = false;
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
        anim = GetComponent<Animator>();
        gamecontroller = FindObjectOfType<GameController>();
        display = RequestObj.GetComponent<RequestDisplay>();
        stopProgress = 0;
        progressStatus = 0;
        isStartProgress = true;
        isWorking = true;
        
        anim.SetBool("isWorking", isWorking);
    }
    private void Start()
    {
        randomRequest();
    }
    private void Update()
    {
        if(gameController.isGameStart)
        {
            anim.SetBool("isWorking", isWorking);
            RequestObj.SetActive(!canProgress);
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
            if(progressHub.totalProgress > stopProgress)
            {
                if(!isGetTranmission)
                {
                    progressHub.totalProgress = (int)stopProgress;
                    canProgress = false;
                    int spawnIndex = 0;
                    if (!alreadySpawn)
                    {
                        for (int i = 0; i < gamecontroller.isSpecialEmpty.Length; i++)
                        {
                            if (gamecontroller.isSpecialEmpty[i])
                            {
                                spawnIndex = i;
                            }
                        }
                        Vector3 spawn = gamecontroller.specialSpawn[spawnIndex].position;
                        GameObject temp;
                        switch (itemRequest)
                        {
                            case RequestItem.ArtAsset:
                                temp = Instantiate(spawnRequest[0], spawn, Quaternion.identity);
                                break;
                            case RequestItem.data:
                                temp = Instantiate(spawnRequest[1], spawn, Quaternion.identity);
                                break;
                            case RequestItem.RedBull:
                                temp = Instantiate(spawnRequest[2], spawn, Quaternion.identity);
                                break;
                        }
                        gameController.isSpecialEmpty[spawnIndex] = false;
                        alreadySpawn = true;
                    }
                }
                else
                {
                    if(stopRemain> 0)
                    {
                        randomRequest();
                    }
                    else
                    {
                        stopProgress = 110;
                        canProgress = true;
                        isGetTranmission = false;
                        alreadySpawn = false;
                    }
                }
            }
            if(progressStatus > 3 || isStun || !canProgress)
            {
                isWorking = false;
            }
            else
            {
                isWorking = true;
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }
    private void restoreStatus()
    {
        isStun = false;
        progressStatus = 0;
        duration = 0;
        isHaveStatus = false;
    }
    public void sendTranmission()
    {
        isGetTranmission = true;
    }
    public void setStatus(bool isStun, byte progressStatus, float duration)
    {
        this.isStun = isStun;
        this.progressStatus =progressStatus;
        this.duration = duration;
        isHaveStatus = true;
        isStartProgress = true;
    }
    private void randomRequest()
    {
        int max = (progressHub.totalProgress < 50) ? 50 : 100;
        stopProgress = Random.Range(progressHub.totalProgress, max);
        byte item = (byte)Random.Range(0, 3);
        switch (item)
        {
            case 0:
                itemRequest = RequestItem.ArtAsset;
                break;
            case 1:
                itemRequest = RequestItem.data;
                break;
            case 2:
                itemRequest = RequestItem.RedBull;
                break;
            default:
                itemRequest = RequestItem.RedBull;
                break;
        }
        display.showRequest(itemRequest);
        stopRemain -= 1;
        canProgress = true;
        isGetTranmission = false;
        alreadySpawn = false;
    }
}
