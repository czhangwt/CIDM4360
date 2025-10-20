// Update all API calls to use the correct path
const API_BASE_URL = 'http://127.0.0.1:8000';

async function fetchGrades() {
    // get the student ID from the input field
    const student_id = document.getElementById("student_id").value;
    
    // Validate input
    if (!student_id) {
        alert("Please enter a Student ID");
        return;
    }
    
    try {
        // make a GET request to the backend API
        const response = await fetch(`${API_BASE_URL}/get_grades?student_id=${student_id}`);
        
        // check if the response is successful
        if (response.ok) {
            const grades = await response.json();
            // display the grades on the web page
            displayGrades(grades);
        } else {
            console.error("Failed to fetch grades. Status:", response.status);
            console.error(response.statusText);
            alert("Failed to fetch grades. Please try again.");
        }
    } catch (error) {
        console.error("Error fetching grades:", error);
        alert("Error connecting to server. Please check your connection.");
    }
}

// write a function to display the grades on the web page
function displayGrades(grades) {
    const resultDiv = document.getElementById("result");
    resultDiv.innerHTML = ""; // clear previous results
    
    if (grades.length > 0) {
        const table = document.createElement("table");
        table.style.borderCollapse = "collapse";
        table.style.width = "100%";
        table.style.marginTop = "20px";
        table.style.border = "1px solid #ddd";
        
        // Create table header
        const headerRow = table.insertRow();
        headerRow.style.backgroundColor = "#f2f2f2";
        
        const headers = Object.keys(grades[0]);
        headers.forEach(headerText => {
            const header = document.createElement("th");
            header.textContent = headerText;
            header.style.padding = "12px";
            header.style.textAlign = "left";
            header.style.border = "1px solid #ddd";
            headerRow.appendChild(header);
        });
        
        // Create table rows
        grades.forEach(grade => {
            const row = table.insertRow();
            row.style.border = "1px solid #ddd";
            
            headers.forEach(header => {
                const cell = row.insertCell();
                cell.textContent = grade[header];
                cell.style.padding = "8px";
                cell.style.border = "1px solid #ddd";
            });
        });
        
        resultDiv.appendChild(table);
    } else {
        resultDiv.textContent = "No grades found for this Student ID.";
        resultDiv.style.color = "#666";
        resultDiv.style.textAlign = "center";
        resultDiv.style.padding = "20px";
    }
}

// Add event listener for Enter key in the input field
document.getElementById("student_id").addEventListener("keypress", function(event) {
    if (event.key === "Enter") {
        fetchGrades();
    }
});