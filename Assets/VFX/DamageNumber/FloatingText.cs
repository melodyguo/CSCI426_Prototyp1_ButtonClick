using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloatingText : MonoBehaviour
{
    [SerializeField] float destroyTime = 3f;
    [SerializeField] Vector3 offset = new Vector3(2, 0, 0);
    [SerializeField] Vector3 rotation = new Vector3(0, -90, 0);
    [SerializeField] float randomOffsetMin = -0.5f;
    [SerializeField] float randomOffsetMax = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        offset += new Vector3(0, Random.Range(randomOffsetMin, randomOffsetMax), Random.Range(randomOffsetMin, randomOffsetMax));
        transform.localPosition += offset;
        transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
