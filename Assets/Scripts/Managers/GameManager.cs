using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Spawners")]
    public Transform spawner1;
    public Transform spawner2;
    public Transform spawner3;
    public WordsList wordsList;

    [Header("ActualWords")]
    public WordsClass[] actualWords;



    private int word;
    public int[] eazyWordsTaken = new int[15];
    private int[] mediumWordsTaken = new int[10];
    private int[] hardWordsTaken = new int[5];
    private bool isWordTaken;
    // Start is called before the first frame update
    void Start()
    {
        actualWords = new WordsClass[30];
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
            actualWords[i] = wordsList.eazy[word];
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
            actualWords[i + 15] = wordsList.medium[word];
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
            actualWords[i + 25] = wordsList.hard[word];
             Debug.Log(wordsList.hard[word].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
