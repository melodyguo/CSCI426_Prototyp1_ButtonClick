using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloatingText : MonoBehaviour
{
    [SerializeField] float destroyTime = 3f;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -0.5f);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        offset += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        transform.localPosition += offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
