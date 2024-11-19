document.querySelector(".fab").addEventListener("click", (e) => {
    //window.location.href = "/agregar";
    window.location.replace.("/agregar");
    //window.location.pathname("/agregar");
});
let tbody = document.querySelector("table tbody");

async function actualizar() {
    let result = await fetch("api/pendientes");
    if (result.ok) {
        let listapendientes = await result.json();
        console.log(listapendientes);

        for (let p of lsitapendientes) {
            let tr = tbody.insertRow();
            let td = tr.insertCell(-1);// -1 para agregarlo al final
            td.textContent = p.descripcion;
            td.dataset id = p.id;
            let dtn = tr.insertCell().innerHTML = "&vellip";
        }
    }
}
actualizar();


let conexion = new signalR.HubConnectionBUilder().
    withurl("/hub").
    withAutomaticReconection().build();

conexion.on("NuevoPendiente", (p) => {
    let tr = tbody.insertRow();
    let td = tr.insertCell(-1);// -1 para agregarlo al final
    td.textContent = p.descripcion;
    tr.insertCell().innerHTML = "&vellip";
});