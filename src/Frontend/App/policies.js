/**
 * Enter here the user flows and custom policies for your B2C application
 * To learn more about user flows, visit: https://docs.microsoft.com/en-us/azure/active-directory-b2c/user-flow-overview
 * To learn more about custom policies, visit: https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-overview
 */
const b2cPolicies = {
    names: {
        signUpSignIn: "B2C_1A_SIGNUP_SIGNIN", //Mandatory field, put here Your userflow or Custom policy B2C_1_rostsignin B2C_1A_SIGNUP_SIGNIN
        editProfile: "B2C_1_edit_profile_v2"
    },
    authorities: {
        signUpSignIn: {
            authority: "https://rostb2c.b2clogin.com/rostb2c.onmicrosoft.com/B2C_1A_signup_signin", //B2C_1_rostsignin /B2C_1A_signup_signin
        },
        editProfile: {
            authority: "https://fabrikamb2c.b2clogin.com/fabrikamb2c.onmicrosoft.com/B2C_1_edit_profile_v2"
        }
    },
    authorityDomain: "rostb2c.b2clogin.com" //Mandatory
}