function Ertekel(p) {
    let pont = document.getElementById("pont").value = p;


    for (let i = 1; i <= 5; i++) {
        let csillag = document.getElementById(i + "cs");

        if (i <= p) {
            csillag.src = "/rating/sargacsillag.png";
        }
        else {
            csillag.src = "/rating/feketecsillag.png";
        }
    }

}

function Emailcheck() {
    let email = document.getElementById("emailbeker").value;
    let emailregex = new RegExp("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$");
    let email_hiba = document.getElementById("emailhiba");

    if (!emailregex.test(email)) {
        email_hiba.innerText = "Hibás email cím!";
        return false;
    }
    else {
        email_hiba.innerText = "";
        return true;
    }
}
