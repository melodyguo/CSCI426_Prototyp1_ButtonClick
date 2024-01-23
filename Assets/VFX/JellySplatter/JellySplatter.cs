using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellySplatter : MonoBehaviour
{
    [SerializeField] float destroyTime = 0.3f;
    [SerializeField] Vector3 offset = new Vector3(0, -1.1f, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
