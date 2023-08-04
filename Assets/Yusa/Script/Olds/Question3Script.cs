using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Question3Script : MonoBehaviour
{
    public bool multiColored;
    public List<Toggle> generatedToggles,selectedToggles;
    public GameObject generatedContent, selectedContent;
    public int selectedCount;
    public int correctAnswer = 0;

    int[] sayilar;
    public Color selectedColor;
    // Start is called before the first frame update
    void Start()
    {
        
        SetQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateSelected()
    {
        sayilar = new int[selectedCount];
        //burada int dizisi oluþturuyoruz.Dizinin eleman sayýsý ise 10 (on).
        int i = 0;// "i" adýnda bir int deðiþkeni atýyoruz ve deðiþkenin deðerini 0 (sýfýr)yapýyoruz.
                  //while döngüsü oluþturuyoruz .Bu döngüde içinde yazmýþ olduðumuz 
                  //koþul saðlandýðý sürece sürekli kendini tekrar eder.
        while (i < selectedCount)// "i" ,10 den küçük olduðu sürece döngüye devam eder.
        {
            int sayi = UnityEngine.Random.RandomRange(0, generatedToggles.Count);// 0 ile 50 arasýnda rasgele bir sayý üretip onu "sayi" deðiþkenine atar.
            if (sayilar.Contains(sayi))// "sayilar" dizisinin içinde "sayi" nýn deðeri olup olmadýðýný kontrol eder.
                continue;// Eðer var ise döngünün içindeki baþka hiçbir koda bakmadan devam eder.         
            sayilar[i] = sayi;//Burada "sayi"deðiþkeninin deðerini "sayilar"dizisinin "i" ninci elemanýna atýyoruz.
                              // Yani "i" 5 ise "sayilar" dizisinin 5. elemanýna(sayilar[5]) .
            i++;// "i" deðiþkeninin deðerini 1 arttýrýyoruz.Açýlýmý þu þekildedir ( i= i + 1 ;)
        }//Burada ise koþulu kontrol eder ve koþul saðlanýyor ise devam eder ,saðlanmýyor ise döngü biter.
        Array.Sort(sayilar);//Burada diziyi küçükten büyüðe sýralar.
        foreach (int sayi in sayilar)//Burada dizi içindeki sayýlarý sýrasýyla "sayi"deðiþkenine atar.
            Console.WriteLine(sayi);// "sayi" deðiþkenini yazdýrýr
        Console.ReadKey();//Konsol uygulamasýný sonlandýrmak için sizden herhangi bir tuþa basmanýzý bekler.
    }
    void SetQuestion()
    {
        GenerateSelected();
        foreach (var tog in generatedToggles)
            tog.isOn = false;


        for (int i = 0; i < sayilar.Length; i++)
        {
            generatedToggles[sayilar[i]].isOn = true;
        }
    }

    public void AnswerQuestion(int answer)
    {
        bool isFail = false;
        for(int i = 0; i < generatedToggles.Count; i++)
        {
            if (generatedToggles[i].isOn != selectedToggles[i].isOn)
            {
                isFail = true;
                Debug.Log("Hatalý");
            }
        }

        if (!isFail)
            correctAnswer++;

        foreach (var tog in selectedToggles)
            tog.isOn = false;
        SetQuestion();
    }

    public void ChooseColor(Toggle toggle)
    {
        if (toggle.isOn)
            selectedColor = toggle.image.color;
    }

    public void CheckQuestion()
    {
        bool isFail = false;
        for (int i = 0; i < generatedToggles.Count; i++)
        {
            if (generatedToggles[i].isOn != selectedToggles[i].isOn)
            {
                isFail = true;
                Debug.Log("Hatalý");
            }else if (generatedToggles[i].image.color != selectedToggles[i].image.color)
            {
                isFail = true;
                Debug.Log("Hatalý");
            }
        }

        if (!isFail)
        {
            correctAnswer++;

            foreach (var tog in selectedToggles)
                tog.isOn = false;
            SetQuestion();
        }
    }
}
