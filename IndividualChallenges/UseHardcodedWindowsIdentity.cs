public void UseHardcodedWindowsIdentity()
{
    // Insecure: Creates a Windows identity with hardcoded credentials
    WindowsIdentity identity = WindowsIdentity.GetCurrent();
    Console.WriteLine("Using Windows identity: " + identity.Name);
}
