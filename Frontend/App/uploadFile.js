function uploadImage(endpoint,token,imageFile)
{
    const headers = new Headers();
    const bearer = `Bearer ${token}`;

    console.log(...imageFile)
    headers.append("Authorization", bearer);
    
    const options = {
        method: "POST",
        headers: headers,
        body: imageFile
      };
    
    fetch(endpoint, options)
        .catch(error => {
        console.error(error);
    });
}