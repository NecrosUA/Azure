// const editProfileArea = document.getElementById('editProfileArea')
// const insuranceArea =  document.getElementById('insuranceArea')

function addInsurance()
{
    insuranceArea.classList.remove('d-none')
    editProfileArea.classList.add('d-none') 
    passTokenToApi("INSURANCEGET")
}

function fillInsurance(response)
{
    picNameIns.textContent = response.Name //TODO add name of properties on backend
    picEmailIns.textContent = response.Email
    picSrcIns.src = response.ProfileImage 
    tbPid.value = response.Pid

    carType.value = response?.CarInsurance.ExpDate
    carBrand.value = response?.CarInsurance.CarBrand
    lastService.value = response?.CarInsurance.LastService
    yearProd.value = response?.CarInsurance.Year
    note.value = response?.CarInsurance.Note
    crashed.value = response?.CarInsurance.Crashed
    firstOwner.value = response?.CarInsurance.FirstOwner
    yearContrib.value = response?.CarInsurance.YearlyContribution
    expDate.value = response?.CarInsurance.ExpDate
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

        fillInsurance(response)
        hideLoaderShowInsurance()
      }).catch(error => {
        console.error(error);
      });
}

function writeInsurance(endpoint, token) //TODO finish saving insurance
{
    const nextYearExp = (today.getMonth()+1)+'-'+today.getDate()+'-'+(today.getFullYear()+1)
    const contribution = yearProd.value * 1.33 // =)
    const headers = new Headers()
    const bearer = `Bearer ${token}`
  
    headers.append("Authorization", bearer)

    const body = 
    { 
        pid: document.getElementById('tbPid').value,
        carInsurance: //RequestedData record on Backend 
        {
            expDate: nextYearExp, //TODO change backend propety names
            informationNote: note.value,
            yearlyContribution: contribution,
            carType: carType.value,
            carBrand: carBrand.value,
            crashed: crashed.value,
            firstOwner: firstOwner.value,
            lastService: lastService.value,
            year: yearProd.value
        }
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

function hideLoaderShowInsurance()
{
    editProfileArea.classList.add('d-none') 
    insuranceArea.classList.remove('d-none') 
    loader.classList.add('d-none')
}