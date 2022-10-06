namespace Week8;

class BusinessTier
{
    
    static void Main(string[] args)
    {
        GuiTier AppGUI = new GuiTier();
        string inputUsername = string.Empty;
        string inputPassword = string.Empty;
        AppGUI.Login(out inputUsername, out inputPassword);

    }
}
