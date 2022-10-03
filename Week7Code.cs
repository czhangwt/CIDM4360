// -----------Week7 Lecture1----------

// Remote Database Setting
Server Type: MySQL
Host: DB_IP_Address
Port: DB_PORT (default: 3306)
Username/Password/Database: which is the name of YOUR WT Email. For example, my email is abc@wtamu.edu, so my username, password and database are abc
Then click Connect button

// Create Student, Course, Enrollment Table
CREATE TABLE Student(  
    studentID INT NOT NULL,
    studentName VARCHAR(50),
    PRIMARY KEY (studentID)
) ;

CREATE TABLE Course(  
    courseID INT NOT NULL,
    courseName VARCHAR(50),
    courseCredit INT NOT NULL,
    PRIMARY KEY (courseID)
) ;

CREATE Table Enrollment (
    studentID INT NOT NULL,
    courseID INT NOT NULL,
    semester VARCHAR(20),
    PRIMARY KEY(studentID, courseID),
    Foreign Key (studentID) REFERENCES Student(studentID),
    Foreign Key (courseID) REFERENCES Course(courseID)
);

// Insert Records to Student, Course, Enrollment Tables
INSERT INTO Course(courseID, courseName, courseCredit) VALUES(2315, 'C#',3);
INSERT INTO Course VALUES (4360, 'OOAD', 3);
INSERT INTO Student VALUES (111,'Alice'), (222, 'Bob');
INSERT INTO Enrollment VALUES (111,2315,'Fall2022'),(222,4360,'Fall2022');

// Join Tables With Foreign Keys
SELECT * FROM Course, Student, Enrollment
WHERE
    Course.courseID = Enrollment.courseID
    AND
    Student.studentID = Enrollment.studentID;

// Create View for studentName and courseName
CREATE VIEW StudentCourse
AS
SELECT studentName, courseName FROM Course, Student, Enrollment
WHERE
    Course.courseID = Enrollment.courseID
    AND
    Student.studentID = Enrollment.studentID;
// Check All Records in StudentCourse View
SELECT * FROM StudentCourse;






// -----------Week7 Lecture2----------
// SQL Code for Creating User Table in Remote Database:
create table User (
    username VARCHAR(50),
    password VARCHAR(50),
    role VARCHAR(50)
);
insert into User (username, password, role) values ('cwoolward0', 'y1XJu9bKgoP', 'Business Development');
insert into User (username, password, role) values ('dmacclay1', 'ih6Pg9k', 'Business Development');
insert into User (username, password, role) values ('icockren2', '75cZn2T3tNt7', 'Human Resources');
insert into User (username, password, role) values ('cwhartonby3', 'zD9B4BlCCuUU', 'Research and Development');
insert into User (username, password, role) values ('nbrien4', 'HPEbpMo3Z', 'Legal');
insert into User (username, password, role) values ('acoutthart5', '9waoemzJXlh5', 'Accounting');
insert into User (username, password, role) values ('pmarkham6', 'UgY5qzNR35Q', 'Legal');
insert into User (username, password, role) values ('ijonson7', 'da9ButUNb', 'Marketing');
insert into User (username, password, role) values ('sscotchforth8', 'HoFdP7Y2U01k', 'Product Management');
insert into User (username, password, role) values ('sjosefson9', '6I2jO8Z', 'Engineering');
insert into User (username, password, role) values ('dswantona', 'lln29r66Qa', 'Human Resources');
insert into User (username, password, role) values ('ecallabyb', 'HkWgJg8', 'Support');
insert into User (username, password, role) values ('ckinkerc', 'ZzO6Lo', 'Marketing');
insert into User (username, password, role) values ('ihalleyd', '5rzGoc7', 'Human Resources');
insert into User (username, password, role) values ('cmasseye', 'lCQPVLotxkx', 'Services');
insert into User (username, password, role) values ('fyeldonf', '2geuebY5s', 'Training');
insert into User (username, password, role) values ('bpaslowg', 'BpbENQ5', 'Human Resources');
insert into User (username, password, role) values ('eiddiensh', 'FO4eoHh', 'Support');
insert into User (username, password, role) values ('zgetcliffi', '67KLtXGmAD', 'Services');
insert into User (username, password, role) values ('oiwanickij', 'lN12wDHN', 'Training');
insert into User (username, password, role) values ('jinglesantk', '3Lfv2ipxr', 'Legal');
insert into User (username, password, role) values ('kbridgenl', 'SJfqnJ', 'Engineering');
insert into User (username, password, role) values ('ageraldinim', 'FMq50mdOnrQY', 'Training');
insert into User (username, password, role) values ('astuttmann', 'OVwUCRXUe', 'Services');


