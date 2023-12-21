function submitForm() {
  const firstName = document.getElementById('firstName').value;
  const middleName = document.getElementById('middleName').value;
  const lastName = document.getElementById('lastName').value;
  const country = document.getElementById('country').value;
  const city = document.getElementById('city').value;

  
  if (!firstName || !lastName || !country || !city) {
    alert('All fields are required.');
    return;
  }

  
  const accountNumber = generateAccountNumber();
  alert(`Account created!\nAccount Number: ${accountNumber}\nFull Name: ${firstName} ${middleName || ''} ${lastName}`);

  
  const user = {
    fullName: `${firstName} ${middleName || ''} ${lastName}`,
    accountNumber: accountNumber
  };
  sessionStorage.setItem('user', JSON.stringify(user));

  
  window.location.href = '../pages/home.html';
}

function generateAccountNumber() {
  return Math.floor(10000000 + Math.random() * 90000000);
}



function submitEditForm() {
  const storedUser = sessionStorage.getItem('user');
  if (!storedUser) {
    alert('User account not found. Please create an account first.');
    return;
  }

  const user = JSON.parse(storedUser);

  const firstName = document.getElementById('firstName').value;
  const middleName = document.getElementById('middleName').value;
  const lastName = document.getElementById('lastName').value;
  const country = document.getElementById('country').value;
  const city = document.getElementById('city').value;

  if (!firstName || !lastName || !country || !city) {
    alert('All fields are required.');
    return;
  }

  user.fullName = `${firstName} ${middleName || ''} ${lastName}`;
  sessionStorage.setItem('user', JSON.stringify(user));

  window.location.href = '../pages/home.html';
}
/*change picture*/
function changePicture() {
  const input = document.createElement('input');
  input.type = 'file';
  input.accept = 'image/*';

  input.onchange = (event) => {
    const file = event.target.files[0];

    if (file) {
      const profilePic = document.getElementById('profile-pic');
      const reader = new FileReader();
      
      reader.onload = (e) => {
        profilePic.src = e.target.result;
      };

      reader.readAsDataURL(file);
    }
  };

  input.click();
}


