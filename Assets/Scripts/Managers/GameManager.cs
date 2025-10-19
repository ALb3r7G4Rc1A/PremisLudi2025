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
    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log(wordsList.eazy[word].ToString());
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
            Debug.Log(wordsList.medium[word].ToString());
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
            Debug.Log(wordsList.hard[word].ToString());
        }
        //només es per comprovar q es vegin les paraules bé
        //provaParaula.text = actualWords[0].basic; // Mostra la paraula
        Debug.Log("Primera paraula: " + actualWords[0].basic);
        SetSpawnTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0 && actualWords.Count > 0)
        {
            SetSpawnTimer();
            time = timer;
            randomWord = UnityEngine.Random.Range(0, actualWords.Count);

            randomSpawner = UnityEngine.Random.Range(0, 3);
            selectedSpawner = randomSpawner switch { 0 => spawner1, 1 => spawner2, _ => spawner3 };

            selectedWord = Instantiate(textPrefab, selectedSpawner);
            selectedWord.GetComponent<TMP_Text>().text = actualWords[randomWord].basic;
            selectedWord.GetComponent<SingleWordScript>().wordsClass = actualWords[randomWord];

            wordsInOrder.Add(selectedWord);
            actualWords.RemoveAt(randomWord);
        }
        else { time -= Time.deltaTime; }

        if (actualWords.Count == 0 && missedWords.Count > 0)
        {
            actualWords = missedWords;
            missedWords = new List<WordsClass>();
        }
    }

    public void OptionPress(int option)
    {
        if (option == 1 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option1;
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(1))
            {
                Debug.Log("Time Alive"+Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive));
                Debug.Log("Resta" + (correctWordPoints - Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)));
                Debug.Log("Multiplicador: " + (speed / 200f));
                actualPoints += (int)((correctWordPoints - Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)) * (speed / 200f)) + correctWordPoints;
                Debug.Log("Punts actuals: " + actualPoints);
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.green;
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
            else
            {
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.red;
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass);
                wordsInOrder.RemoveAt(0);
            }
        }

        if (option == 2 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option2;
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(2))
            {
                Debug.Log("Time Alive"+Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive));
                Debug.Log("Resta" + (correctWordPoints - Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)));
                Debug.Log("Multiplicador: " + (speed / 200f));
                actualPoints += (int)((correctWordPoints - Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)) * (speed / 200f)) + correctWordPoints;
                Debug.Log("Punts actuals: " + actualPoints);
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.green;
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
            else
            {
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.red;
                missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
        }

        if (option == 3 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option3;
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(3))
            {
                Debug.Log("Time Alive"+Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive));
                Debug.Log("Resta" + (correctWordPoints - Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)));
                Debug.Log("Multiplicador: " + (speed / 200f));
                actualPoints += (int)((correctWordPoints - Math.Ceiling(wordsInOrder[0].GetComponent<SingleWordScript>().timeAlive)) * (speed / 200f)) + correctWordPoints;
                Debug.Log("Punts actuals: " + actualPoints);
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.green;
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
            else
            {
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.red;
                missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
        }
    }

    public void Remove0()
    {
        missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass);
        wordsInOrder.RemoveAt(0);
    }

    private void SetSpawnTimer()
    {
        timer = Math.Abs(wordLimit.position.x - spawner1.position.x) / speed / maxWordsOnScreen;
    }
}
