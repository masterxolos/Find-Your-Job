    using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tabtale.TTPlugins;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking.Match;

public class Collector : MonoBehaviour
{
    [Header("Everything")]
    [SerializeField] private GameObject everything;
    [Header("Values")]
    [SerializeField] private int collectedValue = 0;
    [SerializeField] private int barCount = 0;
    private int maxValue = 30;
    [SerializeField] private float durationToTurnRight = 5;
    [SerializeField] private int money;
    private bool defineJob;

    [SerializeField] private GameObject winCanvas;
    
    [Header("Slider")]
    public Slider slider;
    public Image fill;
    
    [Header("Text")]
    public TextMeshProUGUI textBox;
    public TextMeshProUGUI cashTextBox;
    private GameObject _currentPlayer;
    
    [Header("Gate / Player Object")] 
    [SerializeField] private GameObject unemployedObject;
    [SerializeField] private GameObject medStudentObject;
    [SerializeField] private GameObject nurseObject;
    [SerializeField] private GameObject doctorObject;

    [Header("Particles")]
    [SerializeField] private GameObject likeParticle;
    [SerializeField] private GameObject dislikeParticle;
    [SerializeField] private GameObject healthParticle;

    
    private bool is1worked = false;
    private bool is2worked = false;
    private bool is3worked = false;
    private bool is4worked = false;

    private void Start()
    {
        UpdateSlider();
    }

    void Update()
    {
    }

    private void Awake()
    {
        // Initialize CLIK Plugin
        TTPCore.Setup();
        // Your code here
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tech"))
        {
            collectedValue = collectedValue -5 ;
            Destroy(other.gameObject);
            UpdateSlider();
        }
        else if (other.CompareTag("laptop"))
        {
            collectedValue = collectedValue -5 ; ;
            Destroy(other.gameObject);
            UpdateSlider();
        }
        else if (other.CompareTag("Book"))
        {
            collectedValue += 5;
            Destroy(other.gameObject);
            UpdateSlider();
        }
        else if (other.CompareTag("healthKit"))
        {
            collectedValue += 5;
            Destroy(other.gameObject);
            UpdateSlider();
        }

        else if (other.CompareTag("FinishLine"))
        {
            defineJob = true;
        }
        else if (other.CompareTag("Money"))
        {
            Destroy(other.gameObject);
            money+=5;
            cashTextBox.text =""+ money;
        }
        else if (other.CompareTag("Gate"))
        {
            Destroy(other.gameObject.transform.GetChild(0).gameObject);
            collectedValue += 20;
            UpdateSlider();
        }

        else if (other.CompareTag("turnRight"))
        {
            everything.transform.DORotate(new Vector3(0, -90, 0), 1.5f);
            //var pos = gameObject.transform.position - new Vector3(-18, 0, 0);
           // gameObject.transform.DOMove(pos,3);
            //gameObject.transform.DOMoveX(transform.position.x-8,5);
            // StartCoroutine(StopMovement());
        }
        else if (other.CompareTag("CPR"))
        {
            _currentPlayer.GetComponent<Animator>().SetBool("cpr", true);
            gameObject.GetComponent<Movement>().enabled = false;
        }
    }

    IEnumerator Spin()
    {
        Debug.Log("worked");
        _currentPlayer.GetComponent<Animator>().SetBool("spin",true);
        yield return new WaitForSeconds(1.10f);
        _currentPlayer.GetComponent<Animator>().SetBool("spin",false);
    }

    private void ChangeCloth()
    {
        if (barCount == 3)
        {
            textBox.text = "Doctor";
            nurseObject.SetActive(false);
            doctorObject.SetActive(true);
            _currentPlayer = doctorObject;
            StartCoroutine(Spin());
        }
        else if (barCount == 2)
        {
            textBox.text = "Nurse";
            medStudentObject.SetActive(false);
            nurseObject.SetActive(true);
            _currentPlayer = nurseObject;
            StartCoroutine(Spin());
        }
        else if (barCount==1)
        {
            textBox.text = "Med Student";
            unemployedObject.SetActive(false);
            medStudentObject.SetActive(true);
            _currentPlayer = medStudentObject;
            StartCoroutine(Spin());
        }
       
    }
    

    private void UpdateSlider()
    {
        if (collectedValue >= 30)
        {
            collectedValue = 0;
            barCount++;
            ChangeCloth();
        }
        slider.maxValue = maxValue;
        slider.value = collectedValue;
        fill.fillAmount = slider.value;
        
    }
    private IEnumerator StopMovement()
    {
        //yield return new WaitForSeconds(1);
        gameObject.GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Movement>().enabled = true;

    }
}