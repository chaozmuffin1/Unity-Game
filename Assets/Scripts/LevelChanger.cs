using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public Player playerScript;
    private int levelToLoad;
    public Scene scene;

    void Start(){
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == 2){
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    void Update()
    {
        if (scene.buildIndex == 2){
          if (playerScript.currentHealth <= 0){
            //Debug.Log(playerScript.currentHealth);
            FadeToLevel(3);
            }  
        }
    }
    public void FadeToLevel (int levelIndex){
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete(){
        SceneManager.LoadScene(levelToLoad);
    }
}
