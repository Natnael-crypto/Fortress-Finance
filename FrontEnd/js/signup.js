let isLoginForm = false; 

function clearFormErrors() {
  const fullNameError = document.getElementById('fullNameError');
  const emailError = document.getElementById('emailError');
  const passwordError = document.getElementById('passwordError');

  fullNameError.textContent = '';
  emailError.textContent = '';
  passwordError.textContent = '';
}

function validateSignupForm() {
  const fullName = document.getElementById('fullName').value.trim();
  const email = document.getElementById('email').value.trim();
  const password = document.getElementById('password').value.trim();

  const fullNameError = document.getElementById('fullNameError');
  const emailError = document.getElementById('emailError');
  const passwordError = document.getElementById('passwordError');

  fullNameError.textContent = '';
  emailError.textContent = '';
  passwordError.textContent = '';

  let hasErrors = false;

  if (!fullName) {
    fullNameError.textContent = 'Full Name is required';
    hasErrors = true;
  }

  if (!email) {
    emailError.textContent = 'Email is required';
    hasErrors = true;
  } else if (!email.includes('@')) {
    emailError.textContent = 'Please enter a valid email address';
    hasErrors = true;
  }

  if (!password || password.length < 8) {
    passwordError.textContent = 'Password must be at least 8 characters';
    hasErrors = true;
  }

  return hasErrors;
}

function handleSignupSubmit() {
  if (!validateSignupForm()) {
    console.log('Sign up successful');
    window.location.href = '../index.html'; 
  }
}

function redirectToLoginPage() {
  window.location.href = '../pages/login.html';
}



