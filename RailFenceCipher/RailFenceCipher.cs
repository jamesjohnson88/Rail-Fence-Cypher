namespace RailFenceCipher;

public static class RailFenceCipher
{
    public static string Encode(int key, string input)
    {
        var index = 0;
        var hasBottomed = false;
        var array = new string[key];

        foreach (var character in input.Replace(" ", string.Empty))
        {
            array[index] += character.ToString();
            index += hasBottomed ? -1 : 1;
            
            if (index == key || index <= 0 && hasBottomed)
            {
                hasBottomed = !hasBottomed;
                index += hasBottomed ? -2 : index < 0 ? 2 : 0;
            }
        }
        
        return string.Concat(array); 
    }
    
    public static string Decode(int key, string input)
    {
        var array = new string[key];

        PopulateRails(key, input, array);
        ReplaceRailChars(key, input, array);

        var index = 0;
        var output = string.Empty;
        
        for (var i = 0; i < input.Length; i++)
        {
            output += array.Where(x => x[index] != '§').Select(x => x[index]).FirstOrDefault();
            index++;
        }
        
        return output;
    }

    private static void PopulateRails(int key, string input, IList<string> array)
    {
        var index = 0;
        var hasBottomed = false;
        
        foreach (var _ in input.Replace(" ", string.Empty))
        {
            for (var i = 0; i < array.Count; i++)
            {
                if (index == i)
                {
                    array[i] += "±";
                }
                else
                {
                    array[i] += "§";
                }
            }

            index += hasBottomed ? -1 : 1;

            if (index == key || index <= 0 && hasBottomed)
            {
                hasBottomed = !hasBottomed;
                index += hasBottomed ? -2 : index < 0 ? 2 : 0;
            }
        }
    }
    
    private static void ReplaceRailChars(int key, string input, IList<string> array)
    {
        var charIndex = 0;
        for (var i = 0; i < key; i++)
        {
            while (array[i].Contains('±'))
            {
                array[i] = ReplaceFirst(array[i], "±", input[charIndex].ToString());
                charIndex++;
            }
        }
    }
    
    private static string ReplaceFirst(string text, string search, string replace)
    {
        var indexOf = text.IndexOf(search, StringComparison.Ordinal);
        
        return indexOf < 0 ? text 
            : string.Concat(text.AsSpan(0, indexOf), replace, text.AsSpan(indexOf + search.Length));
    }
}