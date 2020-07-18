using UnityEngine;

[CreateAssetMenu]
public class Product : ScriptableObject
{
    [SerializeField] private IComeInProducts[] itemsIncluded;

    private void OnValidate()
    {
        foreach (IComeInProducts item in itemsIncluded)
        {
            string[] itemsProducts = item.GetProductsIncludedWith();
            foreach (string product in itemsProducts)
            {
                if (product == this.name)
                {
                    Debug.Log(item.GetType() + " " + item.GetName() + " and " + this.GetType() + " " + this.name + " already reciprocate.");
                }
            }
        }
    }
}


/*
 * if (item.GetName() == this.name)
            {
                Debug.Log(item.GetType() + " " + item.GetName() + " and " + this.GetType() + " " + this.name + " already reciprocate.");
            }
*/