"""
A simple backend server using FastAPI to serve HTML templates and interact with a SQLite database.
To run this server locally, use the command in terminal: fastapi dev server.py --port 8000
"""

from fastapi import FastAPI, Request
from fastapi.templating import Jinja2Templates
from fastapi.staticfiles import StaticFiles
import sqlite3



app = FastAPI()

# Mount static files (CSS, JS, images) - important for template assets
app.mount("/static", StaticFiles(directory="/"), name="static")

# Templates directory - adjusted path for Docker container structure
templates = Jinja2Templates(directory="templates")


# Default route to render the index.html template
@app.get("/")
def read_index(request: Request):
    return templates.TemplateResponse("index.html", {"request": request})


# Route to get student grades from backend database and render the result in grade_result.html
@app.get("/get_grades")
def get_grades(request: Request):
    # Get student_id from query parameters
    student_id = request.query_params.get("student_id", None)
    
    if not student_id:
        return templates.TemplateResponse(
            "grade_result.html",
            {
                "request": request,
                "error": "Please provide a student ID",
                "grades": []
            }
        )
    
    # Fetch student grades from backend database
    grades = get_student_grades(student_id)

    return templates.TemplateResponse(
        "grade_result.html",  # template file name
        {
            "request": request,  # request object is required by Jinja2
            "grades": grades,
            "student_id": student_id
        },  # context to pass to the template
    )


# Retrieve data from backend sqlite database
def get_student_grades(student_id: int):
    try:
        # Use absolute path to ensure we find the database
        db_path = 'database.sqlite'
        conn = sqlite3.connect(db_path)
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM Grade WHERE stu_id = ?", (student_id,))
        rows = cursor.fetchall()
        conn.close()
        return rows
    except sqlite3.Error as e:
        print(f"Database error: {e}")
        return []
    except Exception as e:
        print(f"Unexpected error: {e}")
        return []