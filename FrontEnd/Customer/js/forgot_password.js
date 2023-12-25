function clearFormErrors() {
    const passwordError = document.getElementById('passwordError');
    passwordError.textContent = '';
  }
  
  function handleResetPassword() {
    const newPassword = document.getElementById('newPassword').value.trim();
    const reenterPassword = document.getElementById('reenterPassword').value.trim();
    const passwordError = document.getElementById('passwordError');
  
    clearFormErrors();
  
    if (newPassword === '' || newPassword.length < 8) {
      passwordError.textContent = 'New password must be at least 8 characters';
      return;
    }
  
    if (newPassword !== reenterPassword) {
      passwordError.textContent = 'Passwords do not match';
      return;
    }
  
    console.log('Password reset successful');
    redirectToLoginPage();
  }
  
  function redirectToLoginPage() {
    window.location.href = '/index.html';
  }
  