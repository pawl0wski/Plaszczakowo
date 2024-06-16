namespace Plaszczakowo.Components.Pages.Input.InputMethods.GraphCreatorInput.Validator;

public record GraphValidatorError(string Content)
{
    public readonly string Content = Content;
}