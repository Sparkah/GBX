using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class WinGameScript : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private float _imgAlpha;
        
        private void OnEnable()
        {
            Debug.Log("Win game");
            _imgAlpha = _image.color.a;
            StartCoroutine(FadeScreen());
        }

        private IEnumerator FadeScreen()
        {
            yield return new WaitForFixedUpdate();
            _imgAlpha += Time.fixedDeltaTime;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _imgAlpha);
            if(_imgAlpha<1)
                StartCoroutine(FadeScreen());
        }
    }
}