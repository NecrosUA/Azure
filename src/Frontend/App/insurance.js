// const editProfileArea = document.getElementById('editProfileArea')
// const insuranceArea =  document.getElementById('insuranceArea')

function addInsurance()
{
    passTokenToApi("INSURANCEPUT")
    insuranceArea.classList.add('d-none')
}

function readInsuranceInfo()
{
  loader.classList.remove('d-none')
  editProfileArea.classList.add('d-none')
  passTokenToApi("INSURANCEGET")
}


function fillInsurance(response)
{
    //console.log("resposne : " + JSON.stringify(response))

    picNameIns.textContent = response.name 
    picEmailIns.textContent = response.email
    picSrcIns.src = response.profileImage 
    tbPid.value = response.pid
    
    carType.value = response.carInsurance?.carType
    carBrand.value = response.carInsurance?.carBrand
    lastService.value = response.carInsurance?.lastService
    yearProd.value = response.carInsurance?.year
    note.value = response.carInsurance?.informationNote
    crashed.checked = response.carInsurance?.crashed
    firstOwner.checked = response.carInsurance?.firstOwner
    yearContrib.value = response.carInsurance?.yearlyContribution
    expDate.value = response.carInsurance?.expDate
}

function readInsurance(endpoint, token) //TODO finish reading insurance
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
        //console.log("fetched response: " + response) //response is ok!
        fillInsurance(response)
        hideLoaderShowInsurance()
      }).catch(error => {
        console.error(error);
      });
}

function writeInsurance(endpoint, token) //TODO finish saving insurance
{
    const today = new Date()
    const day = today.getDate()
    const month = (today.getMonth()+1)
    const year = today.getFullYear()
    const nextYearExp = (year+1)+'-'+(month<=9?"0":"") + month + '-' +(day<=9?"0":"") + day
    const contribution = yearProd.value * 2.33 // =)
    const headers = new Headers()
    const bearer = `Bearer ${token}`
  
    headers.append("Authorization", bearer)

    const body = 
    { 
        pid: document.getElementById('tbPid').value,
        carInsurance: //RequestedData record on Backend 
        {
            expDate: nextYearExp, 
            informationNote: note.value,
            yearlyContribution: contribution,
            carType: carType.value,
            carBrand: carBrand.value,
            crashed: crashed.checked,
            firstOwner: firstOwner.checked,
            lastService: lastService.value,
            year: yearProd.value
        }
    };

    //console.log(body)
  
    const options = 
    {
      method: "PUT",
      headers: headers,
      body: JSON.stringify(body)
    };
  
    //logMessage('Saving user data...');
    
    fetch(endpoint, options)
        .catch(error => 
      {
        console.error(error);
      });
}

function hideLoaderShowInsurance()
{
    editProfileArea.classList.add('d-none') 
    insuranceArea.classList.remove('d-none') 
    loader.classList.add('d-none')
}