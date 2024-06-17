namespace Plaszczakowo.Components.Pages.Input.InputMethods.KeyboardInput.Validator;

public class KeyboardInputValidator
{
    public static List<KeyboardInputError> Validate(string Input)
    {
        List<KeyboardInputError> errors = new();
        if (Input.Length < 1)
            errors.Add(new KeyboardInputError("Fraza jest zbyt krótka!"));

        return errors;
    }
}