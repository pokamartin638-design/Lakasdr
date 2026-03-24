function Ertekel(p) {
    document.getElementById("pont").value = p;

    for (let i = 1; i <= 5; i++) {
        const csillag = document.getElementById(i + "cs");
        if (!csillag) continue;
        csillag.src = i <= p ? "/rating/sargacsillag.png" : "/rating/feketecsillag.png";
    }
}

function Emailcheck() {
    const email = document.getElementById("emailbeker")?.value || "";
    const emailregex = new RegExp("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$");
    const emailHiba = document.getElementById("emailhiba");

    if (!emailregex.test(email)) {
        if (emailHiba) emailHiba.innerText = "Hibás email cím!";
        return false;
    }

    if (emailHiba) emailHiba.innerText = "";
    return true;
}

document.addEventListener("DOMContentLoaded", function () {
    const revealItems = document.querySelectorAll(".reveal");

    if ("IntersectionObserver" in window) {
        const observer = new IntersectionObserver((entries) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    entry.target.classList.add("show");
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.12 });

        revealItems.forEach((item) => observer.observe(item));
    } else {
        revealItems.forEach((item) => item.classList.add("show"));
    }
});
