using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject gameStartObj;
    [SerializeField]
    private GameObject aboutObj;
    [SerializeField]
    private GameObject quitObj;
    [SerializeField]
    private GameObject FadeObj;
    [SerializeField]
    private GameObject MainMenuObj;
    [SerializeField]
    private GameObject AboutObj;

    private Image gamestart;
    private Image about;
    private Image quit;

    private Animator gameStartAnim;
    private Animator aboutAnim;
    private Animator quitAnim;
    private Animator fadeAnim;

    [SerializeField]
    private Color unSelectedColor;

    [SerializeField]
    private Color SelectedColor;

    [SerializeField]
    private int selected;

    private bool isOnMainMenu;

    private WaitForSeconds transitionTime;

    private void Awake()
    {
        gamestart = gameStartObj.GetComponent<Image>();
        about = aboutObj.GetComponent<Image>();
        quit = quitObj.GetComponent<Image>();

        gameStartAnim = gameStartObj.GetComponent<Animator>();
        aboutAnim = aboutObj.GetComponent<Animator>();
        quitAnim = quitObj.GetComponent<Animator>();
        fadeAnim = FadeObj.GetComponent<Animator>();
        transitionTime = new WaitForSeconds(1f);

        selected = 0;
        isOnMainMenu = true;
    }
    private void Start()
    {
        SetContent(); fadeAnim.SetBool("isFadeOut", false);
    }
    private void Update()
    {
        if (isOnMainMenu)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (selected < 2)
                {
                    selected += 1;
                }
                else
                {
                    selected = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (selected > 0)
                {
                    selected -= 1;
                }
                else
                {
                    selected = 2;
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
            {
                isOnMainMenu = false;
                switch (selected)
                {
                    case 0:
                        gameStartAnim.SetBool("isClick", true);
                        fadeAnim.SetBool("isFadeOut", true);
                        StartCoroutine(WaitAndLoad());
                        break;
                    case 1:
                        aboutAnim.SetBool("isClick", true);
                        fadeAnim.SetBool("isFadeOut", true);
                        StartCoroutine(WaitAndChange());
                        break;
                    case 2:
                        quitAnim.SetBool("isClick", true);
                        fadeAnim.SetBool("isFadeOut", true);
                        StartCoroutine(WaitAndQuit());
                        break;
                }
            }
            setColorToSelected();
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                isOnMainMenu = true;
                fadeAnim.SetBool("isFadeOut", true);
                StartCoroutine(WaitAndFadeIn());
            }
        }
    }
    private void setColorToSelected()
    {
        switch(selected)
        {
            case 0:
                gamestart.color = SelectedColor;
                about.color = unSelectedColor;
                quit.color = unSelectedColor;
                break;
            case 1:
                gamestart.color = unSelectedColor;
                about.color = SelectedColor;
                quit.color = unSelectedColor;
                break;
            case 2:
                gamestart.color = unSelectedColor;
                about.color = unSelectedColor;
                quit.color = SelectedColor;
                break;
            default:
                gamestart.color = unSelectedColor;
                about.color = unSelectedColor;
                quit.color = unSelectedColor;
                break;
        }
    }
    private void SetContent()
    {
        MainMenuObj.SetActive(isOnMainMenu);
        AboutObj.SetActive(!isOnMainMenu);
    }
    IEnumerator WaitAndLoad()
    {
        yield return transitionTime;
        SceneManager.LoadScene(1);
    }
    IEnumerator WaitAndChange()
    {
        yield return transitionTime;
        fadeAnim.SetBool("isFadeOut", false);
        SetContent();
        fadeAnim.SetTrigger("FadeIn");
    }
    IEnumerator WaitAndFadeIn()
    {
        yield return transitionTime;
        fadeAnim.SetBool("isFadeOut", false);
        SetContent();
        fadeAnim.SetTrigger("FadeIn");
    }
    IEnumerator WaitAndQuit()
    {
        yield return transitionTime;
        Application.Quit();
    }
}
