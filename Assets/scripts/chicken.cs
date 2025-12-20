using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class chicken : MonoBehaviour
{
    public Sprite idle;
    public Text score;
    public Sprite jump;
    private void Awake()
    {
        
        PlayerPrefs.SetInt("score",0);

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "egg") {
            PlayerPrefs.SetInt("score",PlayerPrefs.GetInt("score") + 1);
            score.text = PlayerPrefs.GetInt("score").ToString();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "stone"){
            if (!PlayerPrefs.HasKey("bestscore"))
            {
                PlayerPrefs.SetInt("bestscore", 0);
            }
            if (PlayerPrefs.GetInt("bestscore") < PlayerPrefs.GetInt("score"))
                {
                    PlayerPrefs.SetInt("bestscore", PlayerPrefs.GetInt("score"));
                }
                
            
            PlayerPrefs.SetInt("lastscore", PlayerPrefs.GetInt("score"));
           
            Destroy(collision.gameObject);
            GameObject.Find("Gameplay").GetComponent<gameplay>().isalive = false;
            StartCoroutine(endgame());
        }
    }
    
    IEnumerator endgame() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = jump;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = idle;
    }
 
}
