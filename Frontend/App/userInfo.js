function readUserInfo(endpoint, token) {
    
    const headers = new Headers();
    const bearer = `Bearer ${token}`;
  
    headers.append("Authorization", bearer);
  
    const options = {
        method: "GET",
        headers: headers
      };
  
    //logMessage('Getting user settings...');
    // document append div class loader
    
    fetch(endpoint, options)
      .then(response => response.json()) //response.json()
      .then(response => {
        //document remove div loader
        hideLoaderShowUser();
        fillUserInfo(response); //fill form
      }).catch(error => {
        console.error(error);
      });
  }

  function fillUserInfo(response)
  {
    //const editProfileArea = document.getElementById('editProfileArea');//Rost edit profile
    const picName = document.getElementById('picName');
    const picEmail = document.getElementById('picEmail');
    const picSrc = document.getElementById('picSrc');
    const tbName = document.getElementById('tbName');
    const tbSurname = document.getElementById('tbSurname');
    const tbPid = document.getElementById('tbPid');
    const tbBirthDate = document.getElementById('tbBirthDate');
    const tbBirthnumber = document.getElementById('tbBirthnumber');
    const tbEmail = document.getElementById('tbEmail');
    const tbMobile = document.getElementById('tbMobile');
    const tbAddress1 = document.getElementById('tbAddress1');
    const tbAddress2 = document.getElementById('tbAddress2');
    const tbCountry = document.getElementById('tbCountry');
    const tbRegion = document.getElementById('tbRegion');

    // const editProfileArea = document.getElementById('editProfileArea')

    // let div = document.createElement("div");
    // let span = document.createElement("span")

    // div.classList.add("spinner-border");
    // div.classList.add("text-secondary");
    // span.classList.add("visually-hidden")

    // div.appendChild(span)
    // editProfileArea.appendChild(div);

    picName.textContent = response.name;
    picEmail.textContent = response.email;
    picSrc.src = response.profileImage //"https://yt3.ggpht.com/a/AATXAJxYRjCkDJNMlaBlFvJkImsx4WfyUDowJ2O64Q=s900-c-k-c0xffffffff-no-rj-mo"
    tbName.value = response.name;
    tbSurname.value = response.surname;
    tbPid.value = response.pid;
    tbBirthDate.value = response.birthdate;
    tbBirthnumber.value = response.birthNumber;
    tbEmail.value = response.email;
    tbMobile.value = response.mobileNumber;
    tbAddress1.value = response.address1;
    tbAddress2.value = response.address2;
    tbCountry.value = "Czech Republic"
    tbRegion.value = "Prague"
  }

  function writeUserInfo(endpoint, token) {
    
    const headers = new Headers();
    const bearer = `Bearer ${token}`;
  
    headers.append("Authorization", bearer);

    const body = { //ReqData class on backend
        pid: document.getElementById('tbPid').value,
        name: document.getElementById('tbName').value,
        surname: document.getElementById('tbSurname').value,   
        //birthdate: document.getElementById('tbBirthDate').value,
        //birthnumber: document.getElementById('tbBirthnumber').value,
        mobileNumber: document.getElementById('tbMobile').value,
        email: document.getElementById('tbEmail').value,
        address1: document.getElementById('tbAddress1').value,
        address2: document.getElementById('tbAddress2').value,
        profileImage: document.getElementById('picSrc').src 
    };
  
    const options = {
        method: "PUT",
        headers: headers,
        body: JSON.stringify(body)
      };
  
    //logMessage('Saving user data...');
    
    fetch(endpoint, options)
        .catch(error => {
        console.error(error);
      });
  }