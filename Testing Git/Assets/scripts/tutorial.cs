using System.Collections;
using TMPro;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public TextMeshProUGUI textElement; // Reference to the UI Text
    public float fadeDuration = 1f;
    public bool done;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!done)
        {
            StartCoroutine(FadeText());
            done = true;
        }
        
    }

    IEnumerator FadeText()
    {
        float elapsedTime = 0f;
        Color originalColor = textElement.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }
}
