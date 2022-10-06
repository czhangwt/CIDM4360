class GuiTier{
    public void Login(out string username, out string password){
        Console.WriteLine("------Welcome to Course Management System------");
        Console.WriteLine("Please input username: ");
        Console.WriteLine("Please input password: ");
        username = Console.ReadLine();
        password = Console.ReadLine();
    }
    public void Dashboard(string username){
        DateTime localDate = DateTime.Now;
        Console.WriteLine($"Hello: {username}; Date/Time: {localDate.ToString("en-US")}");
    }
}