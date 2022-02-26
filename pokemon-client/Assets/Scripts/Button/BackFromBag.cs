using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackFromBag : MonoBehaviour
{
    public Method method;
    // Start is called before the first frame update
    void Start()
    {
        method = GameObject.Find("Method").GetComponent<Method>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void back()
    {
        method.SetFmAndVm(true);
        method.SetPlayer(true);
        method.InitPosition();
        SceneManager.LoadScene(method.mapPath);
    }
}
