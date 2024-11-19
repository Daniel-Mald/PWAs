let form = document.querySelector("form");
let btn = document.getElementById("agregar");
let error = document.getElementById("error");
btn.addEventListener("click", async function (e) {
    error.textContent = "";
    let descr = form.elements["Descripcion"];
    if (descr.chekValidity()) {
        let json = { descripcion: descr.value };
        let result = await fetch("api/Pendientes", {
            method="post",
            body: JSON.stringify(json),

            Headers: {
                "Content-Type": "application/json"
            }

        });
        if (result.ok) {
            window.location.replace("/index");
        }
        else if (result.status == 400) {
            error.textContent = "verifique la informacion";
        }
}
    else {
        descripcion.reportValidity();
    }



    
})