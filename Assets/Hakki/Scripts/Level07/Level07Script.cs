using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level07Script : MonoBehaviour
{
    [SerializeField] private TMP_InputField _field;
    [SerializeField] private Image image;

    // Start is called before the first frame update
    void Start()
    {
        Create();
    }

    private int levelAmount;

    private void Create()
    {
        levelAmount = Random.Range(0, 101);
        image.fillAmount = levelAmount / 100f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Add(TextMeshProUGUI text)
    {
        _field.text += text.text;
    }

    public void Delete()
    {
        if (_field.text != "")
        {
            _field.text = _field.text.Substring(0, _field.text.Length - 1);
        }
    }

    public void Control()
    {
        
        if (Mathf.Abs(levelAmount - int.Parse(_field.text)) < 5)
        {
            transform.GetComponent<Question>().point += 5;
            _field.text = "";
        }

        Create();
    }
}