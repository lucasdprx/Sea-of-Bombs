using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image _image;
    public bool _animation;
    public void Start()
    {
        _animation = true;
    }

    private void Update()
    {
        if (_animation)
        {
            if (_image.color.a > 0)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a - Time.deltaTime);
            }
            else
            {
                Destroy(_image);
                _animation = false;
            }
        }
    }
}
