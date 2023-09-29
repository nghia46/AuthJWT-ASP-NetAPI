document.addEventListener('DOMContentLoaded', function () {
    const loginForm = document.getElementById('loginForm');
    const message = document.getElementById('message');

    loginForm.addEventListener('submit', function (e) {
        e.preventDefault();

        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        // Make an API request to authenticate the user
        fetch('https://localhost:7171/api/Auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ name: username, password: password })
        })
        .then(response => response.json())
        .then(data => {
            // Check if the response contains a token
            if (data && data.token) {
                // Store the JWT token in local storage
                localStorage.setItem('token', data.token);
                message.textContent = 'Login successful!';
                
                // Redirect to the UserList page
                window.location.href = '/UserList.html'; // Replace with the correct URL
                
            } else {
                throw new Error('Login failed');
            }
        })
        .catch(error => {
            message.textContent = 'Login failed. Please check your credentials.';
            console.error(error);
        });
    });
});
