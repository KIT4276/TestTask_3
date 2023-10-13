using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SpeedSlider : MonoBehaviour
{
    private Slider _slider;

    [SerializeField]
    private TouchSystem _touchSystem;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        ChangeSpeed();
    }

    public void ChangeSpeed()
    {
        _touchSystem.ChangeMoveTime(_slider.value);
    }
}
