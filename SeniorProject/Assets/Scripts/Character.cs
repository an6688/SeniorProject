using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    // Character is a super class that works for both the player and enemy

    public Animator MyAnimator { get; private set; }

    protected bool facingRight;
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }

    public abstract bool isDead { get; }

    public abstract void Death();

    [SerializeField] private BoxCollider2D broomCollider; 

    [SerializeField] protected float movementSpeed;

    [SerializeField] protected int health;

    /// A list of damage sources (tags that can damage the character)
    [SerializeField] private List<string> damageSources;

    public BoxCollider2D BroomCollider => broomCollider;

    public abstract IEnumerator TakeDamage();

    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("CharStart: ");
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        // transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;

        transform.localScale = playerScale;
    }

    public void MelleeAttack()
    {
        BroomCollider.enabled = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //If the object we hit is a damage source
        if (damageSources.Contains(other.tag))
        {
            //Run the take damage co routine
            StartCoroutine(TakeDamage());
        }
    }

}
