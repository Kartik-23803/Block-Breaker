using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float rightLaunch = 2f;
    [SerializeField] float upLaunch = 7f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //state
    bool lockBall = true;
    Vector2 paddleToBallVector;
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(lockBall == true)
        {
            LockBallToPaddle();
            LaunchOnMosueClick();
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnMosueClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(rightLaunch, upLaunch);
            lockBall = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0.25f, randomFactor), Random.Range(-randomFactor, -0.25f));
        if(lockBall == false)
        {
            AudioClip clip = ballSounds[Random.Range(0,ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}