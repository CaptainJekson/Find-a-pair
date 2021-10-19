using DG.Tweening;
using TMPro;

namespace CJ.FindAPair.Utility
{
    public static class DOTweenExtension
    {
        public static void ChangeOfNumericValueForText(this TextMeshProUGUI text, int value, int newValue, 
            float duration, Ease ease = Ease.Linear)
        {
            DOTween.To(() => value, x => value = x, newValue, duration);

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(duration / 1000);
            sequence.AppendCallback(() => text.SetText(value.ToString()));
            sequence.SetLoops(1000, LoopType.Incremental).SetEase(ease);
        }
    }
}