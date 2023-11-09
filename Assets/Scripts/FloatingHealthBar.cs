using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Slider slider;
    public CanvasGroup canvasGroup; // Reference to the CanvasGroup

    private Coroutine fadeOutCoroutine; // Coroutine reference

    private float lastHealth;
    // [SerializeField] private GameObject gameObject;

    // Start with the health bar faded out :
    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
	
	public void SetHealth(int health)
	{
		slider.value = health;

        if (health != lastHealth)
        {
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }

            fadeOutCoroutine = StartCoroutine(FadeOutHealthBar());

            canvasGroup.alpha = 1f;
        }
        lastHealth = health;
	}

    private IEnumerator FadeOutHealthBar()
    {
        yield return new WaitForSeconds(2f); // Adjust the delay as needed

        float fadeDuration = 1.0f; // Duration of the fade-out effect
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = 1 - (timer / fadeDuration); // Calculate the alpha value

            canvasGroup.alpha = alpha; // Apply the alpha value to the CanvasGroup

            yield return null;
        }

        // Deactivate the health bar GameObject when it's fully faded out
        // gameObject.SetActive(false);

    }
}
