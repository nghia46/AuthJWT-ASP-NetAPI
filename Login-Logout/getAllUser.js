document.addEventListener('DOMContentLoaded', function () {
    // Retrieve the JWT token from local storage
    const token = localStorage.getItem('token');

    if (!token) {
        // If the user is not authenticated, display a notification
        alert('You are not logged in. Please log in to access the User List.');
        
        // Redirect to the login page
        window.location.href = '/login.html'; // Replace with the correct URL
        return;
    }

    // Make an authenticated API request to get the list of users
    fetch('https://localhost:7171/api/Auth/getAlluser', {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })
    .then(response => {
        if (response.ok) {
            return response.json();
        } else {
            throw new Error('Failed to fetch user list');
        }
    })
    .then(users => {
        // Display the list of users on the page
        const userListElement = document.getElementById('userList');
        if (users.length === 0) {
            userListElement.textContent = 'No users found.';
        } else {
            const userListHTML = users.map(user => `<div>${user.name} - ${user.role}</div>`).join('');
            userListElement.innerHTML = userListHTML;
        }
    })
    .catch(error => {
        console.error(error);
    });

    // Add a click event listener to the logout button
    const logoutButton = document.getElementById('logoutButton');
    logoutButton.addEventListener('click', function () {
        // Remove the JWT token from local storage
        localStorage.removeItem('token');
        // Redirect to the login page
        window.location.href = '/login.html'; // Replace with the correct URL
    });
});
