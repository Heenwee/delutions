using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    public Text dialougeText;
    private Queue<string> sentences;
    bool isTyping;
    AudioSource source;
    public AudioClip sound;
    public Animator anim;

    #region Singleton
    public static DialougeManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        isTyping = true;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isTyping) DisplayNextSentence();
        if (Input.GetKeyDown(KeyCode.Escape)) EndDialouge();
    }

    public void StartDialouge(Dialouge dialouge)
    {
        sentences.Clear();

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialouge();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            source.Stop();
            source.PlayOneShot(sound);

            dialougeText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }

        isTyping = false;
    }

    void EndDialouge()
    {
        anim.SetTrigger("End");
    }
}
