using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaspeGenerator : MonoBehaviour
{
    [SerializeField] ProbabiltyValue[] probabiltyArr;
    [SerializeField] int quantity;
    [SerializeField] Cat catPref;
    [SerializeField] Transform parent;
    [SerializeField] float spawnTime;
    [SerializeField] AudioSource aSource;

    public void GenerateCats()
    {
        StartCoroutine(CreateCatsCoroutine());
    }

    IEnumerator CreateCatsCoroutine()
    {
        float[] raspesValues = new MowRandomizer(probabiltyArr).GetValues(quantity);
        int raspeCount = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                float x = i * 1.5f;
                float z = j * 1.5f;

                Cat cat = Instantiate(catPref, new Vector3(x,0,z), Quaternion.Euler(0,180,0),parent);
                
                yield return new WaitForSeconds(spawnTime);           
                
                cat.SetCat(raspesValues[raspeCount], aSource);                
                
                raspeCount++;
                if(raspeCount >= quantity)
                {
                    cat.SetState(CatStateType.Selection);
                }
                
            }
        }
        aSource.Play();
    }
}
