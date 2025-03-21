using System.Text;

/// <summary>
/// Represent Old mobile phone pad 
/// </summary>
public class OldPhonePad {
    /// <summary>
    /// Mapping from number keys to characters
    /// </summary>
	private static readonly Dictionary<char, string> KeyMap = new() {
		{ '1', "&'("},
		{ '2', "abc"},
		{ '3', "def"},
		{ '4', "ghi"},
		{ '5', "jkl"},
		{ '6', "mno"},
		{ '7', "pqrs"},
		{ '8', "tuv"},
		{ '9', "wxyz"},
		{ '0', " "}
	};
	
    /// <summary>
    /// Convert keypad sequence into the message
    /// </summary>
    /// <param name="input">The input should end with '#' </param>
    /// <returns>The decode message will return as uppercase letters (Not sure since I saw output as uppercase) </returns>
    /// <exception cref="ArgumentException">Thrown if input is null, empty or does not end with # </exception>
	public string ConvertMessage(string input) {
		if (string.IsNullOrEmpty(input) || input[^1] != '#') 
			throw new ArgumentException("Input must end with '#'");
		
            // Remove trailing '#' before
			input = input[..^1];
			var res = new StringBuilder();
			
			var current = new StringBuilder();
			for (int i = 0; i < input.Length; i++)
			{
				char c = input[i];
				
				if (c == '*') {
                    // Remove last character
					if (res.Length > 0) 
						res.Length--;
				} else if (c == ' ') {
                    // Space is pause, clear the current
					AppendSequence(res, current);
					current.Clear();
				} else if (char.IsDigit(c)) {
                    // If digit changes, clear the current
					if (current.Length > 0 && current[0] != c) {
						AppendSequence(res, current);
						current.Clear();
					}
					current.Append(c);
				}
			}
			
            // Add it to remaining 
			AppendSequence(res, current);
			return res.ToString().ToUpper();
	}
			
    /// <summary>
    /// Add decoded character to the result
    /// </summary>
    /// <param name="res">Result that StringBuilder will add</param>
    /// <param name="seq">Sequence of repeated digits to decode</param>
	private void AppendSequence(StringBuilder res, StringBuilder seq) {
		if (seq.Length == 0) 
			return;
		char key = seq[0];
		if (!KeyMap.ContainsKey(key)) 
			return;
			
		var l = KeyMap[key];
		if (l.Length == 0) 
			return;
		
        // Cycle through characters
		int index = (seq.Length - 1) % l.Length;
		res.Append(l[index]);
	}
}

/// <summary>
/// The main program to handle everything
/// </summary>
public class Program {
	public static void Main(string[] args) {
        var pad = new OldPhonePad(); // Init the OldPhone    
        Console.WriteLine("Enter sequence (end with '#'): ");
        string? input = Console.ReadLine();

        // We have to check null value
        if (string.IsNullOrEmpty(input)) {
            Console.WriteLine("No input");
            return;
        }

        try {
            string message = pad.ConvertMessage(input);
            Console.WriteLine("Decoded Message: " + message);
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
	}
}