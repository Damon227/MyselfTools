using System;
using System.Collections.Generic;

public class StringValue : IComparable<StringValue>
{
    public List<string> Values { get; set; }

    public StringValue(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        string temp = "";
        foreach (char c in value)
        {
            if (c.ToString().IsInt32())
            {
                temp += c.ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(temp))
                {
                    Values.Add(temp);
                    temp = "";
                }
                Values.Add(c.ToString())

            }
        }

        if (!string.IsNullOrEmpty(temp))
        {
            Values.Add(temp);
            temp = "";
        }
    }

    public int CompareTo(StringValue other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));

        int c = (int)Math.Min(Values.Count, other.Values.Count);

        for (int i = 0; i < c; i++)
        {
            if (Values[i].Equals(other.Values[i], StringComparer.Ordinal))
            {
                continue;
            }

            if (Values[i].IsInt32() && !other.Values[i].IsInt32())
            {
                return -1;
            }

            if (!Values[i].IsInt32() && other.Values[i].IsInt32())
            {
                return 1;
            }

            if (Values[i].IsInt32() && other.Values[i].IsInt32())
            {
                return Values[i].AsInt32() > other.Values[i].AsInt32() ? 1 : -1;
            }
        }

        return Values.Length > other.Value.Length ? 1 : -1;
    }

    public override ToString()
    {
        return string.Join("", Values);
    }
}