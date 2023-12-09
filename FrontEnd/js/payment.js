let paymentData = {
    fullName: '',
    customerId: '',
    serviceNumber: '',
    recipientAccountNumber: '',
    amount: '',
  };
  
  function handleInputChange(e) {
    const { name, value } = e.target;
    paymentData = { ...paymentData, [name]: value };
  }
  
  function handleServiceButtonClick(recipientAccountNumber) {
    paymentData = { ...paymentData, recipientAccountNumber };
    document.getElementById('recipientAccountNumber').value = recipientAccountNumber;
  }
  
  function handleSubmit(event) {
    event.preventDefault();
    const fullName = document.getElementById('fullName').value.trim();
    const customerId = document.getElementById('customerId').value.trim();
    const serviceNumber = document.getElementById('serviceNumber').value.trim();
    const recipientAccountNumber = document.getElementById('recipientAccountNumber').value.trim();
    const amount = document.getElementById('amount').value.trim();
  
    if (!fullName || !customerId || !serviceNumber || !recipientAccountNumber || !amount) {
      alert('All fields are required.');
      return;
    }
  
    console.log('Payment submitted:', paymentData);
    
  
    
    document.getElementById('fullName').value = '';
    document.getElementById('customerId').value = '';
    document.getElementById('serviceNumber').value = '';
    document.getElementById('recipientAccountNumber').value = '';
    document.getElementById('amount').value = '';
  }
  
  
  