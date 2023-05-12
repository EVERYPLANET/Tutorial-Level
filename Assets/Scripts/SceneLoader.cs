using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;
    
    public void LevelFade()
    {
        animator.SetTrigger("FadeOut");
    }

    public void NextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void Death()
    {
        SceneManager.LoadScene("Death");
    }

    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    
}
