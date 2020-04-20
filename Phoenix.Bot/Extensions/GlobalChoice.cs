using Microsoft.Bot.Builder.Dialogs.Choices;

namespace Phoenix.Bot.Extensions
{
    /// <summary>
    /// Represents a Choice that can be accessed by multiple ChoicePrompts accross a dialog.
    /// </summary>
    public class GlobalChoice : Choice
    {
        /// <summary>
        /// The index of the step in the dialog that the GlobalChoice appears.
        /// </summary>
        public int StepIndex { get; set; }

        /// <summary>
        /// Enable or disable the GlobalChoice.
        /// </summary>
        public bool IsActive { get; set; }

        public GlobalChoice(string value = null, int stepIndex = 0, bool isActive = true)
            : base(value)
        {
            this.StepIndex = stepIndex;
            this.IsActive = IsActive;
        }
    }
}
