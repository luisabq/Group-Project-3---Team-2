using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class EndingCutsceneController : MonoBehaviour
{
    public Image artImage;
    public GameObject text1;
    public GameObject text2;
    public GameObject nextButton;

    public Sprite endingImage;

    [TextArea(2, 4)]
    public string firstText;

    [TextArea(2, 4)]
    public string secondText;

    public float delayBetweenTexts = 2f;
    public float fadeDuration = 0.5f;

    public string nextScene = "WinScreen";

    private TMP_Text text1Comp;
    private TMP_Text text2Comp;

    private CanvasGroup text1Group;
    private CanvasGroup text2Group;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        text1Comp = text1.GetComponent<TMP_Text>();
        text2Comp = text2.GetComponent<TMP_Text>();

        text1Group = text1.GetComponent<CanvasGroup>();
        text2Group = text2.GetComponent<CanvasGroup>();

        StartCutscene();
    }

    void StartCutscene()
    {
        text1Group.alpha = 1f;
        text2Group.alpha = 0f;

        text1.SetActive(true);
        text2.SetActive(false);
        nextButton.SetActive(false);

        artImage.sprite = endingImage;
        text1Comp.text = firstText;

        StartCoroutine(ShowSecondText());
    }

    IEnumerator ShowSecondText()
    {
        yield return new WaitForSeconds(delayBetweenTexts);

        yield return StartCoroutine(FadeCanvasGroup(text1Group, 1f, 0f));
        text1.SetActive(false);

        text2Comp.text = secondText;
        text2.SetActive(true);

        yield return StartCoroutine(FadeCanvasGroup(text2Group, 0f, 1f));

        nextButton.SetActive(true);
    }

    public void Next()
    {
        SceneManager.LoadScene(nextScene);
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
