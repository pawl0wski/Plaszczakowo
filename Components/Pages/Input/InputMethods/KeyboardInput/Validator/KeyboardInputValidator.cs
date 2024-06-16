namespace Plaszczakowo.Components.Pages.Input.InputMethods.KeyboardInput.Validator;

public static class KeyboardInputValidator
{
    public static List<KeyboardInputError> Validate(string Input)
    {
        List<KeyboardInputError> errors = new();
        List<char> distinctChars = new();
        if (Input.Length < 2)
            errors.Add(new KeyboardInputError("Fraza jest zbyt krótka!"));
        foreach (var c in Input)
            if (!distinctChars.Contains(c))
                distinctChars.Add(c);

        if (distinctChars.Count < 2)
            errors.Add(new KeyboardInputError("Niewystarczająca ilość różnych liter!"));

        return errors;
    }
}