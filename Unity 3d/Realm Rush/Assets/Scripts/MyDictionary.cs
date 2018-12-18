using System.Linq;
using System.Collections.Generic;

public class MyDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{

    public override string ToString()
    {
        string concat = "";
        for (int i = 0; i < Count; i++)
        {
            concat += "Key: " + this.ElementAt(i).Key + " Value: " + this.ElementAt(i).Value + "\n";
        }

        return concat;
    }

}
