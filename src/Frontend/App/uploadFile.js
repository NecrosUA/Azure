function uploadImage(endpoint,token,imageFile)
{
    const picSrc = document.getElementById('picSrc');
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
    .then(response => response.json()) //get response image filename 
    .then(response => {
        //console.log("Resposnse name is: "+response)
        picSrc.src = "https://rostupload.blob.core.windows.net/images/"+response //set new image
    }).catch(error => {
        console.error(error);
    });
}