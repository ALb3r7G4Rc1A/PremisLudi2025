using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Spawners")]
    public Transform spawner1;
    public Transform spawner2;
    public Transform spawner3;
    public Transform spawner4;
    public WordsList wordsList;

    [Header("ActualWords")]
    public List<WordsClass> actualWords;
    public List<GameObject> wordsInOrder;
    public List<WordsClass> missedWords;

    public GameObject textPrefab;
    private GameObject selectedWord;
    public float speed;

    private int randomSpawner;
    private int randomWord;
    private Transform selectedSpawner;

    private int word;
    public int[] eazyWordsTaken = new int[15];
    private int[] mediumWordsTaken = new int[10];
    private int[] hardWordsTaken = new int[5];
    private bool isWordTaken;

    private float time;
    private float timer;
    public float maxWordsOnScreen;

    public Transform wordLimit;


    [Header("Progress")]
    public int correctWordPoints;
    public int actualPoints;
    private float multiplier;


    [Header("Audio")]
    [SerializeField] private string levelSongName = "LevelSong";

    public GoosScript goosScript;

    [Header("PointsBox")]
    public ParticleSystem popUpBoxGoodPoints;
    public ParticleSystem popUpBoxBadPoints;
    public TMP_Text popUpTextGoodPoints;
    public TMP_Text popUpTextBadPoints;
    public ParticleSystem popUpBoxMultiplier;
    public TMP_Text popUpTextMultiplier;
    public Image pointsBox;
    public TMP_Text pointsText;
    public GameObject endPanel;
    public TMP_Text endPointsText;
    [Header("Strek")]
    private int streak;
    private int badStreak;
    public GestureTest gestureTest;
    public GameObject bafarada;
    public TMP_Text textBafarda;
    private float bafaradaTimer;
    private bool isReapeat;
    private bool isReapeatDoubleCheck;
    private bool isLastWord;

    [Header("Fade")]
    public GameObject fadeRainbow;
    public GameObject fadeDrop;
    // Start is called before the first frame update
    void Start()
    {
        isLastWord = false;
        endPanel.SetActive(false);
        fadeDrop.SetActive(false);
        fadeRainbow.SetActive(false);
        isReapeatDoubleCheck = true;
        isReapeat = false;
        multiplier = 3;
        streak = 0;
        badStreak = 0;
        AudioManager.Instance.Play(levelSongName);  
        actualWords = new List<WordsClass>();
        for (int i = 0; i < 15; i++)
        {
            isWordTaken = true;
            while (isWordTaken)
            {
                word = UnityEngine.Random.Range(0, 118);
                isWordTaken = false;
                for (int x = 0; x < i; x++)
                {
                    if (word == eazyWordsTaken[x]) isWordTaken = true;
                }
            }
            eazyWordsTaken[i] = word;
            actualWords.Add(wordsList.eazy[word]);
        }

        for (int i = 0; i < 10; i++)
        {
            isWordTaken = true;
            while (isWordTaken)
            {
                word = UnityEngine.Random.Range(0, 57);
                isWordTaken = false;
                for (int x = 0; x < i; x++)
                {
                    if (word == mediumWordsTaken[x]) isWordTaken = true;
                }
            }
            mediumWordsTaken[i] = word;
            actualWords.Add(wordsList.medium[word]);
        }

        for (int i = 0; i < 5; i++)
        {
            isWordTaken = true;
            while (isWordTaken)
            {
                word = UnityEngine.Random.Range(0, 30);
                isWordTaken = false;
                for (int x = 0; x < i; x++)
                {
                    if (word == hardWordsTaken[x]) isWordTaken = true;
                }
            }
            hardWordsTaken[i] = word;
            actualWords.Add(wordsList.hard[word]);
        }
        SetSpawnTimer();
    }

    // Update is called once per frame
    void Update()
    {     
        if (bafaradaTimer < 0)
        {
            bafarada.SetActive(false);
        }
        else
        {
            bafaradaTimer -= Time.deltaTime;
        }        pointsText.text = actualPoints + "";
        if (wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().alpha = Mathf.Lerp(wordsInOrder[0].GetComponent<TMP_Text>().alpha, 1f, Time.deltaTime * 4);
        }
        if (time < 0 && actualWords.Count > 0)
        {
            SetSpawnTimer();
            time = timer;
            randomWord = UnityEngine.Random.Range(0, actualWords.Count);

            randomSpawner = UnityEngine.Random.Range(0, 4);
            selectedSpawner = randomSpawner switch { 0 => spawner1, 1 => spawner2, 2 => spawner3, _ => spawner4 };
            selectedWord = Instantiate(textPrefab, selectedSpawner);
            selectedWord.GetComponent<TMP_Text>().text = actualWords[randomWord].basic; 
            selectedWord.GetComponent<SingleWordScript>().wordsClass = actualWords[randomWord];
            if (randomSpawner == 0 || randomSpawner == 1)
            {
                selectedWord.GetComponent<SingleWordScript>().GoingLeftWord(true);
            }
            else
            {
                selectedWord.GetComponent<SingleWordScript>().GoingLeftWord(false);
            }
            wordsInOrder.Add(selectedWord);
            actualWords.RemoveAt(randomWord);

            if (wordsInOrder.Count == 1)
            {
                goosScript.Turn(wordsInOrder[0].GetComponent<SingleWordScript>().isGoingLeftWord);
            }
            if (isReapeat && isReapeatDoubleCheck)
            {
                bafarada.SetActive(true);
                textBafarda.text = "Em sonen les paraules";
                bafaradaTimer = 1;
                AudioManager.Instance.Play("Woof");
                isReapeatDoubleCheck = false;
            }
        }
        else { time -= Time.deltaTime; }

        if (actualWords.Count == 0 && missedWords.Count > 0)
        {
            actualWords = missedWords;
            missedWords = new List<WordsClass>();
            isReapeat = true;
        }
    }
    
    public void OptionPress(int option)
    {
        AudioManager.Instance.Play("Stroke");  
        if (option == 1 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option1;
        }
        else if (option == 2 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option2;
        }
        else if (option == 3 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option3;
        }

        if (wordsInOrder.Count > 0)
        {
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(option))
            {
                fadeDrop.SetActive(false);
                streak++;
                badStreak = 0;
                if (streak == 5)
                {
                    fadeRainbow.SetActive(true);
                    AudioManager.Instance.Play("MagicWand");
                    goosScript.Shine();
                    gestureTest.streak = true;
                    bafarada.SetActive(true);
                    int x = UnityEngine.Random.Range(0,4);
                    if (x == 0)
                    {
                        textBafarda.text = "Estic Imparable";
                    }
                    else if (x == 1)
                    {
                        textBafarda.text = "En Ratxa";
                    }
                    else
                    {
                        textBafarda.text = "Soc Súper";
                    }
                    bafaradaTimer = 1;
                    AudioManager.Instance.Play("Woof");
                }
                else
                {
                    goosScript.Attack(true);
                }
                wordsInOrder[0].GetComponent<SingleWordScript>().points = (int)((correctWordPoints - Mathf.Ceil(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)) * (speed / 0.8)) + correctWordPoints;
                wordsInOrder[0].GetComponent<SingleWordScript>().CorrectAnswer(true);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder[0].GetComponent<SingleWordScript>().isResolved = true;
                if (wordsInOrder.Count == 1 && missedWords.Count < 1 && actualWords.Count < 1)
                {
                    isLastWord = true;
                }
                NextWord();
            }
            else
            {
                goosScript.Attack(false);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder[0].GetComponent<SingleWordScript>().CorrectAnswer(false);
                missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass);
                NextWord();
            }
        }
    }

    public void Remove0()
    {
        missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass);
        NextWord();
    }

    private void SetSpawnTimer()
    {
        timer = Mathf.Abs(wordLimit.position.x - spawner1.position.x) / speed / maxWordsOnScreen;
    }

    private void NextWord()
    {
        wordsInOrder.RemoveAt(0);
        if (wordsInOrder.Count > 0)
        {
            goosScript.Turn(wordsInOrder[0].GetComponent<SingleWordScript>().isGoingLeftWord);
        }
    }
    public void Hitted()
    {
        streak = 0;
        badStreak++;
        fadeRainbow.SetActive(false);
        gestureTest.streak = false;
        if (badStreak >= 3)
        {
            fadeDrop.SetActive(true);
            FindAnyObjectByType<AudioManager>().mixer.SetFloat("MusicPitch", 0.9f);
        }
        else
        {
            FindAnyObjectByType<AudioManager>().mixer.SetFloat("MusicPitch", 1f);
        }        
        goosScript.Hitted(badStreak);
        AudioManager.Instance.Play("inkSplash");
        if (actualPoints > correctWordPoints)
        {
            popUpTextBadPoints.text = "-" + correctWordPoints;
            popUpBoxBadPoints.Play();
            actualPoints -= correctWordPoints;
            pointsBox.GetComponent<Animator>().SetTrigger("IncorrectEnter");
            pointsText.GetComponent<Animator>().SetTrigger("IncorrectEnter");
        }
    }
    public void AddPoints(int points)
    {
        if (streak >= 5)
        {
            FindAnyObjectByType<AudioManager>().mixer.SetFloat("MusicPitch", 1.1f);
            AudioManager.Instance.Play("MagicPoints");
            popUpTextMultiplier.text = "X3";
            popUpBoxMultiplier.Play();
            points = (int)(points * multiplier);
        }
        else
        {
            FindAnyObjectByType<AudioManager>().mixer.SetFloat("MusicPitch", 1f);
            AudioManager.Instance.Play("Points");
        }
        popUpTextGoodPoints.text = "+" + points;
        popUpBoxGoodPoints.Play();
        actualPoints += points;
        pointsBox.GetComponent<Animator>().SetTrigger("CorrectEnter");
        pointsText.GetComponent<Animator>().SetTrigger("CorrectEnter");
        if (isLastWord)
        {
            Debug.Log("END");
            endPointsText.text = "" + actualPoints;
            endPanel.SetActive(true);
            goosScript.gameObject.transform.position = new Vector3(2000,0,0);
        }
    }
    public void BackToMenu()
    {
        AudioManager.Instance.Stop(levelSongName);
        SceneManager.LoadScene(0);
    }
}
