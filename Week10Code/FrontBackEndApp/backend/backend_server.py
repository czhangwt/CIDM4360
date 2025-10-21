"""
A simple backend server using FastAPI to serve API and interact with a SQLite database.
To run this server locally, use the command in terminal: fastapi dev backend_server.py --port 8000
"""

from fastapi import FastAPI
from fastapi import Request
import sqlite3
from fastapi.middleware.cors import CORSMiddleware

app = FastAPI()


# CORS (Cross-Origin Resource Sharing) allows restricted resources on a web page to be requested from another domain
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # Allows all origins
    allow_credentials=True,
    allow_methods=["*"],  # Allows all methods
    allow_headers=["*"],  # Allows all headers
)


# new route to get student grades from backend database and render the result in grade_result.html
@app.get("/get_grades")
def get_grades(request: Request):
    # get student_id from query parameters
    student_id = request.query_params.get("student_id", None)
    # fetch student grades from backend database
    grades = get_student_grades(student_id)
    # convert query result to a JSON-serializable format
    # (`stu_id` INTEGER, `stu_name` TEXT, `course_name` TEXT, `course_grade` TEXT)
    grades_json = [{"Student ID": row[0], "Student Name": row[1], "Course Name": row[2], "Course Grade": row[3]} for row in grades]
    # return the JSON response grades_json
    return  grades_json


# retreive data from backend sqlite database
def get_student_grades(student_id: int):
    conn = sqlite3.connect('database.sqlite')
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM Grade WHERE stu_id = ?", (student_id,))
    rows = cursor.fetchall()
    conn.close()
    return rows