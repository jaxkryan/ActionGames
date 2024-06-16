using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Text txtTime;
    [SerializeField]
    int TotalTime = 60;
    private float time = 0;
    [SerializeField]
    Text txtPause;
    private bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        txtTime.text = "Rescue the prince: " + TotalTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time>=1)
        {
            TotalTime--;
            txtTime.text = "Rescue the prince: " + TotalTime.ToString();
            time = 0;
        }
        if (TotalTime <= 0)
        {
            txtTime.text = "Rescue the prince: 0";
            Time.timeScale = 0;
        }
    }

    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            txtPause.text = "Resume";
            Time.timeScale = 0;
        }
        else
        {
            txtPause.text = "Pause";
            Time.timeScale = 1;
        }
    }
}
