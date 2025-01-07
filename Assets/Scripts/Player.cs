using UnityEngine;

public class Player : MonoBehaviour 
{
    [SerializeField] private float upForce = 350f;
    
    // Add a serialized field for the flap sound effect
    [SerializeField] private AudioSource flapSoundEffect;

    private Rigidbody2D playerRb;
    private bool isDead;
    private Animator playerAnimator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            Flap();
        }
    }

    private void Flap()
    {
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(Vector2.up * upForce);
        playerAnimator.SetTrigger("Flap");

        // Play flap sound effect if assigned
        if (flapSoundEffect != null)
        {
            flapSoundEffect.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca contra un objeto sólido (suelo o pipes)
        if (!isDead)
        {
            isDead = true;
            playerAnimator.SetTrigger("Die");
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si toca un trigger (como el Sky)
        if (collision.CompareTag("Sky") && !isDead)
        {
            isDead = true;
            playerAnimator.SetTrigger("Die");
            GameManager.Instance.GameOver();
        }
    }
}