// ----------------- Example1: Connect Remote Database in C#
namespace Week7Lecture2;
using System.Data;
// import MySQL package for connecting remote database
using MySql.Data;
using MySql.Data.MySqlClient;
class Program
{
    static void Main(string[] args)
    {
        string connStr = "server=DB_IP_Address;user=YOUR_USERNAME;database=YOUR_DATABASE;port=8080;password=YOUR_PASSWORD";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            // Find all users whose roles are Engineering;
            // We put SQL Comman in "", the string value in SQL is in ''
            string sql = "SELECT username, password, role FROM User WHERE role='Engineering'";
           
            // the SQL query to be executed is passed to the MySqlCommand
            MySqlCommand cmd = new MySqlCommand(sql, conn);
           
            // ExecuteReader to query the database.
            // The MySqlReader object contains the results generated by the SQL executed on the MySqlCommand object.
            MySqlDataReader rdr = cmd.ExecuteReader();

            // the information stored in MySqlReader is printed out by a while loop
            while (rdr.Read())
            {
                Console.WriteLine($"Username: {rdr[0]} -- Password: {rdr[1]} -- Role: {rdr[2]}");
            }
            // MySqlReader object is disposed of by invoking the Close method
            rdr.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        conn.Close();
        Console.WriteLine("Done.");
    }
}

// ----------------- Example2:Working SQL with Parameter
namespace Week7Lecture2;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class Program
{
    static void Main(string[] args)
    {
        string connStr = "server=DB_IP_Address;user=YOUR_USERNAME;database=YOUR_DATABASE;port=8080;password=YOUR_PASSWORD";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
           
            // The parameter is preceded by an '@' symbol to indicate it is to be treated as a parameter.
            string sql = "SELECT username, role FROM User WHERE role= @role";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
           
            // get a role value from keyboard
            Console.WriteLine("Enter a role e.g.:Engineering, Support, Services, Training");
            string user_input = Console.ReadLine();
            // assign input value to the SQL command
            cmd.Parameters.AddWithValue("@role", user_input);
           
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {   // select columon based on column name
                Console.WriteLine($"Username: {rdr["username"]} -- Role: {rdr["role"]}");
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        conn.Close();
        Console.WriteLine("Done.");
    }
}

// ----------------- Example3: Insert/Update/Delete Records
namespace Week7Lecture2;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class Program
{
    static void Main(string[] args)
    {
        string connStr = "server=DB_IP_Address;user=YOUR_USERNAME;database=YOUR_DATABASE;port=8080;password=YOUR_PASSWORD";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
           
            string sql = "INSERT INTO User VALUES (@username, @password, @role)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
           
            // input information for new record
            Console.WriteLine("Enter Username:");
            string input_username = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string input_password = Console.ReadLine();
            Console.WriteLine("Enter Role:");
            string input_role = Console.ReadLine();
            // assign input values to the SQL command
            cmd.Parameters.AddWithValue("@username", input_username);
            cmd.Parameters.AddWithValue("@password", input_password);
            cmd.Parameters.AddWithValue("@role", input_role );
           
            // ExecuteNonQuery to insert, update, and delete data.
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        conn.Close();
        Console.WriteLine("Done.");
    }
}

// ----------------- Example 4: Working with Stored Procedure

DELIMITER //
CREATE PROCEDURE LoginCount (IN inputUsername CHAR(20), IN inputPassword CHAR(20), OUT userCount INT)
BEGIN
  SELECT COUNT(*) INTO userCount FROM User
        WHERE
            User.username = inputUsername
            AND
            User.password = inputPassword;

END //
DELIMITER ;


CALL LoginCount('Alice','alice123', @userCount);
SELECT @userCount;


namespace Week7Lecture2;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class Program
{
    static void Main(string[] args)
    {
        string connStr = "server=DB_IP_Address;user=YOUR_USERNAME;database=YOUR_DATABASE;port=8080;password=YOUR_PASSWORD";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

             // input information for new record
            Console.WriteLine("Enter Username:");
            string input_username = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string input_password = Console.ReadLine();

            string procedure = "LoginCount";
            MySqlCommand cmd = new MySqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@inputUsername", input_username);
            cmd.Parameters.AddWithValue("@inputPassword", input_password);
            
            // set the out parameter @userCount in procedure as output parameter
            cmd.Parameters.Add("@userCount", MySqlDbType.Int32).Direction =  ParameterDirection.Output;

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
            // convert returned value to int datatype
            int returnCount = (int) cmd.Parameters["@userCount"].Value;
            // if return value is 1, it means can find the user, else can not find the user
            if(returnCount ==1){
                Console.WriteLine("Login Successfully!");
            }
            else{
                Console.WriteLine("Cannot find user");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        conn.Close();
        Console.WriteLine("Done.");
    }
}
