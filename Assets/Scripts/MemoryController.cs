using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MemoryController : MonoBehaviour {

    //recursos
    public Object[] fotos;
    public Object[] noms;

	public Object[] llista_sprites;

    //ordre
	public int[] llista_alumnes;

	public int[] llista_cartes;

	// random generator
	System.Random rnd;

	float width,height,left_padding, top_padding, card_height;
	GameObject cardHolder;
	GridLayoutGroup cardholder_glg;

	GameObject selected_item=null;

	int marcador_hits=0;
	int marcador_misses=0;

	bool flip_selected=false;


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

		//cardholder_glg.padding.left = (int)left_padding;
		//cardholder_glg.padding.top = (int)top_padding;

		rnd = new System.Random();

		fotos = Resources.LoadAll(path: "Fotos", systemTypeInstance: typeof(Sprite));
        noms = Resources.LoadAll(path: "Noms", systemTypeInstance: typeof(Sprite));

		Debug.Log("level#: " + Settings.Level);
		Debug.Log("card#: " + Settings.Cards);

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
		this.llista_sprites = new Object[Settings.Cards*2];

		Debug.Log("Settings cartes: "+Settings.Cards);

		Debug.Log("total cartes: "+this.llista_cartes.Length);

		for (int i = 0; i < Settings.Cards; i++)
        {
            this.llista_cartes[i] = llista_alumnes[i];
			//Debug.Log("carta["+i+"]="+this.llista_cartes[i]);
			// afegeix parella a N+i
			this.llista_cartes[Settings.Cards+i] = llista_alumnes[i]; 
			//Debug.Log("carta["+(Settings.Cards+i)+"]="+this.llista_cartes[i]);

			switch (Settings.Level)
			{
				case 0:
					// facil
					Debug.Log("*X facil");
					this.llista_sprites[i] = fotos[this.llista_cartes[i]];
					Debug.Log("sprite["+i+"]="+this.llista_sprites[i].name);
					this.llista_sprites[Settings.Cards+i] = fotos[this.llista_cartes[i]];
					Debug.Log("sprite["+(Settings.Cards+i)+"]="+this.llista_sprites[(Settings.Cards+i)].name);
					break;
				case 1:
					//mig
					Debug.Log("*X mig");
					this.llista_sprites[i] = noms[this.llista_cartes[i]];
					this.llista_sprites[Settings.Cards+i] = noms[this.llista_cartes[i]];
					Debug.Log((fotos[this.llista_cartes[i]].name));
					break;
				default:
					// dificil
					Debug.Log("*X dificil");
					this.llista_sprites[i] = fotos[this.llista_cartes[i]];
					this.llista_sprites[Settings.Cards+i] = noms[this.llista_cartes[i]];
					Debug.Log((fotos[this.llista_cartes[i]].name));
					break;
			}


        }

		//Debug.Log("PRE RANDOM - this.llista_sprites: ");
		//for (int i = 0; i < this.llista_sprites.Length; i++)
		//{
		//	Debug.Log(i+": "+this.llista_sprites[i].name);
		//}

		Object mytmpobj;
		int mytmpint;
		
		//desordenar cartes escollides - inclou les parelles
		for (int i = 0; i < this.llista_sprites.Length; i++)
		{
			int random_index = rnd.Next(i, this.llista_sprites.Length);
			if(random_index==i) continue;
			Debug.Log("randomize: "+i+" <-> "+random_index);
			Debug.Log("randomize: "+this.llista_sprites[i].name+" <-> "+this.llista_sprites[random_index].name);
			mytmpint = this.llista_cartes[i];
			mytmpobj = this.llista_sprites[i];

			this.llista_cartes[i] = this.llista_cartes[random_index];
			this.llista_sprites[i] = this.llista_sprites[random_index];

			this.llista_cartes[random_index] = mytmpint;
			this.llista_sprites[random_index] = mytmpobj;
		}

		Debug.Log("this.llista_cartes: ");
		for (int i = 0; i < this.llista_cartes.Length; i++)
		{
			Debug.Log(i+": "+this.llista_cartes[i]);
		}

		Debug.Log("this.llista_sprites: ");
		for (int i = 0; i < this.llista_sprites.Length; i++)
		{
			Debug.Log(i+": "+this.llista_sprites[i].name);
		}

		for (int i = 0; i < this.llista_sprites.Length; i++)
		{
			GameObject newGO=new GameObject(this.llista_cartes[i]+" "+this.llista_sprites[i].name);
			newGO.AddComponent(typeof(SpriteRenderer));
			newGO.AddComponent(typeof(RectTransform));
			newGO.AddComponent(typeof(BoxCollider2D));

			//newGO.transform.parent = cardholder_glg.transform;
			newGO.transform.SetParent(cardholder_glg.transform);
         	//newGO.transform.localScale = new Vector3( 1, 1, 1 );
         	//newGO.transform.localPosition = Vector3.zero;

			newGO.GetComponent<SpriteRenderer>().sprite = this.llista_sprites[i] as Sprite;
			newGO.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
            //newGO.GetComponent<SpriteRenderer>().size += new Vector2(0.05f, 0.01f);
			//newGO.GetComponent<SpriteRenderer>().size = new Vector2(5,6);

			newGO.GetComponent<BoxCollider2D>().size = newGO.GetComponent<SpriteRenderer>().sprite.bounds.size;
			
		}

	}

	void Update () 
	{
		if(Input.GetMouseButton(0))
		{
			var inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
			if (touches.Length > 0)
			{
				//Debug.Log("touche!");
				var hit = touches[0];
                if (hit.transform != null)
                {
					if(selected_item==null)
					{
						if(!flip_selected)
						{
							Debug.Log("seleccionat "+hit.transform.gameObject.name);
							selected_item = hit.transform.gameObject;
							flip_selected=true;
						}
						else Debug.Log("skipped select");
					}
					else
					{
						if(hit.transform.position!=selected_item.transform.position)
						{
							Debug.Log(hit.transform.gameObject.name+" <> "+selected_item.name);
							Debug.Log(hit.transform.gameObject.transform.position+" <> "+selected_item.transform.position);
							if(hit.transform.gameObject.name==selected_item.name)
							{
								Debug.Log("hit");
								marcador_hits++;
							}
							else
							{
								Debug.Log("miss");
								marcador_misses++;
							}
							selected_item=null;
							flip_selected=true;
						}
					}
				}
			}
		}
		else
		{
			flip_selected=false;
		}
	}
}
