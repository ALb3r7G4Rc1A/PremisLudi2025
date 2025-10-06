using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

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


    public TMP_Text provaParaula;

    public TMP_Text text;

    public GameObject textPrefab;

    public int speed;

    private int randomSpawner;
    private int randomWord;

    private int word;
    public int[] eazyWordsTaken = new int[15];
    private int[] mediumWordsTaken = new int[10];
    private int[] hardWordsTaken = new int[5];
    private bool isWordTaken;
    // Start is called before the first frame update
    void Start()
    {
        actualWords = new List<WordsClass>();
        for (int i = 0; i < 15; i++)
        {
            isWordTaken = true;
            while (isWordTaken)
            {
                word = Random.Range(0, 117);
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
                word = Random.Range(0, 58);
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
                word = Random.Range(0, 30);
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
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && actualWords.Count > 0)
        {
            randomSpawner = Random.Range(0, 3);
            randomWord = Random.Range(0, actualWords.Count);
            if (randomSpawner == 0)
            {
                GameObject a = Instantiate(textPrefab, spawner1);
                a.GetComponent<TMP_Text>().text = actualWords[randomWord].basic;
                a.GetComponent<SingleWordScript>().wordsClass = actualWords[randomWord];
                wordsInOrder.Add(a);
            }
            else if (randomSpawner == 1)
            {
                GameObject a = Instantiate(textPrefab, spawner2);
                a.GetComponent<TMP_Text>().text = actualWords[randomWord].basic;
                a.GetComponent<SingleWordScript>().wordsClass = actualWords[randomWord];
                wordsInOrder.Add(a);
            }
            else
            {
                GameObject a = Instantiate(textPrefab, spawner3);
                a.GetComponent<TMP_Text>().text = actualWords[randomWord].basic;
                a.GetComponent<SingleWordScript>().wordsClass = actualWords[randomWord];
                wordsInOrder.Add(a);
            }
            actualWords.RemoveAt(randomWord);
        }

        if (Input.GetKeyDown(KeyCode.A) && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option1;
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(1))
            {
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.green;
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
            else
            {
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.red;
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                //actualWords.Add(actualWords[0]);
                wordsInOrder.RemoveAt(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option2;
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(2))
            {
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.green;
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
            else
            {
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.red;
                //actualWords.Add(actualWords[0]);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.D) && wordsInOrder.Count > 0) 
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.option3;
            if (wordsInOrder[0].GetComponent<SingleWordScript>().wordsClass.isCorrectAccent(3))
            {
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.green;
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
            else
            {
                wordsInOrder[0].GetComponent<TMP_Text>().color = Color.red;
                //wordsInOrder.Add(actualWords[0]);
                wordsInOrder[0].GetComponent<SingleWordScript>().isRemoved = true;
                wordsInOrder.RemoveAt(0);
            }
        }
    }

    public void Remove0()
    {
        wordsInOrder.RemoveAt(0);
    }
}
