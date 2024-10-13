using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HpCanvasScript : MonoBehaviour
{
    [SerializeField] private Image _image1;
    [SerializeField] private Image _image2;
    [SerializeField] private Image _image3;
    [SerializeField] private Image _image4;
    [SerializeField] private Image _image5;
    private Image[] _images;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    private void Start()
    {
        _images= new Image[]{_image1,_image2,_image3,_image4,_image5};
    }

    private void Update()
    {
        for(int i=0;i<_images.Length;i++)
        {
            if(i<playerScript.playerHP)
            {
                _images[i].sprite=_fullHeart;
            }
            else
            {
                _images[i].sprite=_emptyHeart;
            }
        }
    }
    public void TestFunc1()
    {
        playerScript.playerHP=3;
    }
    public void TestFunc2()
    {
        playerScript.playerHP--;
    }
    public void TestFunc3()
    {
        playerScript.playerHP++;
    }
}
