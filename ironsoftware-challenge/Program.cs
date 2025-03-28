using Ninject;

/// <summary>
/// The main program to handle everything
/// </summary>
public class Program {
	public static void Main(string[] args) {		
		Console.WriteLine("Choose langauge: [en/th]");
		var lang = Console.ReadLine()?.Trim().ToLower();
		
		var kernel = new StandardKernel();
		
		if (lang == "th") {
			kernel.Bind<Dictionary<char, string>>().ToConstant(KeyMaps.Thai);
		} else if (lang == "en") {
			kernel.Bind<Dictionary<char,string>>().ToConstant(KeyMaps.English);
		} else {
			Console.WriteLine("Invalid langauge");
			return;
		}

		kernel.Bind<PhonePad>().To<OldPhonePad>();
		// Dictionary<char, string> keyMap = lang?.ToLower() == "th" ? KeyMaps.Thai : KeyMaps.English;
		var pad = kernel.Get<PhonePad>(); 
		
		Console.WriteLine("Enter keypad sequence (end with '#'): ");
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