using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInputController
{
    public static bool getCollectButton(byte player)
    {
        return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button3) : Input.GetKeyDown(KeyCode.Joystick2Button3);
    }
    public static bool getPunchButton(byte player)
    {
        return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button2) : Input.GetKeyDown(KeyCode.Joystick2Button2);
    }
    public static bool getUseButton1(byte player)
    {
        return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button1) : Input.GetKeyDown(KeyCode.Joystick2Button1);
    }
    public static bool getUseButton2(byte player)
    {
        return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button5) : Input.GetKeyDown(KeyCode.Joystick2Button5);
    }
    public static bool getDiscardButton(byte player)
    {
        return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button0) : Input.GetKeyDown(KeyCode.Joystick2Button0);
    }
    public static Vector2 getMovement(byte player)
    {
        float moveX = Input.GetAxis("Joy" + player + "MoveAxisX");
        float moveY = Input.GetAxis("Joy" + player + "MoveAxisY");
        if(moveX != 0 && moveY != 0)
        {
            moveX *= 0.85f;
            moveY *= 0.85f;
        }
        return new Vector2(moveX,moveY);
    }
    public static Vector2 getAim(byte player)
    {
        return new Vector2(Input.GetAxis("Joy"+player+"AimAxisX"), Input.GetAxis("Joy" + player + "AimAxisY"));
    }
}
public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject ArrowObj;
    [SerializeField]
    private GameObject EffectObj = null;
    [SerializeField]
    private GameObject ProgressCheckObj = null;
    [SerializeField]
    private LayerMask punchMask;

    private float punchTimer = 0;
    public float punchCd = 0.1f;
    private bool punching = false;
    public byte PlayerOrder;
    public float speed = 5f;
    public bool isHaveStatus = false;

    public float DurationStatus;
    public bool isStun;
    private float DirectionX = 0;
    private float DirectionY = 0;
    private bool isRuning = false;

    private Rigidbody2D myRig = null;
    private Vector2 ArrowDirection;
    private EffectChecking effectChecking = null;
    private Inventory inventory = null;
    public GameObject pickupObj = null;
    private GameController gameController = null;
    private ProgressChecker progressChecker = null;
    private Animator PlayerAnim = null;

    private void Awake()
    {
        myRig = GetComponent<Rigidbody2D>();
        effectChecking = EffectObj.GetComponent<EffectChecking>();
        inventory = GetComponent<Inventory>();
        gameController = FindObjectOfType<GameController>();
        progressChecker = ProgressCheckObj.GetComponent<ProgressChecker>();
        PlayerAnim = GetComponent<Animator>();
        isStun = false;
        isHaveStatus = false;
        DirectionX = 0;
        DirectionY = -1;
    }
    public void setStatus(bool isStun,float speed,float duration)
    {
        this.isStun = isStun;
        this.speed += speed;
        this.DurationStatus = duration;
        isHaveStatus = true;
    }
    private void restoreStatus()
    {
        speed = 5f;
        isStun = false;
        isHaveStatus = false;
        DurationStatus = 0;
    }
    private void FixedUpdate()
    {
        if (gameController.isGameStart)
        {
            if (isHaveStatus)
            {
                if (DurationStatus > 0)
                {
                    DurationStatus -= Time.deltaTime;
                }
                else
                {
                    restoreStatus();
                    PlayerAnim.SetBool("isKnockDown", isStun);
                }
            }
            if (!isStun)
            {
                Vector2 movement = ActionInputController.getMovement(PlayerOrder) * speed;
                PlayerAnimation(movement.x, movement.y);
                if (!punching)
                {
                    myRig.velocity = movement;
                }
                else
                {
                    myRig.velocity = Vector2.zero;
                }
                ArrowDirection = ActionInputController.getAim(PlayerOrder);
                float angle = Mathf.Atan2(-ArrowDirection.y, ArrowDirection.x);
                angle *= Mathf.Rad2Deg;

                if (ActionInputController.getPunchButton(PlayerOrder))
                {
                    if(!punching)
                    {
                        Punch();
                    }
                }
                Item use = null;
                use = inventory.useItem();
                if (use != null)
                {
                    if (use.UseType == ItemUsedType.AttackAim)
                    {
                        ArrowObj.SetActive(true);
                        ArrowObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    }
                    else
                    {
                        ArrowObj.SetActive(false);
                    }
                }
                else
                {
                    ArrowObj.SetActive(false);
                }
                if (ActionInputController.getUseButton1(PlayerOrder) || ActionInputController.getUseButton2(PlayerOrder))
                {
                    if (use != null)
                    {
                        switch (use.UseType)
                        {
                            case ItemUsedType.Attack:
                                if (effectChecking.targetObject != null)
                                {
                                    use.UseItem(effectChecking.targetObject);
                                    inventory.removeItem();
                                }
                                else if (progressChecker.targetObject != null)
                                {
                                    if (progressChecker.targetObject.GetComponent<Progresser>().playerOrder != PlayerOrder)
                                    {
                                        use.UseItem(progressChecker.targetObject);
                                        inventory.removeItem();
                                    }
                                }
                                break;
                            case ItemUsedType.AttackAim:
                                use.UseItem(ArrowDirection, this.gameObject);
                                inventory.removeItem();
                                break;
                            case ItemUsedType.Boots:
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (ActionInputController.getCollectButton(PlayerOrder) && pickupObj != null)
                {
                    pickupObj.GetComponent<Item>().Pickup(this.gameObject);
                }
                if (punching)
                {
                    if (punchTimer > 0)
                    {
                        punchTimer -= Time.deltaTime;
                    }
                    else
                    {
                        punching = false;
                        punchTimer = 0;
                        PlayerAnim.SetBool("isPunch", punching);
                    }
                }
            }
            else
            {
                myRig.velocity = new Vector2(0, 0);
                PlayerAnim.SetBool("isKnockDown", isStun);
            }
        }
    }
    private void PlayerAnimation(float x, float y)
    {
        if (x != 0 || y != 0)
        {
            DirectionX = x;
            DirectionY = y;
            isRuning = true;
        }
        else
        {
            isRuning = false;
        }
        PlayerAnim.SetBool("isRuning", isRuning);
        PlayerAnim.SetFloat("MoveX", DirectionX);
        PlayerAnim.SetFloat("MoveY", DirectionY);
    }
    private void Punch()
    {
        punching = true;
        punchTimer = punchCd;
        PlayerAnim.SetBool("isPunch",punching);
    }
}
