//Alphabet Textbox input.
function validateNonNumber(evt) {
    var keyCode = evt.which ? evt.which : evt.keyCode;
    return keyCode < '0'.charCodeAt() || keyCode > '9'.charCodeAt();
}


//Disable ASP.Net Validator.
function disableValidator(validator) {

    ValidatorEnable(validator, false);    
}


//Enable ASP.Net Validator.
function enableValidator(validator) {

    ValidatorEnable(validator, true);
}

//Alpha Numeric Textbox.
function FilterAlphaNumeric(evt) {

    var keyCode = evt.which ? evt.which : evt.keyCode;
    if (evt.shiftKey) {
        if (90 >= keyCode && keyCode >= 65) //you can press shiftkey when input alpha.
        {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        if ((57 >= keyCode && keyCode >= 48)
            || (105 >= keyCode && keyCode >= 96)
            || (90 >= keyCode && keyCode >= 65)
            || (40 >= event.keyCode && event.keyCode >= 37)
            || (keyCode == 8) || (keyCode == 46)
            || (keyCode == 13) || (keyCode == 32) || keyCode == 9) {
            return true;
        }
        else {
            return false;
        }
    }
}

//Alpha Numeric Textbox with dash.
function FilterAlphaNumericDash(evt) {

    var keyCode = evt.which ? evt.which : evt.keyCode;
    if (evt.shiftKey) {
        if (90 >= keyCode && keyCode >= 65) //you can press shiftkey when input alpha.
        {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        if ((57 >= keyCode && keyCode >= 48)
            || (105 >= keyCode && keyCode >= 96)
            || (90 >= keyCode && keyCode >= 65)
            || (40 >= event.keyCode && event.keyCode >= 37)
            || (keyCode == 8) || (keyCode == 46) || (keyCode == 189)
            || (keyCode == 13)) {
            return true;
        }
        else {
            return false;
        }
    }
}


function confirmDelete(message1, message2) {

    var ans = confirm(message1);

    if (ans) {
        var otherAns = confirm(message2);
        if (otherAns) {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}