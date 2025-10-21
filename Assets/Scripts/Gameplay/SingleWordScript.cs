using System.Collections;
using System.Collections.Generic;
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
    private GameObject splashDrop;
    private float waveFrequency = 2f;
    private float waveAmplitude = 50f;

    private float waveOffset; // unique offset per object

    // Start is called before the first frame update
    void Start()
    {
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

        Vector2 waveMovement = new Vector2(-currentSpeed * Time.deltaTime, yOffset * Time.deltaTime);
        transform.Translate(waveMovement);

        timeAlive += Time.deltaTime;
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
            //Destroy(gameObject);
        }
    }
}
