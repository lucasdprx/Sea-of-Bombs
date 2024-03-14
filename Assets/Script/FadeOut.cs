using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image _image;
    public bool _animation;
    public void StartAnimation()
    {
        _image.gameObject.SetActive(true);
        _animation = true;
    }

    private void Update()
    {

        if (_animation)
        {
            if (_image.color.a < 1)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a + Time.deltaTime);
            }
            else
            {
                _animation = false;
                if (SceneManager.GetActiveScene().name == "MainMenu")
                    ButtonManager.instance.PlayGame();
                else if (SceneManager.GetActiveScene().name == "Shop")
                    Card.instance.StartGame();
            }
        }
    }
}
