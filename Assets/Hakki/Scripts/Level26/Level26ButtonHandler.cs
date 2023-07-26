using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Level26ButtonHandler : MonoBehaviour
{
    private Button btn;

    [SerializeField] List<Color> _colors;

    public int colorIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => ChangeColor());
    }

    void ChangeColor()
    {
        colorIndex = colorIndex++ < 2 ? colorIndex : 0;
        transform.GetComponent<Image>().color = _colors[colorIndex];
        Level26Scripts.Instance.Control();
    }
}
