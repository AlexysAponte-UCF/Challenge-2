using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public GameObject player;
    public Text score;
    public Text winText;
    public Text subText;
    public AudioSource WNSource;
    public AudioSource BGSource;
    private Vector2 originalPos = new Vector2 (-1,-2);
    private int scoreValue = 0;
    private int level;
    private GameObject[] coin;
    private GameObject[] enmy;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource BGSource = GetComponent<AudioSource>();
        AudioSource WNSource = GetComponent<AudioSource>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        subText.text = "";
        coin = GameObject.FindGameObjectsWithTag("Coin");
        enmy = GameObject.FindGameObjectsWithTag("Enemy");
        level = SceneManager.GetActiveScene().buildIndex + 1;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            other.gameObject.SetActive(false);
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            player.gameObject.SetActive(false);
            Invoke("Reset", 1f);
        }
    }
    private void Reset()
    {
        BGSource.Stop();
        scoreValue = 0;
        SetCountText();
        player.gameObject.SetActive(true);
        transform.position = originalPos;
        foreach (GameObject go in coin)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in enmy)
        {
            go.SetActive(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.velocity = new Vector2(0.0f, 7.0f);
            }
        }
        if (collision.collider.tag == "platform")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetCountText()
    {
        score.text = "Score = " + scoreValue.ToString();
        if (scoreValue >= 4)
        {
            BGSource.Stop();
            WNSource.Play();
            SceneManager.LoadScene(level);
            winText.text = "You Win! Game created by Alexys Aponte";
            subText.text = "Press Any Key To Replay";
            Invoke("Retry", 5f);
        }
    }
    void Retry()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}