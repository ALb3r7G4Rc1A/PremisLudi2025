using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleWordScript : MonoBehaviour
{
    private GameManager gameManager;
    public WordsClass wordsClass;
    public bool isRemoved;
    public float timeAlive;
    private float baseSpeed;
    private float currentSpeed;
    private float acceleration = 1f;
    public GameObject splashDrop;
    private float waveFrequency = 2f;
    private float waveAmplitude = 0.5f;

    private float waveOffset; // unique offset per object

    public bool isGoingLeftWord = false;
    private Vector2 waveMovement;


    private TMP_Text tmp;
    private BoxCollider2D col;
    private Rigidbody2D rb;
    public GameObject correctAnswer;
    public GameObject wrongAnswer;
    private bool lowAlpha;

    void AdjustColliderToText()
    {
        tmp = GetComponent<TMP_Text>();
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        tmp.ForceMeshUpdate();
        var bounds = tmp.textBounds; // TMP built-in text bounds
        col.size = bounds.size;
        col.offset = bounds.center;
    }
    // Start is called before the first frame update
    void Start()
    {
        wrongAnswer.SetActive(false);
        correctAnswer.SetActive(false);
        gameManager = FindAnyObjectByType<GameManager>();
        baseSpeed = gameManager.speed;
        currentSpeed = baseSpeed;
        isRemoved = false;
        waveOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRemoved)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, baseSpeed * 10f, Time.deltaTime * acceleration);
        }
        else
        {
            currentSpeed = baseSpeed;
        }
        float yOffset = Mathf.Sin(Time.time * waveFrequency + waveOffset) * waveAmplitude;
        if (isGoingLeftWord)
        {
            waveMovement = new Vector2(-currentSpeed * Time.deltaTime, yOffset * Time.deltaTime);
        }
        else
        {
            waveMovement = new Vector2(currentSpeed * Time.deltaTime, yOffset * Time.deltaTime);
        }
        transform.Translate(waveMovement);

        timeAlive += Time.deltaTime;
        if (lowAlpha && GetComponent<TMP_Text>().alpha > 0.3f)
        {
            GetComponent<TMP_Text>().alpha = Mathf.Lerp(GetComponent<TMP_Text>().alpha,0.3f,Time.deltaTime*2);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("WordLimit"))
        {
            if (!isRemoved)
            {
                gameManager.Remove0();
            }
            splashDrop.GetComponent<ParticleSystem>().Play();
            Instantiate(splashDrop, transform.position, Quaternion.identity, null);
            Destroy(gameObject);
        }
    }
    public void GoingLeftWord(bool sino)
    {
        isGoingLeftWord = sino;
        AdjustColliderToText();
    }

    public void CorrectAnswer(bool correct)
    {
        if (correct)
        {
            correctAnswer.SetActive(true);
        }
        else
        {
            wrongAnswer.SetActive(true);
        }
        lowAlpha = true;
    }
}
