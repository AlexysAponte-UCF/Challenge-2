using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public GameObject player;
    public Text loseText;

    private void Start()
    {
        loseText.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if (health <= 0)
            {
                player.gameObject.SetActive(false);
                loseText.text = "You Lose!" +
                    " Press any button to reset";
                if (Input.anyKey)
                {
                    SceneManager.LoadScene("SampleScene");
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
        }
    }
}

