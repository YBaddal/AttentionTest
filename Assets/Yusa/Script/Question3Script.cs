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
        //burada int dizisi olu�turuyoruz.Dizinin eleman say�s� ise 10 (on).
        int i = 0;// "i" ad�nda bir int de�i�keni at�yoruz ve de�i�kenin de�erini 0 (s�f�r)yap�yoruz.
                  //while d�ng�s� olu�turuyoruz .Bu d�ng�de i�inde yazm�� oldu�umuz 
                  //ko�ul sa�land��� s�rece s�rekli kendini tekrar eder.
        while (i < selectedCount)// "i" ,10 den k���k oldu�u s�rece d�ng�ye devam eder.
        {
            int sayi = UnityEngine.Random.RandomRange(0, generatedToggles.Count);// 0 ile 50 aras�nda rasgele bir say� �retip onu "sayi" de�i�kenine atar.
            if (sayilar.Contains(sayi))// "sayilar" dizisinin i�inde "sayi" n�n de�eri olup olmad���n� kontrol eder.
                continue;// E�er var ise d�ng�n�n i�indeki ba�ka hi�bir koda bakmadan devam eder.         
            sayilar[i] = sayi;//Burada "sayi"de�i�keninin de�erini "sayilar"dizisinin "i" ninci eleman�na at�yoruz.
                              // Yani "i" 5 ise "sayilar" dizisinin 5. eleman�na(sayilar[5]) .
            i++;// "i" de�i�keninin de�erini 1 artt�r�yoruz.A��l�m� �u �ekildedir ( i= i + 1 ;)
        }//Burada ise ko�ulu kontrol eder ve ko�ul sa�lan�yor ise devam eder ,sa�lanm�yor ise d�ng� biter.
        Array.Sort(sayilar);//Burada diziyi k���kten b�y��e s�ralar.
        foreach (int sayi in sayilar)//Burada dizi i�indeki say�lar� s�ras�yla "sayi"de�i�kenine atar.
            Console.WriteLine(sayi);// "sayi" de�i�kenini yazd�r�r
        Console.ReadKey();//Konsol uygulamas�n� sonland�rmak i�in sizden herhangi bir tu�a basman�z� bekler.
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
                Debug.Log("Hatal�");
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
                Debug.Log("Hatal�");
            }else if (generatedToggles[i].image.color != selectedToggles[i].image.color)
            {
                isFail = true;
                Debug.Log("Hatal�");
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
