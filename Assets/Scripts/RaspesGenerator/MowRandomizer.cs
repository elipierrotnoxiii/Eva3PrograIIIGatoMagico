using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowRandomizer 
{
    ProbabiltyValue[] probabiltyArr;

    public MowRandomizer(ProbabiltyValue[] _probabiltyArr)
    {
        probabiltyArr = _probabiltyArr;
        float percentCount = 0;
        foreach (var item in probabiltyArr)
        {
            percentCount += item.probabilty;
        }
        if(percentCount != 1)
        {
            Debug.LogError("Las suma de las probabilidades deben ser igual a 1");
        }
        
    }

    public float[] GetValues(int quantity)
    {
        float[] values = new float[quantity];
        bool isSearching = true;
        int whileCount = 0;
        while (isSearching)
        {
            List<float> valuesList = RandomValues(quantity);
            values = valuesList.ToArray();
            isSearching = !IsProbabilityCorrect(valuesList);
            whileCount++;
            //Debug.Log("Try count = " + whileCount);
            if(whileCount >= 10000)
            {
                isSearching = false;
                Debug.LogError("I Cant Make This");
            }
        }
        return values;
    }

    List<float> RandomValues(int quantity)
    {
        List<float> values = new List<float>();

        for (int i = 0; i < quantity; i++)
        {
            values.Add(probabiltyArr[Random.Range(0, probabiltyArr.Length)].value);
        }
        return values;
    }

    bool IsProbabilityCorrect(List<float> randomValues)
    {
        foreach (var item in probabiltyArr)
        {
            List<float> allItemValues = randomValues.FindAll(x => x == item.value);
            float itemQuantity = allItemValues.Count;

            float percent = itemQuantity / randomValues.Count;
            //Debug.Log("Percent = " + percent);
            if(percent < item.probabilty-0.05f || percent > item.probabilty + 0.05f)
            {
                return false;
            }         
        }
        return true;
    }
}
