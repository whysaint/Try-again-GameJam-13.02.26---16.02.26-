using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button play;

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
