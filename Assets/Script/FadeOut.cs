using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image _image;
    public float _speed;
    public void StartAnimation()
    {
        _image.gameObject.SetActive(true);
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(0.1f);
        if (_image.color.a < 1)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a + Time.deltaTime * _speed);
            StartCoroutine(Animation());
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
                ButtonManager.instance.PlayGame();
            else if (SceneManager.GetActiveScene().name == "Shop")
                Card.instance.StartGame();
        }
    }
}
