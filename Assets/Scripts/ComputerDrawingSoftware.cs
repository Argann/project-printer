using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComputerDrawingSoftware : MonoBehaviour
{
    [SerializeField]
    private Button _drawButton;

    [SerializeField]
    private RawImage _drawingZone;

    [SerializeField]
    private TMP_InputField _codeField;

    void OnEnable()
    {
        _drawButton.onClick.AddListener(OnDrawButtonPressed);
    }

    void OnDisable()
    {
        _drawButton.onClick.RemoveListener(OnDrawButtonPressed);
    }

    void OnDrawButtonPressed()
    {
        Texture2D texture = CodeParser.Parse(_codeField.text);

        _drawingZone.texture = texture;
    }
}
