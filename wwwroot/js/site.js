// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function edit(val){

    window.location.href = "http://localhost:5000/Task?edit=" + val;

}

function getUser(val){
    "user strict";

    fetch("user/" + val)
        .then((response) => {
            return response.json();
        })
        .then((myJson) => {

            var user = myJson;

            var wrapperDiv = document.getElementById("wrapper");

            var wrapperDivInnerHTML = "";

            wrapperDivInnerHTML =
            "<div class=\"wrapperUser\">" +
                "<form action=\"/user/edit\" method=\"post\">" +
                    "<fieldset>"+
                    "<legend>User</legend>" +
                    "<div>" +
                        "<label for=\"userId\">ID: </label><br>" +
                        "<input readonly type=\"number\" id=\"id\"" + "name=\"id\" value="+ user.userId + ">"+
                    "</div>" +
                    "<div>" +
                        "<label for=\"username\">Username: </label><br>" +
                        "<input type=\"text\" id=\"username\"" + "name=\"username\" value="+ user.username + ">"+
                    "</div>" +
                    "<div>" +
                        "<label for=\"username\">Email: </label><br>" +
                        "<input type=\"text\" id=\"email\"" + "name=\"email\" value="+ user.email + ">"+
                    "</div>" +
                    "<div>" +
                        "<label for=\"username\">Password: </label><br>" +
                        "<input type=\"text\" id=\"password\"" + "name=\"password\" value=\"\">"+
                    "</div>" +
                    "<div>" +
                        "<label for=\"userId\">Privilege: </label><br>" +
                        "<input type=\"number\" id=\"privilege\"" + "name=\"privilege\" value="+ user.privilege + ">"+
                    "</div>" +
                    "<div>" +
                        "<input type=\"submit\" value=\"Edit\">" +
                    "</div>" +
                "</fieldset>" +
            "</form>" +
            "<button onclick=\"deleteUser("+user.userId+")\">Delete</button>" +
        "</div>";

        wrapperDiv.innerHTML = wrapperDivInnerHTML;

        });

}

function deleteUser(val){
    console.log(val);

    fetch("user/delete/" + val, {method: 'DELETE'})
        .then((response) => {
            return response;
        })
        .then((myJson) => {
            console.log("reloading..");
            window.location.reload();
        });

}


