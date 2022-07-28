// Create the main myMSALObj instance
// configuration parameters are located at authConfig.js
const myMSALObj = new msal.PublicClientApplication(msalConfig);

let accountId = "";
let username = "";

function setAccount(account) {
    accountId = account.homeAccountId;
    username = account.username;
    welcomeUser(username);
}

function selectAccount() {
    /**
     * See here for more info on account retrieval: 
     * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-common/docs/Accounts.md
     */

    const currentAccounts = myMSALObj.getAllAccounts();

    if (currentAccounts.length < 1) {
        return;
    } else if (currentAccounts.length > 1) {

        /**
         * Due to the way MSAL caches account objects, the auth response from initiating a user-flow
         * is cached as a new account, which results in more than one account in the cache. Here we make
         * sure we are selecting the account with homeAccountId that contains the sign-up/sign-in user-flow, 
         * as this is the default flow the user initially signed-in with.
         */
        const accounts = currentAccounts.filter(account =>
            account.homeAccountId.toUpperCase().includes(b2cPolicies.names.signUpSignIn.toUpperCase())
            &&
            account.idTokenClaims.iss.toUpperCase().includes(b2cPolicies.authorityDomain.toUpperCase())
            &&
            account.idTokenClaims.aud === msalConfig.auth.clientId 
            );

        if (accounts.length > 1) {
            // localAccountId identifies the entity for which the token asserts information.
            if (accounts.every(account => account.localAccountId === accounts[0].localAccountId)) {
                // All accounts belong to the same user
                setAccount(accounts[0]);
            } else {
                // Multiple users detected. Logout all to be safe.
                signOut();
            };
        } else if (accounts.length === 1) {
            setAccount(accounts[0]);
        }

    } else if (currentAccounts.length === 1) {
        setAccount(currentAccounts[0]);
    }
}

// in case of page refresh
selectAccount();

function handleResponse(response) {
    /**
     * To see the full list of response object properties, visit:
     * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/request-response-object.md#response
     */

    if (response !== null) {
        setAccount(response.account);
    } else {
        selectAccount();
    }
}

function signIn() {

    /**
     * You can pass a custom request object below. This will override the initial configuration. For more information, visit:
     * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/request-response-object.md#request
     */

    myMSALObj.loginPopup(loginRequest)
        .then(handleResponse)
        .catch(error => {
            console.log(error);
        });
}

function signOut() {

    /**
     * You can pass a custom request object below. This will override the initial configuration. For more information, visit:
     * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-browser/docs/request-response-object.md#request
     */

    const logoutRequest = {
        postLogoutRedirectUri: msalConfig.auth.redirectUri,
        mainWindowRedirectUri: msalConfig.auth.redirectUri
    };

    myMSALObj.logoutPopup(logoutRequest);
}

function getTokenPopup(request) {

    /**
    * See here for more information on account retrieval: 
    * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-common/docs/Accounts.md
    */
    request.account = myMSALObj.getAccountByHomeId(accountId);


    /**
     * 
     */
    return myMSALObj.acquireTokenSilent(request)
        .then((response) => {
            // In case the response from B2C server has an empty accessToken field
            // throw an error to initiate token acquisition
            if (!response.accessToken || response.accessToken === "") {
                throw new msal.InteractionRequiredAuthError;
            }
            return response;
        })
        .catch(error => {
            console.log("Silent token acquisition fails. Acquiring token using popup. \n", error);
            if (error instanceof msal.InteractionRequiredAuthError) {
                // fallback to interaction when silent call fails
                return myMSALObj.acquireTokenPopup(request)
                    .then(response => {
                        console.log(response);
                        return response;
                    }).catch(error => {
                        console.log(error);
                    });
            } else {
                console.log(error);
            }
        });
}

function passTokenToApi(key,img = null) {
    getTokenPopup(tokenRequest)
        .then(response => {
            if (response) {
                console.log("access_token acquired at: " + new Date().toString());
                //console.log("Access token response : " + response.accessToken); //Added by Rost
                try {
                    switch (key) {
                        case "GET":
                            readUserInfo(apiConfigRead.webApi+getUserId(response.accessToken), response.accessToken); //Call information from backend about user
                            //console.log("Current user id is: " + apiConfigRead.webApi + getUserId(response.accessToken))
                            break;
                        case "PUT":
                            writeUserInfo(apiConfigWrite.webApi, response.accessToken); //Save information about user
                            break;
                        case "POST":
                            uploadImage(apiUploadImage.webApi, response.accessToken,img); //Upload image
                            break;
                    }
                    
                } catch (error) {
                    console.log(error);
                }
            }
        });
}

/**
 * To initiate a B2C user-flow, simply make a login request using
 * the full authority string of that user-flow e.g.
 * https://fabrikamb2c.b2clogin.com/fabrikamb2c.onmicrosoft.com/B2C_1_edit_profile_v2 
 */
async function editProfile() {
    hideUserShowLoader()

    passTokenToApi("GET") //pass token and call my API


    // const editProfileRequest = b2cPolicies.authorities.editProfile;
    // editProfileRequest.loginHint = myMSALObj.getAccountByHomeId(accountId).username;

    // myMSALObj.loginPopup(editProfileRequest)
    //     .catch(error => {
    //         console.log(error);
    //     });
}

function saveProfile(){
    const editProfileArea = document.getElementById('editProfileArea')//Rost edit profile
    editProfileArea.classList.add('d-none')//Hide user edit area   
    passTokenToApi("PUT") //pass token and call my API
}

function hideLoaderShowUser()
{
    const editProfileArea = document.getElementById('editProfileArea')//Rost edit profile
    editProfileArea.classList.remove('d-none')//Rost show user profile editing 
    const loader = document.getElementById('loader')
    loader.classList.add('d-none')
    document.getElementById('insuranceArea').classList.remove('d-none')
    document.getElementById('profileText').textContent = "Profile Settings"
}

function checkRegistration()
{
    const birthdate = document.getElementById('tbBirthDate') //In case registartion finish set fields to readonly 
    const birthnumber = document.getElementById('tbBirthnumber')
    if(birthdate.value.length > 0 || birthnumber.value.length > 0) 
    {
        birthdate.readOnly = true;
        birthnumber.readOnly = true;
    } 

    const name = document.getElementById('tbName') //Hide all elements except registration
    const surname = document.getElementById('tbSurname')
    if( name.value.length == 0 || 
        surname.value.length == 0 || 
        birthdate.value.length == 0 || 
        birthnumber.value.length == 0 )
    {
        hideLoaderShowUser()
        document.getElementById('insuranceArea').classList.add('d-none')
        document.getElementById('profileText').textContent = "FINISH REGISTRATION PLEASE"
    }

}

function hideUserShowLoader()
{
    const editProfileArea = document.getElementById('editProfileArea');//Rost edit profile
    editProfileArea.classList.add('d-none')//Rost show user profile editing 
    const loader = document.getElementById('loader')
    loader.classList.remove('d-none')

}