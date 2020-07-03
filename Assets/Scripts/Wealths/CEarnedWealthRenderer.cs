using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Wealths
{
    public class CEarnedWealthRenderer : MonoBehaviour
    {
        private Text _text;
        public long value;
    
    
        void Start()
        {
            _text = GetComponent<Text>();
            if(value >= 0)
                _text.text = "+" + UnitConversion.ConverseUnit(value).ConversedUnitToString();
            else
                _text.text = "-" + UnitConversion.ConverseUnit(value).ConversedUnitToString();
            transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }

        void FixedUpdate()
        {
            transform.position += new Vector3(0,-0.02f,0);
            var color = _text.color;
            color = new Color(color.r,color.g,color.b,color.a - 0.05f);
            _text.color = color;
        
            if(color.a < 0)
                Destroy(gameObject);
        }
    }
}
