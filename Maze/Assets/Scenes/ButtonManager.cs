using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button SesButton;
    [SerializeField] private GameObject SesSlider;
    [SerializeField] private GameObject SesToggle;


    public void GameSceneLoad()
    {
        SceneManager.LoadScene(1);
    }

    public void SoundButton()
    {
        SesSlider.SetActive(true);
        SesToggle.SetActive(true);
    }
}
