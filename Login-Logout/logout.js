document.addEventListener('DOMContentLoaded', function () {
    const logoutButton = document.getElementById('logoutButton');

    logoutButton.addEventListener('click', function () {
        // Remove the JWT token from local storage
        localStorage.removeItem('token');
        window.location.href = '/login'; // Redirect to the login page
    });
});
