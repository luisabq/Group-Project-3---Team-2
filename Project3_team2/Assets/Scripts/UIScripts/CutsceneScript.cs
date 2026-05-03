using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class SlideshowController : MonoBehaviour
{
    [Header("UI")]
    public Image artImage;
    public GameObject text1;
    public GameObject text2;
    public GameObject nextButton;

    [Header("Slides")]
    public Sprite[] images;

    [TextArea(2, 4)]
    public string[] firstTexts;

    [TextArea(2, 4)]
    public string[] secondTexts;

    [Header("Settings")]
    public float delayBetweenTexts = 2f;
    public float fadeDuration = 0.5f;

    [Header("Scene")]
    public string nextSceneName = "Hub Scene";

    private int currentSlide = 0;

    private TMP_Text text1Comp;
    private TMP_Text text2Comp;

    private CanvasGroup text1Group;
    private CanvasGroup text2Group;

    void Start()
    {
        text1Comp = text1.GetComponent<TMP_Text>();
        text2Comp = text2.GetComponent<TMP_Text>();

        text1Group = text1.GetComponent<CanvasGroup>();
        text2Group = text2.GetComponent<CanvasGroup>();

        StartSlide();
    }

    void StartSlide()
    {

        text1Group.alpha = 1f;
        text2Group.alpha = 0f;

        text1.SetActive(true);
        text2.SetActive(false);
        nextButton.SetActive(false);

        artImage.sprite = images[currentSlide];
        text1Comp.text = firstTexts[currentSlide];

        text2Comp.text = "";

        StartCoroutine(ShowSecondText());
    }

    IEnumerator ShowSecondText()
    {
        yield return new WaitForSeconds(delayBetweenTexts);

        yield return StartCoroutine(FadeCanvasGroup(text1Group, 1f, 0f));

        text1.SetActive(false);

        text2Comp.text = secondTexts[currentSlide];
        text2.SetActive(true);

        yield return StartCoroutine(FadeCanvasGroup(text2Group, 0f, 1f));

        nextButton.SetActive(true);
    }

    public void NextSlide()
    {
        currentSlide++;

        if (currentSlide >= images.Length)
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        StartSlide();
    }

    IEnumerator FadeCanvasGroup(CanvasGroup group, float start, float end)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            group.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            yield return null;
        }

        group.alpha = end;
    }
}