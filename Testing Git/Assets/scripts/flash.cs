using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class flash : MonoBehaviour
{
    public Light lightSource;
    public float fadeDuration;
    public float maxIntensity;
    public bool LightFlashing;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!LightFlashing)
            {
                StartCoroutine(FadeOutLight());
                LightFlashing = true;
            }
            
        }
    }

    IEnumerator FadeOutLight()
    {
        lightSource.intensity = maxIntensity;
        float elapsedTime = 0f;
        if (Random.Range(0, 10) == 0)
        {
            lightSource.color = Color.red;
        }
        
        while (elapsedTime < fadeDuration)
        {
            lightSource.intensity = Mathf.Lerp(maxIntensity, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        lightSource.intensity = 0;
        lightSource.color = Color.white;
        yield return new WaitForSeconds(1);
        LightFlashing = false;
    }
}
