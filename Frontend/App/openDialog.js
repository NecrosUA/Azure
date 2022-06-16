var fileSelector = document.createElement('input');
fileSelector.setAttribute('type', 'file');
fileSelector.setAttribute('accept','image/*');

function openFile() {
     fileSelector.click();

     fileSelector.remove();
     return false;
}

