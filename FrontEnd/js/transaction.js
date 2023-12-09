
document.addEventListener('DOMContentLoaded', function () {
    const transactions = [
      {
        date: '2023-03-01',
        transactionType: 'Deposit',
        accountNumber: '123456789',
        amount: 1000,
        receiverAccountNumber: null,
      },
      {
        date: '2023-03-05',
        transactionType: 'Withdrawal',
        accountNumber: '123456789',
        amount: 500,
        receiverAccountNumber: null,
      },
      {
        date: '2023-03-10',
        transactionType: 'Transfer',
        accountNumber: '123456789',
        amount: 300,
        receiverAccountNumber: '987654321',
      },
      
    ];
  
    const tableBody = document.getElementById('transactionTableBody');
  
    transactions.forEach((transaction, index) => {
      const row = document.createElement('tr');
  
      const dateCell = document.createElement('td');
      dateCell.textContent = transaction.date;
      row.appendChild(dateCell);
  
      const typeCell = document.createElement('td');
      typeCell.textContent = transaction.transactionType;
      row.appendChild(typeCell);
  
      const accountNumberCell = document.createElement('td');
      accountNumberCell.textContent = transaction.accountNumber;
      row.appendChild(accountNumberCell);
  
      const amountCell = document.createElement('td');
      amountCell.textContent = transaction.amount;
      row.appendChild(amountCell);
  
      const receiverAccountCell = document.createElement('td');
      receiverAccountCell.textContent = transaction.receiverAccountNumber || 'N/A';
      row.appendChild(receiverAccountCell);
  
      tableBody.appendChild(row);
    });
  });
  