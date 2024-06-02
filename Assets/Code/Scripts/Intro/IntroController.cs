using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroController : MonoBehaviour
{
    public string startGame;

    [SerializeField] VideoPlayer _videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer.loopPointReached += FinishedIntro;
    }
    public void FinishedIntro(VideoPlayer vp)
    {
        SceneManager.LoadScene(startGame);
    }
    public void SkipIntro()
    {
        SceneManager.LoadScene(startGame);
    }
}
