using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManagerTutorial : MonoBehaviour
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

    private float time;
    public float maxWordsOnScreen;
    public int actualWord;

    public Transform wordLimit;

    [Header("Audio")]
    [SerializeField] private string levelSongName = "LevelSong";

    public GoosScriptTutorial goosScript;

    public GameObject bafarada;
    public TMP_Text textBafarada;
    void Start()
    {
        AudioManager.Instance.Play(levelSongName);
        actualWords = new List<WordsClass>();
        actualWords.Add(wordsList.eazy[0]);
        actualWords.Add(wordsList.hard[10]);
        actualWords.Add(wordsList.eazy[1]);
        actualWord = 0;
        time = -1;
    }

    // Update is called once per frame
    void Update()
    {     
        if (wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().alpha = Mathf.Lerp(wordsInOrder[0].GetComponent<TMP_Text>().alpha, 1f, Time.deltaTime * 4);
        }
        if (time < 0 && actualWords.Count > 0)
        {
            time = 1;
            randomSpawner = UnityEngine.Random.Range(0, 4);
            if (actualWord == 0) selectedSpawner = spawner1;
            if (actualWord == 1) selectedSpawner = spawner4;
            if (actualWord == 2) selectedSpawner = spawner2;
            selectedWord = Instantiate(textPrefab, selectedSpawner);
            selectedWord.GetComponent<TMP_Text>().text = actualWords[0].basic;
            selectedWord.GetComponent<SingleWordScriptTutorial>().wordsClass = actualWords[0];
            if (actualWord == 0 || actualWord == 2)
            {
                selectedWord.GetComponent<SingleWordScriptTutorial>().GoingLeftWord(true);
            }
            else
            {
                selectedWord.GetComponent<SingleWordScriptTutorial>().GoingLeftWord(false);
            }
            wordsInOrder.Add(selectedWord);
            actualWords.RemoveAt(randomWord);

            if (wordsInOrder.Count == 1)
            {
                goosScript.Turn(wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().isGoingLeftWord);
            }
        }

        if (actualWords.Count == 0 && missedWords.Count > 0)
        {
            actualWords = missedWords;
            missedWords = new List<WordsClass>();
            actualWord = 0;
        }
        if (wordsInOrder.Count < 1 && missedWords.Count < 1 && actualWords.Count < 1)
        {
            Debug.Log("END");
            
        }
    }
    
    public void OptionPress(int option)
    {
        AudioManager.Instance.Play("Stroke");  
        if (option == 1 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().wordsClass.option1;
        }
        else if (option == 2 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().wordsClass.option2;
        }
        else if (option == 3 && wordsInOrder.Count > 0)
        {
            wordsInOrder[0].GetComponent<TMP_Text>().text = wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().wordsClass.option3;
        }

        if (wordsInOrder.Count > 0)
        {
            if (wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().wordsClass.isCorrectAccent(option))
            {
                actualWord++;
                goosScript.Attack(true);
                wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().CorrectAnswer(true);
                wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().isRemoved = true;
                wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().isResolved = true;
                NextWord();
                time = -1;
                bafarada.SetActive(false);
            }
            else
            {
                goosScript.Attack(false);
            }
        }
    }

    public void Remove0()
    {
        missedWords.Add(wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().wordsClass);
        NextWord();
    }

    private void NextWord()
    {
        wordsInOrder.RemoveAt(0);
        if (wordsInOrder.Count > 0)
        {
            goosScript.Turn(wordsInOrder[0].GetComponent<SingleWordScriptTutorial>().isGoingLeftWord);
        }
    }
    public void Hitted()
    {
        goosScript.Hitted();
    }
    public void wordStop()
    {
        bafarada.SetActive(true);
        AudioManager.Instance.Play("Woof");
        if (actualWord == 0 || actualWord == 1)
        {
            textBafarada.text = "Dibuixa l'accent correcte";
        }
        else if (actualWord == 2)
        {
            textBafarada.text = "Dibuixa un cercle si no té accent";
        }
        /* if (actualWord == 0)
        {
            textBafarada.text = "Dibuixa l'accent obert";
        }
        else if ( actualWord == 1)
        {
            textBafarada.text = "Dibuixa l'accent tancat";
        }
        else if (actualWord == 2)
        {
            textBafarada.text = "Dibuixa un cercle";
        } */
    }
    public void BackToMenu()
    {
        AudioManager.Instance.Stop(levelSongName);
        SceneManager.LoadScene(0);
    }
}
