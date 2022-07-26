# JS single page application + Azure AD B2C authorization .Net 6 farmework isolated project functions

## About
Sample project on vanilla JavaScript single page application based on [Microsoft opensource sample] (https://github.com/Azure-Samples/ms-identity-b2c-javascript-spa) using Azure AD B2C authorization .Net 6 farmework isolated project functions, cosmos db for user data and blob storage for images. 
## Overview
* Sign-in, Sign-up user. When user register using AD B2C it also register the same user in cosmos db with extended data about insurance etc.  
* Read and modify information about user.
* Upload user image to blob storage.
* Modify insurance information including benefits (not implemented yet)
* Get token and pass to frontend. 
* Sign out user.