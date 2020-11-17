using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.Physics2D;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] private Collider2D other;

    // Start is called before the first frame update
    private void Awake()
    {

        // TODO: work on this, from video 16.2, towards the last couple mins 
        /*if (other != null)
        {
            IgnoreCollision(GetComponent<Collider2D>(), other, true);
        }*/
    }
}