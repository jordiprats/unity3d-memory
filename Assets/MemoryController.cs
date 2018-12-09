using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MemoryController : MonoBehaviour {

    //recursos
    public Object[] fotos;
    public Object[] noms;

    //ordre
	public int[] llista_alumnes;

	public int[] llista_cartes;

	// random generator
	System.Random rnd;

	float width,height,left_padding, top_padding, card_height;

	GameObject cardHolder;
	GridLayoutGroup cardholder_glg;

	// Use this for initialization
	void Start () 
	{
		width = Screen.currentResolution.width;
		height = Screen.currentResolution.height;

		Debug.Log("screen: w" + width + "h" + height);

		cardHolder = GameObject.Find("CardHolder");

		left_padding = width/3;
		top_padding = height/8;
		card_height = (height/8)*3;

		cardholder_glg = cardHolder.GetComponent<GridLayoutGroup>();

		rnd = new System.Random();

		fotos = Resources.LoadAll(path: "Fotos", systemTypeInstance: typeof(Sprite));
        noms = Resources.LoadAll(path: "Noms", systemTypeInstance: typeof(Sprite));

        Debug.Log("#fotos: " + fotos.Length);
        Debug.Log("#noms: " + noms.Length);

        this.llista_alumnes = new int[fotos.Length];

        for (int i = 0; i < this.llista_alumnes.Length; i++)
        {
            this.llista_alumnes[i] = i;
        }

        //desordenar
		this.llista_alumnes = Enumerable.Range(0, this.llista_alumnes.Length).OrderBy(r => rnd.Next()).ToArray();

		this.llista_cartes = new int[Settings.Cards*2];

		Debug.Log("Settings cartes: "+Settings.Cards);

		Debug.Log("total cartes: "+this.llista_cartes.Length);

		for (int i = 0; i < Settings.Cards; i++)
        {
            this.llista_cartes[i] = llista_alumnes[i];
			Debug.Log("carta["+i+"]="+this.llista_cartes[i]);
			this.llista_cartes[Settings.Cards+i] = llista_alumnes[i];
			Debug.Log("carta["+(Settings.Cards+i)+"]="+this.llista_cartes[i]);
        }

		//desordenar cartes escollides
		this.llista_cartes = Enumerable.Range(0, this.llista_cartes.Length).OrderBy(r => rnd.Next()).ToArray();

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
