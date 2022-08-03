/* Insurance and user areas and loader */ 
const editProfileArea = document.getElementById('editProfileArea') 
const insuranceArea =  document.getElementById('insuranceArea')
const loader = document.getElementById('loader')

/* editProfileArea form elements */
const picName = document.getElementById('picName')
const picEmail = document.getElementById('picEmail')
const picSrc = document.getElementById('picSrc')
const tbName = document.getElementById('tbName')
const tbSurname = document.getElementById('tbSurname')
const tbPid = document.getElementById('tbPid')
const tbBirthDate = document.getElementById('tbBirthDate')
const tbBirthnumber = document.getElementById('tbBirthnumber')
const tbEmail = document.getElementById('tbEmail')
const tbMobile = document.getElementById('tbMobile')
const tbAddress1 = document.getElementById('tbAddress1')
const tbAddress2 = document.getElementById('tbAddress2')
const tbCountry = document.getElementById('tbCountry')
const tbRegion = document.getElementById('tbRegion')

/* Insurance profile area */
const picNameIns = document.getElementById('picNameIns')
const picEmailIns = document.getElementById('picEmailIns')
const picSrcIns = document.getElementById('picSrcIns')
const carType = document.getElementById('carType') 
const carBrand = document.getElementById('carBrand')
const lastService = document.getElementById('lastService')
const yearProd = document.getElementById('yearOfProduction')
const note = document.getElementById('informationNote')
const crashed = document.getElementById('carshed')
const firstOwner = document.getElementById('firstOwner')
const yearContrib = document.getElementById('yearlyContribution')
const expDate = document.getElementById('expirationDate')

/* Select DOM elements to work with */ 
const signInButton = document.getElementById('signIn')
const signOutButton = document.getElementById('signOut')
const titleDiv = document.getElementById('title-div')
const welcomeDiv = document.getElementById('welcome-div')
const tableDiv = document.getElementById('table-div')
const tableBody = document.getElementById('table-body-div')
const editProfileButton = document.getElementById('editProfileButton')
const insuranceButton = document.getElementById('InsuranceButton')
const response = document.getElementById("response")
const label = document.getElementById('label')

function welcomeUser(username) {
    welcomeDiv.innerHTML = `Welcome ${username}!`

    label.classList.add('d-none')
    signInButton.classList.add('d-none')
    titleDiv.classList.add('d-none')
    //editProfileArea.classList.add('d-none') //Rost hide element

    signOutButton.classList.remove('d-none')
    insuranceButton.classList.remove('d-none')
    editProfileButton.classList.remove('d-none')
    welcomeDiv.classList.remove('d-none')
    //callApiButton.classList.remove('d-none');
}

function logMessage(s) {
    response.appendChild(document.createTextNode('\n' + s + '\n'))
}