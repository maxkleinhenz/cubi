using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public GameObject CountdownUi;
    public Text CountdownText;
    public int Timer = 3;
    
    public AudioClip CountdownBeep;
    public float CountdownPitch = .5f;
    public float StartPitch = 2.0f;

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
	    StartCoroutine(CountDownCoroutine());	
	}

    IEnumerator CountDownCoroutine()
    {
        _audioSource.pitch = CountdownPitch;

        for (var i = Timer; i > 0; i--)
        {
            CountdownText.text = Mathf.CeilToInt(i).ToString();
            CountdownText.GetComponent<Animator>().SetTrigger("Animate");
            _audioSource.Play();

            yield return new WaitForSeconds(1);
        }

        _audioSource.pitch = StartPitch;
        _audioSource.Play();

        GameManager.Instance.SetRunning();

        yield return new WaitForSeconds(.5f);

        CountdownUi.SetActive(false);
        Destroy(CountdownUi);
    }
}
