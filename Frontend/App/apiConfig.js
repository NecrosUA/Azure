// The current application coordinates were pre-registered in a B2C tenant.
// const apiConfig = { //Call watch API
//     b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.read"],
//     webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/watchinfo?id=abc"
//   };


const apiConfigRead = { //Call userinfo
    b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.read"],
    webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/readuser?pid=PID1234567890"
  };

  const apiConfigWrite = { //Call userinfo
    b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.write"],
    webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/writeuser"
  };

  const apiUploadImage = { //Upload image
    b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.write"],
    webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/uploadimage"
  };