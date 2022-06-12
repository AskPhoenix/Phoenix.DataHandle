namespace Phoenix.DataHandle.Main.Types
{
    public enum OneTimeCodePurpose
    {
        None = 0,
        Verification,   // Verify that the phone number belongs to the user
        Identification  // Identify a user among many with the same phone number
    }
}
