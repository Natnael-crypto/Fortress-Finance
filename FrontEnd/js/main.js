document.addEventListener('DOMContentLoaded', function () {
  const user = JSON.parse(sessionStorage.getItem('user')) || {};
  const accountNumberSpan = document.getElementById('accountNumber');
  const userName = document.querySelector('.user-details h2');

  if (user.fullName) {
    userName.textContent = user.fullName;
  }

  if (user.accountNumber) {
    accountNumberSpan.textContent = user.accountNumber;
    accountNumberSpan.parentElement.style.display = 'block';
  }

});
document.getElementById('edit').addEventListener('click', function() {
  window.location.href = '../pages/editprofile.html';
});
function redirectToEditProfilePage() {
  window.location.href = '../pages/edit_profile.html';
}
document.addEventListener('DOMContentLoaded', function () {
  const ctx = document.getElementById('budgetChart').getContext('2d');
  const data = {
    labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'],
    datasets: [{
      label: 'Income',
      data: [200, 150, 300, 100, 250],
      backgroundColor: 'rgba(75, 192, 192, 0.2)',
      borderColor: 'rgba(75, 192, 192, 1)',
      borderWidth: 1
    }, {
      label: 'Expense',
      data: [50, 100, 75, 200, 30],
      backgroundColor: 'rgba(255, 99, 132, 0.2)',
      borderColor: 'rgba(255, 99, 132, 1)',
      borderWidth: 1
    }]
  };

  const config = {
    type: 'bar',
    data: data,
    options: {
      scales: {
        y: {
          beginAtZero: true
        }
      }
    }
  };

  const budgetChart = new Chart(ctx, config);
});



