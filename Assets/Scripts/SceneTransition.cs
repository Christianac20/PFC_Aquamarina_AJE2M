using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    #region VARIABLES
    Animator animator;
    [SerializeField] string scene;
    [SerializeField] AnimationClip animacionFinal;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            StartCoroutine(ChangeScene());
        }
    }

    public void SceneChange()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        animator.SetTrigger("Iniciar");

        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene(scene);
    }
    #endregion
}
