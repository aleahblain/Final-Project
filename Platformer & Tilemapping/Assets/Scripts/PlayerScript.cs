using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    private int livesValue = 3;
    public Text lives;
    Animator anim;
    public Text winText;
    public Text snake;
    private bool facingRight = true;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioClip musicClipFour;
    public AudioClip musicClipFive;
    public AudioSource musicSource;
    public AudioSource musicSource2;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    private int permLivesLost = 0;
    public Text TimerText;
    public bool playing;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {

        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Arrows: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        anim = GetComponent<Animator>();
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;

    }

    void Update()
    {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (playing == true)
        {

            Timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(Timer / 60F);
            int seconds = Mathf.FloorToInt(Timer % 60F);
            int milliseconds = Mathf.FloorToInt((Timer * 100F) % 100F);
            TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }

        if (Timer > 70.0f)
        {
            winText.text = "You lose! Game created by Aleah Blain";
            musicSource.Stop();
            musicSource.clip = musicClipThree;
            musicSource.loop = false;
            musicSource.Play();
            Destroy(this.gameObject);
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");


        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        anim.SetInteger("State", 0);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            anim.SetInteger("State", 2);

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            musicSource2.clip = musicClipFour;
            musicSource2.Play();
            musicSource2.loop = false;
        }


        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Arrow")
        {
            scoreValue += 1;
            score.text = "Arrows: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "Enemy")
        {
            musicSource2.clip = musicClipFive;
            musicSource2.Play();
            musicSource2.loop = false;
            livesValue--;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (heart1.active == false)
            {
                if (heart2.active == false)
                {
                    heart3.SetActive(false);
                }
                else
                {
                    heart2.SetActive(false);
                }
            }
            else
            {
                heart1.SetActive(false);
            }


        }

        if (collision.collider.tag == "Enemy2")
        {
            musicSource2.clip = musicClipFive;
            musicSource2.Play();
            musicSource2.loop = false;
            livesValue--;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (heart1.active == false)
            {
                if (heart2.active == false)
                {
                    heart3.SetActive(false);
                }
                else
                {
                    heart2.SetActive(false);
                }
            }
            else
            {
                heart1.SetActive(false);
            }

            speed -= 2;
            permLivesLost+= 1;
            snake.text = "You've permanently lost a life!";
        }


        if (scoreValue == 4)
        {
            livesValue = 3 - permLivesLost;
            lives.text = "Lives: " + livesValue.ToString();
            transform.position = new Vector2(100.0f, 14.26f);
            scoreValue++;
            if(permLivesLost == 1)
            {
                heart2.SetActive(true);
            } else
            {
                heart1.SetActive(true);
                heart2.SetActive(true);
            }
        } else if (scoreValue > 4) {

           score.text = "Arrows: " + (scoreValue - 1).ToString();
        }

            if (scoreValue >= 9)
            {
                winText.text = "You win! Game created by Aleah Blain.";
                playing = false;
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.loop = false;
                musicSource.Play();
                Destroy(this.gameObject);
            }

            if (livesValue <= 0)
            {

                winText.text = "You lose! Game created by Aleah Blain";
                playing = false;
                musicSource.Stop();
                musicSource.clip = musicClipThree;
                musicSource.loop = false;
                musicSource.Play();
                Destroy(this.gameObject);
            }


        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ground")
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                }
            }

        }

        void Flip()
        {
            facingRight = !facingRight;
            Vector2 Scaler = transform.localScale;
            Scaler.x = Scaler.x * -1;
            transform.localScale = Scaler;
        }
    
}
