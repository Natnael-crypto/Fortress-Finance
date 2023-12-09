function handleTransfer(event) {
    event.preventDefault();
  
    
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