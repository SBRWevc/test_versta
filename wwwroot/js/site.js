function togglePasswordVisibility() {
    const passwordInput = document.getElementById('passwordInput');
    const passwordIcon = document.getElementById('passwordIcon');
    const isPasswordVisible = passwordInput.type === 'text';
    passwordInput.type = isPasswordVisible ? 'password' : 'text';
    passwordIcon.classList.toggle('bi-eye', !isPasswordVisible);
    passwordIcon.classList.toggle('bi-eye-slash', isPasswordVisible);
}

function toggleConfirmPasswordVisibility() {
    const passwordInput = document.getElementById('confirmPasswordInput');
    const passwordIcon = document.getElementById('confirmPasswordIcon');
    const isPasswordVisible = passwordInput.type === 'text';
    passwordInput.type = isPasswordVisible ? 'password' : 'text';
    passwordIcon.classList.toggle('bi-eye', !isPasswordVisible);
    passwordIcon.classList.toggle('bi-eye-slash', isPasswordVisible);
}

