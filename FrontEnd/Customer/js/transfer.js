document.addEventListener('DOMContentLoaded', function () {
  const form = document.getElementById('transferForm');

  form.addEventListener('submit', function (event) {
      event.preventDefault();

      if (!form.checkValidity()) {
          event.preventDefault();
          form.classList.add('was-validated');
      } else {
          handleTransfer(event);
      }
  });
});

function handleTransfer(event) {
  const form = document.getElementById('transferForm');
  const isValid = form.checkValidity();

  if (isValid) {
      setTimeout(() => {
          const successMessage = document.getElementById('successMessage');
          successMessage.style.display = 'block';
          form.reset();
          setTimeout(() => {
              successMessage.style.display = 'none';
          }, 3000);
      }, 1000);
  } else {
      form.reportValidity();
  }
}
