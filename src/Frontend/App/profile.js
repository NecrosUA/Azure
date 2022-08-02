var form = document.getElementById('profile-update-form')
function submitProfile(){

    form.classList.add('was-validated')

    if(form.checkValidity())
    {
        saveProfile()
        form.classList.remove('was-validated')
    } 
}