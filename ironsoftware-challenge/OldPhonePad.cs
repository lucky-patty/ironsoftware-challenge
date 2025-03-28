using System.Text;

/// <summary>
/// Represent Old mobile phone pad 
/// </summary>
public class OldPhonePad : PhonePad {

    /// <summary>
    /// Create mapping to check whether which lang we choose
    /// </summary>
    private readonly Dictionary<char, string> _keyMap;

    /// <summary>
    /// Pass keyMap as param so we can know whether which one we will use
    /// </summary>
    /// <param name="keyMap"></param>
    public OldPhonePad(Dictionary<char, string> keyMap) {
        _keyMap = keyMap;
    }


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
		if (!_keyMap.ContainsKey(key)) 
			return;
			
		var l = _keyMap[key];
		if (l.Length == 0) 
			return;
		
        // Cycle through characters
		int index = (seq.Length - 1) % l.Length;
		res.Append(l[index]);
	}
}
