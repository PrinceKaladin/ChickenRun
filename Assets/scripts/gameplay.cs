using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameplay : MonoBehaviour
{
    [Header("Настройки курицы")]
    public GameObject chicken;
    public float x;
    public bool can = true;
    public bool isalive = true;
    [Header("Объекты для спавна")]
    public GameObject objectA;
    public GameObject objectB;

    [Header("Настройки движения спавн-объектов")]
    public float speed = 2f;
    [Header("Точка спавна")]
    public Vector2 spawnPosition = new Vector2(4f, -1.5f);

    [Header("Время между спавнами")]
    public float spawnDelay = 3f;
    [Header("ScoresMenu")]
    public bool isGameover;
    public Text ls;
    public Text bs;
    public Vector2 targetposition;

    private void Start()

    {
        
        if (isGameover) {
            ls.text = PlayerPrefs.GetInt("lastscore").ToString();
            bs.text = PlayerPrefs.GetInt("bestscore").ToString();
        }
        InvokeRepeating(nameof(SpawnRandomObject), 0f, spawnDelay);
    }
    void SpawnRandomObject()
    {
        if (isalive)
        {
            GameObject randomObject =
                Random.value > 0.5f ? objectA : objectB;

            GameObject spawned = Instantiate(
                randomObject,
                spawnPosition,
                Quaternion.identity
            );

            MoveLeft move = spawned.AddComponent<MoveLeft>();
            move.speed = speed;
        }
    }
    public void loadscene(int scene)
    {

        SceneManager.LoadScene(scene);

    }
    public void Jump() {
        if (can)
        {
            Rigidbody2D rb = chicken.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, x));
            can = false;
            StartCoroutine(ResetJump());
        }
    }
    IEnumerator ResetJump() { 
    yield return new WaitForSeconds(1);
    can = true;

    }
}
public class MoveLeft : MonoBehaviour
{
    public float speed = 2f;
    public float lifeTime = 15f;

    void Start()
    {
        // Уничтожить объект через 15 секунд, если он ещё существует
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-3.75f, -2.15f), speed * Time.deltaTime);


    }
}