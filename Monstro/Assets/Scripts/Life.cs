using UnityEngine;

public class Life : MonoBehaviour
{
    RectTransform bar;
    int maxBar = 10;
    int minBar = 190;

    int tamBarTotal;


    void Start()
    {
        bar = GetComponent<RectTransform>();
        tamBarTotal = minBar - maxBar;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeBar((int, int) pack)            //mudar tamanho da barra de acordo com a vida atual e maxima
    {
        print("Teste");
        (int currentLife, int maxLife) = pack;
        float newTam = tamBarTotal * currentLife;
        newTam /= maxLife;
        print(newTam);
        print(bar.offsetMax);
        SetRight(minBar - newTam);
        print(bar.offsetMax);
    }

    void SetRight(float right)                                        //mudar a parte direita
    {
        bar.offsetMax = new Vector2(-right, bar.offsetMax.y);        //vetor canto superior direito
    }
}
