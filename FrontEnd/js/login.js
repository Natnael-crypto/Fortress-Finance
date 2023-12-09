let isLoginForm = true;

function clearFormErrors() {
  const emailError = document.getElementById('emailError');
  const passwordError = document.getElementById('passwordError');

  emailError.textContent = '';
  passwordError.textContent = '';
}

function validateLoginForm() {
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value.trim();
  
    const emailError = document.getElementById('emailError');
    const passwordError = document.getElementById('passwordError');
  
    emailError.textContent = '';
    passwordError.textContent = '';
  
    let hasErrors = false;
  
    if (email === '') {
      emailError.textContent = 'Email is required';
      hasErrors = true;
    }
  
    if (password === '' || password.length < 8) {
      passwordError.textContent = 'Password must be at least 8 characters';
      hasErrors = true;
    }
  
    return hasErrors;
  }
  
  function handleLoginSubmit() {
    if (!validateLoginForm()) {
      console.log('Login successful');
      window.location.href = '../pages/home.html'; 
    }
  }
  
function forgotPassword() {
  console.log('Forgot Password clicked');
 
}
document.addEventListener('DOMContentLoaded', function () {
  const toggleButton = document.getElementById('toggleButton');
  if (toggleButton) {
    toggleButton.style.display = 'none';
  }
});

function showLoginForm(isCustomer) {
  const userTypeSection = document.getElementById('userTypeSection');
  const loginFormSection = document.getElementById('loginFormSection');
  const loginInputs = document.getElementById('loginInputs');
  const loginButtonSection = document.getElementById('loginButtonSection');
  const toggleButton = document.getElementById('toggleButton');

  if (isCustomer) {
    userTypeSection.style.display = 'none';
    toggleButton.style.display = 'flex';

    loginFormSection.style.display = 'flex';
    loginInputs.style.display = 'flex';
    loginButtonSection.style.display = 'flex';
  } else {
    userTypeSection.style.display = 'none';
    toggleButton.style.display = 'none';

    loginFormSection.style.display = 'flex';
    loginInputs.style.display = 'flex';
    loginButtonSection.style.display = 'none';
  }
}


