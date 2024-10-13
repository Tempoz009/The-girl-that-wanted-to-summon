using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_Ground;
    [SerializeField] private Transform m_GroundCheck;
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    public bool m_HasJumped; 
    public AudioSource landingSound;
    public AudioSource jumpSound;
    public  bool wasGrounded;

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        wasGrounded = Physics2D.OverlapCircle(m_GroundCheck.position, 0.2f, m_Ground);
        if (!m_HasJumped && !wasGrounded)
        {
            m_HasJumped = true;
        }

        if (wasGrounded)
        {
            if (m_HasJumped)
            {
                landingSound.Play();
                OnLandEvent.Invoke();
                m_HasJumped = false; 
            }
        }
    }

    public void Move(float move, bool jump)
    {
        if ((m_HasJumped && m_AirControl) || !m_HasJumped)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }

            if (!m_HasJumped && jump)
            {
                m_HasJumped = true;
                jumpSound.Play();
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
