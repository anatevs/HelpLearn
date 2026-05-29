using System.Collections;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class PlayerLooking : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text[] _nameText = new TMP_Text[2];

        [SerializeField]
        private MeshRenderer[] _colorRenderers;

        public void SetName(string name)
        {
            for (int i = 0; i < _nameText.Length; i++)
            {
                _nameText[i].text = name;
            }
        }

        public void SetColor(Color color)
        {
            for (int i = 0; i < _colorRenderers.Length; i++)
            {
                _colorRenderers[i].material.color = color;
            }
        }
    }
}