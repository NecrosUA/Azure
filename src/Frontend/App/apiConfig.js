// The current application coordinates were pre-registered in a B2C tenant.
// const apiConfig = { //Call watch API
//     b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.read"],
//     webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/watchinfo?id=abc"
//   };

  const apiConfigReadInsurance = { //TODO implement this on backend => Call info about insurance 
    b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.read"],
    webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/insurance/"
  };

  const apiConfigWriteInsurance = { //TODO implement this on backend => Save user insurance 
    b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.write"],
    webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/insurance"
  };

  const apiConfigRead = { //Call userinfo
    b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.read"],
    webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/users/"
  };

  const apiConfigWrite = { //save userinfo
    b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.write"],
    webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/users"
  };

  const apiUploadImage = { //Upload image
    b2cScopes: ["https://rostb2c.onmicrosoft.com/tasks.read/tasks.write"],
    webApi: "https://premiumwatchreseller.azure-api.net/watchportalrost/images"
  };