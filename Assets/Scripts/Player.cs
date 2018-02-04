using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInputController
{
    public static bool getCollectButton(byte player, bool isJoy)
    {
        if (isJoy)
        {
            return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button3) : Input.GetKeyDown(KeyCode.Joystick2Button3);
        }
        else
        {
            return (player == 1) ? Input.GetKeyDown(KeyCode.B) : Input.GetKeyDown(KeyCode.KeypadEnter);
        }
    }
    public static bool getPunchButton(byte player, bool isJoy)
    {
        if (isJoy)
        {
            return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button2) : Input.GetKeyDown(KeyCode.Joystick2Button2);
        }
        else
        {
            return (player == 1) ? Input.GetKeyDown(KeyCode.Space) : Input.GetKeyDown(KeyCode.KeypadPlus);
        }
    }
    public static bool getUseButton1(byte player, bool isJoy)
    {
        if (isJoy)
        {
            return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button1) : Input.GetKeyDown(KeyCode.Joystick2Button1);
        }
        else
        {
            return (player == 1) ? Input.GetKeyDown(KeyCode.Space) : Input.GetKeyDown(KeyCode.Keypad5);
        }
    }
    public static bool getUseButton2(byte player)
    {
        return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button5) : Input.GetKeyDown(KeyCode.Joystick2Button5);
    }
    public static bool getDiscardButton(byte player, bool isJoy)
    {
        if (isJoy)
        {
            return (player == 1) ? Input.GetKeyDown(KeyCode.Joystick1Button0) : Input.GetKeyDown(KeyCode.Joystick2Button0);
        }
        else
        {
            return (player == 1) ? Input.GetKeyDown(KeyCode.V) : Input.GetKeyDown(KeyCode.KeypadMinus);
        }
    }
    public static bool getStartButton()
    {
        return (Input.GetKeyDown(KeyCode.Joystick1Button9) || Input.GetKeyDown(KeyCode.Joystick2Button9)) ? true : false;
    }
    public static bool getBackButton()
    {
        return (Input.GetKeyDown(KeyCode.Joystick1Button10) || Input.GetKeyDown(KeyCode.Joystick2Button10)) ? true : false;
    }
    public static Vector2 getMovement(byte player, bool isJoy)
    {
        if (isJoy)
        {
            float moveX = Input.GetAxis("Joy" + player + "MoveAxisX");
            float moveY = Input.GetAxis("Joy" + player + "MoveAxisY");
            if (moveX != 0 && moveY != 0)
            {
                moveX *= 0.85f;
                moveY *= 0.85f;
            }
            return new Vector2(moveX, moveY);
        }
        else
        {
            float moveX = 0;
            float moveY = 0;

            if (player == 1)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    moveX = 1;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    moveX = -1;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    moveY = 1;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    moveY = -1;
                }
                if (moveX != 0 && moveY != 0)
                {
                    moveX *= 0.85f;
                    moveY *= 0.85f;
                }
                return new Vector2(moveX, moveY);
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    moveX = 1;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    moveX = -1;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    moveY = 1;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    moveY = -1;
                }
                if (moveX != 0 && moveY != 0)
                {
                    moveX *= 0.85f;
                    moveY *= 0.85f;
                }
                return new Vector2(moveX, moveY);
            }
        }
    }
    public static Vector2 getAim(byte player, bool isJoy)
    {
        if (isJoy)
        {
            return new Vector2(Input.GetAxis("Joy" + player + "AimAxisX"), Input.GetAxis("Joy" + player + "AimAxisY"));
        }
        else
        {
            float AxisX = 0;
            float AxisY = 0;

            if (player == 1)
            {
                if (Input.GetKey(KeyCode.L))
                {
                    AxisX = 1;
                }
                else if (Input.GetKey(KeyCode.J))
                {
                    AxisX = -1;
                }

                if (Input.GetKey(KeyCode.K))
                {
                    AxisY = 1;
                }
                else if (Input.GetKey(KeyCode.I))
                {
                    AxisY = -1;
                }

                return new Vector2(AxisX, AxisY);
            }
            else
            {
                if (Input.GetKey(KeyCode.Keypad6))
                {
                    AxisX = 1;
                }
                else if (Input.GetKey(KeyCode.Keypad4))
                {
                    AxisX = -1;
                }

                if (Input.GetKey(KeyCode.Keypad2))
                {
                    AxisY = 1;
                }
                else if (Input.GetKey(KeyCode.Keypad8))
                {
                    AxisY = -1;
                }

                return new Vector2(AxisX, AxisY);
            }
        }
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

    public AudioClip hit;
    public AudioClip glass;
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

    public GameObject effect;
    private Animator effectAnim;
    private Rigidbody2D myRig = null;
    private Vector2 ArrowDirection;
    private EffectChecking effectChecking = null;
    private Inventory inventory = null;
    public GameObject pickupObj = null;
    private GameController gameController = null;
    private ProgressChecker progressChecker = null;
    private Animator PlayerAnim = null;
    private AudioSource audioSource;
    public GameObject bubble;

    private void Awake()
    {
        myRig = GetComponent<Rigidbody2D>();
        effectChecking = EffectObj.GetComponent<EffectChecking>();
        inventory = GetComponent<Inventory>();
        gameController = FindObjectOfType<GameController>();
        progressChecker = ProgressCheckObj.GetComponent<ProgressChecker>();
        effectAnim = effect.GetComponent<Animator>();
        PlayerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isStun = false;
        isHaveStatus = false;
        DirectionX = 0;
        DirectionY = -1;
        bubble.SetActive(!inventory.isSlotAvilable);
    }
    public void setStatus(bool isStun, float speed, float duration)
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
            bubble.SetActive(!inventory.isSlotAvilable);
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
                Vector2 movement = ActionInputController.getMovement(PlayerOrder, gameController.isJoystick) * speed;
                PlayerAnimation(movement.x, movement.y);
                if (!punching)
                {
                    myRig.velocity = movement;
                }
                else
                {
                    myRig.velocity = Vector2.zero;
                }
                ArrowDirection = ActionInputController.getAim(PlayerOrder, gameController.isJoystick);
                float angle = Mathf.Atan2(-ArrowDirection.y, ArrowDirection.x);
                angle *= Mathf.Rad2Deg;

                if (ActionInputController.getPunchButton(PlayerOrder, gameController.isJoystick))
                {
                    if (!punching)
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
                if (ActionInputController.getUseButton1(PlayerOrder, gameController.isJoystick) || ActionInputController.getUseButton2(PlayerOrder))
                {
                    if (use != null)
                    {
                        switch (use.UseType)
                        {
                            case ItemUsedType.Attack:
                                if (effectChecking.targetObject != null)
                                {
                                    use.UseItem(effectChecking.targetObject);
                                    switch (use.objItem)
                                    {
                                        case gameItem.chair:
                                            effectAnim.SetTrigger("ChairSmash");
                                            audioSource.PlayOneShot(hit);
                                            break;
                                        case gameItem.keyboard:
                                            effectAnim.SetTrigger("KeybordeSmash");
                                            audioSource.PlayOneShot(glass);
                                            break;
                                    }
                                    inventory.removeItem();
                                }
                                else if (progressChecker.targetObject != null)
                                {
                                    if (progressChecker.targetObject.GetComponent<Progresser>().playerOrder != PlayerOrder)
                                    {
                                        use.UseItem(progressChecker.targetObject);
                                        switch (use.objItem)
                                        {
                                            case gameItem.chair:
                                                audioSource.PlayOneShot(hit);
                                                effectAnim.SetTrigger("ChairSmash");
                                                break;
                                            case gameItem.keyboard:
                                                audioSource.PlayOneShot(glass);
                                                effectAnim.SetTrigger("KeybordeSmash");
                                                break;
                                        }
                                        inventory.removeItem();
                                    }
                                }
                                break;
                            case ItemUsedType.AttackAim:
                                if (ArrowDirection.x != 0 || ArrowDirection.y != 0)
                                {
                                    use.UseItem(ArrowDirection, this.gameObject);
                                    inventory.removeItem();
                                }
                                break;
                            case ItemUsedType.Boots:
                                if (progressChecker.targetObject != null)
                                {
                                    use.UseItem(progressChecker.targetObject);
                                    inventory.removeItem();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (ActionInputController.getCollectButton(PlayerOrder, gameController.isJoystick) && pickupObj != null)
                {
                    Item pick = pickupObj.GetComponent<Item>();
                    pick.Pickup(this.gameObject);
                    if (pick.isSpecial)
                    {
                        gameController.isSpecialEmpty[pick.onIndex] = true;
                    }
                    else if (pick.isOnRedBull)
                    {
                        gameController.isRedbullEmpty[pick.onIndex] = true;
                    }
                    else
                    {
                        gameController.isCommonEmpty[pick.onIndex] = true;
                    }
                }
                if (ActionInputController.getDiscardButton(PlayerOrder, gameController.isJoystick))
                {
                    inventory.removeItem();
                    Item used = inventory.useItem();
                    Vector3 spawnPoint = transform.position;
                    if (used != null)
                    {
                        switch (used.objItem)
                        {
                            case gameItem.artAsset:
                                Instantiate(gameController.otherSpecial[0], spawnPoint, Quaternion.identity);
                                break;
                            case gameItem.chair:
                                Instantiate(gameController.commonObject[0], spawnPoint, Quaternion.identity);
                                break;
                            case gameItem.ice:
                                Instantiate(gameController.commonObject[1], spawnPoint, Quaternion.identity);
                                break;
                            case gameItem.keyboard:
                                Instantiate(gameController.commonObject[2], spawnPoint, Quaternion.identity);
                                break;
                            case gameItem.penpad:
                                Instantiate(gameController.commonObject[3], spawnPoint, Quaternion.identity);
                                break;
                            case gameItem.redbull:
                                Instantiate(gameController.otherSpecial[2], spawnPoint, Quaternion.identity);
                                break;
                            case gameItem.usb:
                                Instantiate(gameController.otherSpecial[1], spawnPoint, Quaternion.identity);
                                break;
                            case gameItem.virus:
                                Instantiate(gameController.otherSpecial[3], spawnPoint, Quaternion.identity);
                                break;
                        }
                    }
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
        PlayerAnim.SetBool("isPunch", punching);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("items"))
        {
            AttackPenPad pen = collision.GetComponent<AttackPenPad>();
            AttackIce ice = collision.GetComponent<AttackIce>();

            if (pen != null)
            {
                if (pen.isShoot && pen.whoUse != PlayerOrder)
                {
                    effectAnim.SetTrigger("Star");
                    audioSource.PlayOneShot(hit);
                }
            }
            else if (ice != null)
            {
                if (ice.isShoot && ice.whoUse != PlayerOrder)
                {
                    effectAnim.SetTrigger("Water");
                    audioSource.PlayOneShot(glass);
                }
            }
        }
    }
}
