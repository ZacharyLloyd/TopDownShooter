using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Transform tf;
    [Header("Rotation")]
    [SerializeField]
    [Range(0, 360)]
    [Tooltip("Rate at which the object spins")]
    private int rotationSpeed;

    private void Start()
    {
        tf = GetComponent<Transform>();
    }

    private void Update()
    {
        Spin();
    }
    protected virtual void OnPickup(GameObject target) { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Stats>() != null)
        {
           // if (other.gameObject == GameManager.instance.spawnedPlayer)
           // {
                OnPickup(other.gameObject);
            //}
        }
    }

    /// <summary>
    /// Spin the object about the Y axis by rotationSpeed * time since last frame
    /// </summary>
    void Spin()
    {
        tf.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}