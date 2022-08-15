function readUserInfo(endpoint, token) 
{   
    const headers = new Headers();
    const bearer = `Bearer ${token}`;
  
    headers.append("Authorization", bearer);
  
    const options = {
        method: "GET",
        headers: headers
      };
  
    
    fetch(endpoint, options)
      .then(response => response.json()) //response.json() TODO fix isolated project changes 
      .then(response => {
        fillUserInfo(response)
        hideLoaderShowUser()
        checkRegistration()
      }).catch(error => {
        console.error(error);
      });
  }

  function fillUserInfo(response)
  {
    picName.textContent = response?.name
    picEmail.textContent = response?.email
    picSrc.src = response?.profileImage //"https://yt3.ggpht.com/a/AATXAJxYRjCkDJNMlaBlFvJkImsx4WfyUDowJ2O64Q=s900-c-k-c0xffffffff-no-rj-mo"
    tbName.value = response?.name
    tbSurname.value = response?.surname;
    tbPid.value = response?.pid
    tbBirthDate.value = response?.birthdate
    tbBirthnumber.value = response?.birthNumber
    tbEmail.value = response?.email
    tbMobile.value = response?.mobileNumber
    tbAddress1.value = response?.address1
    tbAddress2.value = response?.address2
    tbCountry.value = "Czech Republic"
    tbRegion.value = "Prague"
  }

  function writeUserInfo(endpoint, token) 
  {
    
    const headers = new Headers()
    const bearer = `Bearer ${token}`;
  
    headers.append("Authorization", bearer)

    const body = { //ReqData class on backend
        pid: document.getElementById('tbPid').value,
        name: document.getElementById('tbName').value,
        surname: document.getElementById('tbSurname').value,   
        birthdate: document.getElementById('tbBirthDate').value,
        birthNumber: document.getElementById('tbBirthnumber').value,
        mobileNumber: document.getElementById('tbMobile').value,
        email: document.getElementById('tbEmail').value,
        address1: document.getElementById('tbAddress1').value,
        address2: document.getElementById('tbAddress2').value,
        profileImage: document.getElementById('picSrc').src,
        // carInsurance: //RequestedData record on Backend 
        // {
        //     expDate: document.getElementById('test').src
        // }
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

async function editProfile() 
{
    hideUserShowLoader()

    passTokenToApi("GET") 
}

function saveProfile()
{
    editProfileArea.classList.add('d-none')
    passTokenToApi("PUT") 
}

function hideLoaderShowUser()
{
    editProfileArea.classList.remove('d-none') 
    insuranceArea.classList.add('d-none') 
    loader.classList.add('d-none')
    document.getElementById('profileText').textContent = "Profile Settings"
}

function hideUserShowLoader()
{
    editProfileArea.classList.add('d-none') 
    insuranceArea.classList.add('d-none') 
    loader.classList.remove('d-none')
}