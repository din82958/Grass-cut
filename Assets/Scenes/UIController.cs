using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI panel References")] 
    [SerializeField]  Button playBtn;
    [SerializeField]  GameObject canvas;
    [SerializeField]  GameObject cutter;
    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        canvas.SetActive(false);
        cutter.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
