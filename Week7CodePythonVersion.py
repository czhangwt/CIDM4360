# Python Example 1: Connect Remote Database
# Find all users whose roles are Engineering

import mysql.connector  # Import the MySQL connector library

def main():
    # Define the connection string
    conn_str = {
        'host': '34.69.59.37',
        'user': 'YOUR_USERNAME',  # Replace with your MySQL username
        'database': 'YOUR_DATABASE',  # Replace with your database name
        'port': 8080,
        'password': 'YOUR_PASSWORD'  # Replace with your MySQL password
    }
    try:
        print("Connecting to MySQL...")  # Establish a connection to the MySQL database
        conn = mysql.connector.connect(**conn_str)  # Use dictionary unpacking
        cursor = conn.cursor()  # Create a cursor object to execute SQL queries
        # Define the SQL query
        sql = "SELECT username, password, role FROM User WHERE role='Engineering'"
        cursor.execute(sql)  # Execute the SQL query
        results = cursor.fetchall()  # Fetch all the results from the query
        # Iterate over the results and print the data
        for row in results:
            print(f"Username: {row[0]} -- Password: {row[1]} -- Role: {row[2]}")
    except mysql.connector.Error as err:
        # Handle any exceptions that occur during the process
        print(f"Error: {err}")
    finally:
        # Ensure the connection is closed, even if an error occurred
        if conn.is_connected():
            cursor.close()  # Close the cursor
            conn.close()  # Close the connection
            print("MySQL connection is closed")
    print("Done.")

# Execute the main function if the script is run directly
if __name__ == "__main__":
    main()



# Python Example 2: Working SQL with Parameter
# Find all users match input role
import mysql.connector  # Import the MySQL connector library

def main():
    # Define the connection string
    conn_str = {
        'host': '34.69.59.37',
        'user': 'YOUR_USERNAME',  # Replace with your MySQL username
        'database': 'YOUR_DATABASE',  # Replace with your database name
        'port': 8080,
        'password': 'YOUR_PASSWORD'  # Replace with your MySQL password
    }
    try:
        print("Connecting to MySQL...")  # Establish a connection to the MySQL database
        conn = mysql.connector.connect(**conn_str)  # Use dictionary unpacking
        cursor = conn.cursor()  # Create a cursor object to execute SQL queries
         # SQL query with a placeholder %s
        sql = "SELECT username, role FROM User WHERE role = %s"
        # Get role value from keyboard
        role_input = input("Enter a role e.g.: Engineering, Support, Services, Training: ")
        # Execute the query with the parameter
        cursor.execute(sql, (role_input,))  # Pass the parameter as a tuple
        results = cursor.fetchall()  # Fetch all the results from the query
        # Iterate over the results and print the data
        for row in results:
            print(f"Username: {row[0]} -- Role: {row[1]}")
    except mysql.connector.Error as err:
        # Handle any exceptions that occur during the process
        print(f"Error: {err}")
    finally:
        # Ensure the connection is closed, even if an error occurred
        if conn.is_connected():
            cursor.close()  # Close the cursor
            conn.close()  # Close the connection
            print("MySQL connection is closed")
    print("Done.")

# Execute the main function if the script is run directly
if __name__ == "__main__":
    main()


# Python Example 3: Insert/Update/Delete Records
# Insert a new student record to User table 
import mysql.connector  # Import the MySQL connector library
def main():
    # Define the connection string
    conn_str = {
        'host': '34.69.59.37',
        'user': 'YOUR_USERNAME',  # Replace with your MySQL username
        'database': 'YOUR_DATABASE',  # Replace with your database name
        'port': 8080,
        'password': 'YOUR_PASSWORD'  # Replace with your MySQL password
    }
    try:         # Establish connection
        print("Connecting to MySQL...")
        conn = mysql.connector.connect(**conn_str)
        cursor = conn.cursor()
        # SQL query with placeholders %s
        sql = "INSERT INTO User (username, password, role) VALUES (%s, %s, %s)"
        # Input information for the new record
        username = input("Enter Username: ")
        password = input("Enter Password: ")
        role = input("Enter Role: ")
        # Data to be inserted
        val = (username, password, role)  # Tuple of values
        # Execute the query with parameters
        cursor.execute(sql, val)
        # Commit the changes to the database
        conn.commit()
        print(cursor.rowcount, "record inserted.") # Get affected rows.
        # Close resources
        cursor.close()
    except mysql.connector.Error as err:
        # Handle any exceptions that occur during the process
        print(f"Error: {err}")
    finally:
        # Ensure the connection is closed, even if an error occurred
        if conn.is_connected():
            cursor.close()  # Close the cursor
            conn.close()  # Close the connection
            print("MySQL connection is closed")
    print("Done.")
if __name__ == "__main__":
    main()






