using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System;

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


    public TMP_Text provaParaula;

    public TMP_Text text;

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


    [Header("Audio")]
    [SerializeField] private string levelSongName = "LevelSong";

    public GoosScript goosScript;
    // Start is called before the first frame update
    void Start()
    {
       // AudioManager.Instance.Play(levelSongName);  
        actualWords = new List<WordsClass>();
        for (int i = 0; i < 15; i++)
        {
            isWordTaken = true;
            while (isWordTaken)
            {
                word = UnityEngine.Random.Range(0, 117);
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
                word = UnityEngine.Random.Range(0, 58);
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

        if (wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().alpha = Mathf.Lerp(wordsInOrder[0].GetComponent<TMP_Text>().alpha,1f,Time.deltaTime*4);
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
        }
        else { time -= Time.deltaTime; }

        if (actualWords.Count == 0 && missedWords.Count > 0)
        {
            actualWords = missedWords;
            missedWords = new List<WordsClass>();
        }
        if (wordsInOrder.Count < 1 && missedWords.Count < 1 && actualWords.Count < 1)
        {
            Debug.Log("END");
        }
    }

    public void OptionPress(int option)
    {
        goosScript.Attack();
        if (option == 1 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option1;
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(1))
            {
                actualPoints += (int)((correctWordPoints - Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)) * (speed / 0.8)) + correctWordPoints;
                wordsInOrder[0].GetComponent<SingleWordScript>().CorrectAnswer(true);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                NextWord();
            }
            else
            {
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder[0].GetComponent<SingleWordScript>().CorrectAnswer(false);
                missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass);
                NextWord();
            }
        }

        if (option == 2 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option2;
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(2))
            {
                actualPoints += (int)((correctWordPoints - Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)) * (speed / 0.8)) + correctWordPoints;
                wordsInOrder[0].GetComponent<SingleWordScript>().CorrectAnswer(true);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                NextWord();
            }
            else
            {
                wordsInOrder[0].GetComponent<SingleWordScript>().CorrectAnswer(false);
                missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                NextWord();
            }
        }

        if (option == 3 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option3;
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(3))
            {
                actualPoints += (int)((correctWordPoints - Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)) * (speed / 0.8)) + correctWordPoints;
                wordsInOrder[0].GetComponent<SingleWordScript>().CorrectAnswer(true);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                NextWord();
            }
            else
            {
                wordsInOrder[0].GetComponent<SingleWordScript>().CorrectAnswer(false);
                missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
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
        timer = Math.Abs(wordLimit.position.x - spawner1.position.x) / speed / maxWordsOnScreen;
    }

    private void NextWord()
    {
        wordsInOrder.RemoveAt(0);
        if (wordsInOrder.Count > 0)
        {
            goosScript.Turn(wordsInOrder[0].GetComponent<SingleWordScript>().isGoingLeftWord);
        }
    }
}
