using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    [SerializeField] private Image image;
    [SerializeField] private float duration;
    [SerializeField] private AudioClip music;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    public void FadeAndLoad(int sceneIndex)
    {
        StartCoroutine(Fader(sceneIndex));
    }

    private IEnumerator Fader(int sceneIndex)
    {
        float timer = 0.0f;
        Color color = image.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            color.a = timer / duration;
            image.color = color;

            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    public IEnumerator FadeOut()
    {
        float timer = 0.0f;
        Color color = image.color;

        while (timer < 1)
        {
            timer += Time.deltaTime;

            color.a = 1.0f - (timer / 1.0f);
            image.color = color;

            yield return null;
        }
    }
}
