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

    private Image gamestart;
    private Image about;
    private Image quit;

    private Animator gameStartAnim;
    private Animator aboutAnim;
    private Animator quitAnim;

    [SerializeField]
    private Color unSelectedColor;

    [SerializeField]
    private Color SelectedColor;

    [SerializeField]
    private int selected;

    private void Awake()
    {
        gamestart = gameStartObj.GetComponent<Image>();
        about = aboutObj.GetComponent<Image>();
        quit = quitObj.GetComponent<Image>();

        gameStartAnim = gameStartObj.GetComponent<Animator>();
        aboutAnim = aboutObj.GetComponent<Animator>();
        quitAnim = quitObj.GetComponent<Animator>();

        selected = 0;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
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
        else if(Input.GetKeyDown(KeyCode.UpArrow))
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
        
        if(Input.GetKeyDown(KeyCode.KeypadEnter)||Input.GetKeyDown(KeyCode.Space))
        {
            switch(selected)
            {
                case 0:
                    gameStartAnim.SetBool("isClick", true);
                    StartCoroutine(WaitAndLoad(1.5f));
                    break;
                case 1:
                    aboutAnim.SetBool("isClick", true);
                    StartCoroutine(WaitAndChange(1.5f));
                    break;
                case 2:
                    quitAnim.SetBool("isClick", true);
                    StartCoroutine(WaitAndChange(1f));
                    break;
            }
        }
        setColorToSelected();
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
    IEnumerator WaitAndLoad(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }
    IEnumerator WaitAndChange(float time)
    {
        yield return new WaitForSeconds(time);
        print("Finish");
    }
    IEnumerator WaitAndQuit(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
        print("Finish");
    }
}
