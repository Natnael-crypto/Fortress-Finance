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
  
    if (!email) {
      emailError.textContent = 'Email is required';
      hasErrors = true;
    } else if (!email.includes('@')) {
      emailError.textContent = 'Please enter a valid email address';
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




