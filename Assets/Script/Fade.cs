using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image _image;
    public float _speed;
    public void Start()
    {
        StartCoroutine(Animation()); 
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(0.1f);
        if (_image.color.a > 0)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a - Time.deltaTime * _speed);
            StartCoroutine(Animation());
        }
        else
        {
            Destroy(_image);
        }
    }
}
