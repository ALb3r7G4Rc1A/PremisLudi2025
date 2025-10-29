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
    public bool isResolved;
    public Transform pointsBox;
    public int points;

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
        pointsBox = GameObject.FindGameObjectWithTag("PointsBox").transform;
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
            //currentSpeed = Mathf.Lerp(currentSpeed, baseSpeed * 10f, Time.deltaTime * acceleration);
            currentSpeed = baseSpeed * 10f;
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
        if (isResolved)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointsBox.position, baseSpeed * 10 * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 0.5f);
        }
        else
        {
            transform.Translate(waveMovement);
        }
        timeAlive += Time.deltaTime;
        if (lowAlpha && GetComponent<TMP_Text>().alpha > 0.3f)
        {
            GetComponent<TMP_Text>().alpha = Mathf.Lerp(GetComponent<TMP_Text>().alpha,0.3f,Time.deltaTime*2);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("WordLimit") && !isResolved)
        {
            if (!isRemoved)
            {
                gameManager.Remove0();
            }
            gameManager.Hitted();
            splashDrop.GetComponent<ParticleSystem>().Play();
            Instantiate(splashDrop, transform.position, Quaternion.identity, null);
            Destroy(gameObject);
        }
        if (collision.tag.Equals("PointsBox") && isResolved)
        {
            Destroy(gameObject);
            //Efecte punts
            gameManager.AddPoints(points);
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
