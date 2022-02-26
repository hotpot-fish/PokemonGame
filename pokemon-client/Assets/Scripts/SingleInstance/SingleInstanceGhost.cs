using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleInstanceGhost : MonoBehaviour
{
    private static SingleInstanceGhost instance = null;
    public string path;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(this!=instance)
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
