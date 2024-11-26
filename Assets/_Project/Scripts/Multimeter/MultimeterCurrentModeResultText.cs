using TMPro;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(TMP_Text))]
    public class MultimeterCurrentModeResultText : MonoBehaviour
    {
        private TMP_Text _text;
        private Multimeter _multimeter;
        private MultimeterResultFormatter _multimeterResultFormatter;

        public void Construct(Multimeter multimeter, MultimeterResultFormatter multimeterResultFormatter)
        {
            _text = GetComponent<TMP_Text>();
            _multimeter = multimeter;
            _multimeterResultFormatter = multimeterResultFormatter;

            _multimeter.Ticked += OnTicked;
        }

        private void OnDestroy()
        {
            _multimeter.Ticked -= OnTicked;
        }

        private void OnTicked()
        {
            _text.text = _multimeterResultFormatter.Format(
                _multimeter.GetResult());
        }
    }
}
