using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MemoryController : MonoBehaviour {

    //recursos
    public Object[] fotos;
    public Object[] noms;

    //ordre
	public int[] llista_alumnes;

	// random generator
	System.Random rnd;

	// Use this for initialization
	void Start () {
		rnd = new System.Random();

		fotos = Resources.LoadAll("Fotos", typeof(Sprite));
        noms = Resources.LoadAll("Noms", typeof(Sprite));

        Debug.Log("#fotos: " + fotos.Length);
        Debug.Log("#noms: " + noms.Length);

        this.llista_alumnes = new int[fotos.Length];

        for (int i = 0; i < this.llista_alumnes.Length; i++)
        {
            this.llista_alumnes[i] = i;
        }

        //desordenar
		llista_alumnes = Enumerable.Range(0, this.llista_alumnes.Length).OrderBy(r => rnd.Next()).ToArray();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
