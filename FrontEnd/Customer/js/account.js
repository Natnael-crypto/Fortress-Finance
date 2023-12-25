function changePicture() {
  const input = document.createElement('input');
  input.type = 'file';
  input.accept = 'image/*';

  input.onchange = (event) => {
    const file = event.target.files[0];

    if (file) {
      const profilePic = document.querySelector('.profile-pic'); // Change here
      const reader = new FileReader();

      reader.onload = (e) => {
        profilePic.src = e.target.result;
      };

      reader.readAsDataURL(file);
    }
  };

  input.click();
}



