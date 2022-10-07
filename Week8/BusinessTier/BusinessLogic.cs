namespace Week8;
using System.Data;
using MySql.Data.MySqlClient;
class BusinessLogic
{
    
    static void Main(string[] args)
    {
        User user;
        GuiTier appGUI = new GuiTier();
        DataTier database = new DataTier();

        //create a login 
        string inputUsername = string.Empty;
        string inputPassword = string.Empty;
        user = appGUI.Login();

        if (database.LoginCheck(user)){


            int option  = appGUI.Dashboard(user);
            switch(option){
                // check enrollment
                case 1:
                    DataTable tableEnrollment = database.CheckEnrollment(user);
                    appGUI.DisplayEnrollment(tableEnrollment);
                    break;
                //  Add A Course
                case 2:
                    break;
                // Drop A Course
                case 3:
                    break;
                // Log Out
                case 4:
                    break;
                // default: wrong input
                default:
                    Console.WriteLine("Wrong Input");
                    break;
            }

        }else{
            Console.WriteLine("Login Failed, Goodbye.");
        }
    }
}