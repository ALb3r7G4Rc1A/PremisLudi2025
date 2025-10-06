using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWordScript : MonoBehaviour
{
    private GameManager gameManager;
    public WordsClass wordsClass;
    public bool isRemoved;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        isRemoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * gameManager.speed * -Vector2.right);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("WordLimit"))
        {
            if (!isRemoved)
            {
                gameManager.Remove0();
            }
            Destroy(gameObject);
        }
    }
}
