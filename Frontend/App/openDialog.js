var fileSelector = document.createElement('input');
fileSelector.setAttribute('type', 'file');
fileSelector.setAttribute('accept','image/*');
fileSelector.addEventListener('change', handleFileInput);

function openFile() {   
    fileSelector.click();    
    //return false;
}

function handleFileInput()
{  
    const picSrc = document.getElementById('picSrc');
    const file = fileSelector.files[0]    
    const myFormData = new FormData();

    myFormData.append('File', file);   
    
    passTokenToApi("POSTIMG", myFormData);
    
    //picSrc.src = "https://rostupload.blob.core.windows.net/images/"+file.name; //change image 

    
}

