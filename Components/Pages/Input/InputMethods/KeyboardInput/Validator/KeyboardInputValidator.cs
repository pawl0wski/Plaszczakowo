namespace Plaszczakowo.Components.Pages.Input.InputMethods.KeyboardInput.Validator;

public class KeyboardInputValidator
{
    public static List<KeyboardInputError> Validate(string Input)
    {
        List<KeyboardInputError> errors = new();
        List<char> distinctChars = new();
        if (Input.Length < 2)
            errors.Add(new("Fraza jest zbyt krótka!"));
        foreach (char c in Input)
        {
            if (!distinctChars.Contains(c))
                distinctChars.Add(c);
        }

        if (distinctChars.Count < 2)
            errors.Add(new("Niewystarczająca ilość różnych liter!"));

        return errors;
    }
}