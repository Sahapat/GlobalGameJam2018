using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ProgressHub : MonoBehaviour
{
    private float _totalProgress;
    public float totalProgress
    {
        get
        {
            return _totalProgress;
        }
        set
        {
            _totalProgress = (value >= 0) ? value : 0;
            if(_totalProgress >= 100)
            {
                _totalProgress = 100;
                completeObject.SetActive(true);
            }
            else
            {
                completeObject.SetActive(false);
            }
            slider.value = _totalProgress;
            showProgress.text = _totalProgress + "%";
        }
    }
    private Slider slider;
    [SerializeField]
    private GameObject completeObject;
    [SerializeField]
    private Text showProgress;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        totalProgress = 0;
    }
}
