using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInstanceFm : MonoBehaviour
{
    private static SingleInstanceFm instance = null;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
