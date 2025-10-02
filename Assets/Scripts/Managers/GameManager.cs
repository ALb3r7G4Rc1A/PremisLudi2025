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
    private int[] eazyWordsTaken;
    private int[] mediumWordsTaken;
    private int[] hardWordsTaken;
    private bool isWordTaken;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            isWordTaken = true;
            while (isWordTaken)
            {
                word = Random.Range(0, 120);
                isWordTaken = false;
                for (int x = 0; x < i; x++)
                {
                    if (word == eazyWordsTaken[x]) isWordTaken = true;
                }
            }
            eazyWordsTaken[i] = word;
            actualWords[i] = wordsList.eazy[word];
        }

        for (int i = 15; i < 25; i++)
        {
            isWordTaken = true;
            while (isWordTaken)
            {
                word = Random.Range(0, 60);
                isWordTaken = false;
                for (int x = 15; x < i; x++)
                {
                    if (word == mediumWordsTaken[x]) isWordTaken = true;
                }
            }
            mediumWordsTaken[i] = word;
            actualWords[i] = wordsList.medium[word];
        }

        for (int i = 25; i < 30; i++)
        {
            isWordTaken = true;
            while (isWordTaken)
            {
                word = Random.Range(0, 20);
                isWordTaken = false;
                for (int x = 25; x < i; x++)
                {
                    if (word == hardWordsTaken[x]) isWordTaken = true;
                }
            }
            hardWordsTaken[i] = word;
            actualWords[i] = wordsList.hard[word];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
