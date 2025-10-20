# create a simple static file server using FastAPI to return static files from the 'frontend' directory
"""A simple frontend server using FastAPI to serve static files for the frontend application.
To run this server locally, use the command in terminal: fastapi dev frontend_server.py --port 80
"""
from fastapi import FastAPI
from fastapi.staticfiles import StaticFiles
from fastapi import Request
from fastapi.responses import HTMLResponse

app = FastAPI()

app.mount("/", StaticFiles(directory="static", html=True), name="static")

@app.get("/")
def read_index(request: Request):
    return HTMLResponse(open("static/index.html").read())
