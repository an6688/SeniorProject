using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform platformTransform;

    [SerializeField] private Transform transformB;

    private Vector3 posA;

    private Vector3 posB;

    private Vector3 nextPos;

    [SerializeField] private float movementSpeed;


    // Use this for initialization
    void Start()
    {
        //Sets pos a equal to the platform's startpostion
        posA = platformTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the platform
        platformTransform.localPosition = Vector3.MoveTowards(platformTransform.localPosition, nextPos, movementSpeed * Time.deltaTime);

        //Checks if we need to change destitaion from a to b or b to a
        if (Vector3.Distance(platformTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != posA ? posA : posB;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.layer = 10; //Change the player's layer to platform layer
            other.transform.SetParent(platformTransform); //Change the players parent to the platform
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //remove the players as a child
            other.transform.SetParent(null);
        }
    }
}
