function callApi(endpoint, token) {
    
    const headers = new Headers();
    const bearer = `Bearer ${token}`;
  
    headers.append("Authorization", bearer);
  
    const options = {
        method: "GET",
        headers: headers
      };
  
    logMessage('Calling web API...');
    
    fetch(endpoint, options)
      .then(response => response.text()) //response.json()
      .then(response => {

        if (response) {
          logMessage('Web API responded: ' + response); 
        }
        
        // var test = JSON.parse(response);//Parsing to JSON
        // alert(test[0].manufacturer);//Now I can call like regular array by index

        return response; //JSON.stringify(response)
      }).catch(error => {
        console.error(error);
      });
  }